using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterManager : BaseManager, ICharacterInfoView
{
    public CharacterInfoController controllerForCharacterInfo;
    //角色模型数据
    public Dictionary<string, GameObject> dicModel = new Dictionary<string, GameObject>();
    //角色信息数据
    public Dictionary<long, CharacterInfoBean> dicCharacterInfo = new Dictionary<long, CharacterInfoBean>();

    public List<Character> listPlayerCharacter = new List<Character>();
    public List<Character> listEnemyCharacter = new List<Character>();


    private void Awake()
    {
        InitAllCharacterInfo();
    }

    /// <summary>
    /// 初始化所有数据
    /// </summary>
    public void InitAllCharacterInfo()
    {
        if (controllerForCharacterInfo == null)
        {
            controllerForCharacterInfo = new CharacterInfoController(this, this);
        }
        Action<List<CharacterInfoBean>> callBack = (listCharacter) =>
        {
            dicCharacterInfo.Clear();
            for (int i = 0; i < listCharacter.Count; i++)
            {
                CharacterInfoBean itemData = listCharacter[i];
                dicCharacterInfo.Add(itemData.id, itemData);
            }
        };
        controllerForCharacterInfo.GetAllCharacterInfoData(callBack);
    }

    /// <summary>
    /// 获取所有角色
    /// </summary>
    /// <returns></returns>
    public List<Character> GetAllCharacter()
    {
        List<Character> listData = new List<Character>();
        if(!CheckUtil.ListIsNull(listPlayerCharacter))
            listData.AddRange(listPlayerCharacter);
        if (!CheckUtil.ListIsNull(listEnemyCharacter))
            listData.AddRange(listEnemyCharacter);
        return listData;
    }

    /// <summary>
    /// 分配对手
    /// </summary>
    /// <param name="character"></param>
    public Character DistributeRival(Character character)
    {
        List<Character> listRival = null;
        if (character.camp == CampEnum.Player)
        {
            listRival = listEnemyCharacter;
        }
        else if (character.camp == CampEnum.Enemy)
        {
            listRival = listPlayerCharacter;
        }
        if (CheckUtil.ListIsNull(listRival))
        {
            return null;
        }
        float minDistance = float.MaxValue;
        Character rivalCharacter = null;
        for (int i = 0; i < listRival.Count; i++)
        {
            Character itemRivalCharacter = listRival[i];
            //选择距离最近的对手
            float tempDis = Vector3.Distance(character.transform.position, itemRivalCharacter.transform.position);
            if (tempDis < minDistance)
            {
                rivalCharacter = itemRivalCharacter;
                minDistance = tempDis;
            }
        }
        return rivalCharacter;
    }

    /// <summary>
    /// 根据ID获取角色数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="action"></param>
    public CharacterInfoBean GetCharacterInfoById(long id)
    {
        if (dicCharacterInfo == null || dicCharacterInfo.Count == 0)
            return null;
        if (dicCharacterInfo.TryGetValue(id, out CharacterInfoBean value))
        {
            return value;
        }
        return null;
    }

    /// <summary>
    /// 获取角色基础模型
    /// </summary>
    /// <param name="characterCamp"></param>
    /// <returns></returns>
    public GameObject GetCharacterBaseModel(CampEnum characterCamp)
    {
        string baseModelName = "";

        switch (characterCamp)
        {
            case CampEnum.Player:
                baseModelName = "PlayerCharacter";
                break;
            case CampEnum.Enemy:
                baseModelName = "EnemyCharacter";
                break;
        }
        GameObject objBaseModel = GetModel(dicModel, "character/base", baseModelName, "Assets/Prefabs/Character/Base/" + baseModelName + ".prefab");
        return objBaseModel;
    }

    /// <summary>
    /// 获取样子模型
    /// </summary>
    /// <param name="modelName"></param>
    /// <returns></returns>
    public GameObject GetCharacterLookModel(string modelName)
    {
        GameObject objBaseModel = GetModel(dicModel, "character/character", modelName, "Assets/Prefabs/Character/" + modelName + ".prefab");
        return objBaseModel;
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="character"></param>
    public void AddCharacter(Character character)
    {
        if (character.camp == CampEnum.Enemy)
        {
            listEnemyCharacter.Add(character);
        }
        else if (character.camp == CampEnum.Player)
        {
            listPlayerCharacter.Add(character);
        }
    }

    /// <summary>
    /// 移除角色
    /// </summary>
    /// <param name="character"></param>
    public void RemoveCharacter(Character character)
    {
        if (character.camp == CampEnum.Enemy)
        {
            if (listEnemyCharacter.Contains(character))
                listEnemyCharacter.Remove(character);
        }
        else if (character.camp == CampEnum.Player)
        {
            if (listPlayerCharacter.Contains(character))
                listPlayerCharacter.Remove(character);
        }
    }

    /// <summary>
    /// 清除所有角色
    /// </summary>
    public void ClearAllCharacter()
    {
        CptUtil.RemoveChildsByActive(gameObject);
        listPlayerCharacter.Clear();
        listEnemyCharacter.Clear();
    }

    public void ClearAllCharacterInEditor()
    {
        CptUtil.RemoveChildsByActiveInEditor(gameObject);
        listPlayerCharacter.Clear();
        listEnemyCharacter.Clear();
    }


    #region 角色获取回调
    public void GetCharacterInfoFail(string failMsg, Action action)
    {

    }

    public void GetCharacterInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }
    #endregion
}
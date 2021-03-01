﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterManager : BaseManager, ICharacterInfoView
{
    public CharacterInfoController characterInfoController;
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
        if (characterInfoController == null)
        {
            characterInfoController = new CharacterInfoController(this, this);
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
        characterInfoController.GetAllCharacterInfoData(callBack);
    }


    /// <summary>
    /// 分配对手
    /// </summary>
    /// <param name="character"></param>
    public Character DistributeRival(Character character)
    {
        List<Character> listRival = null;
        if (character.characterCamp == CharacterCampEnum.Player)
        {
            listRival = listEnemyCharacter;
        }
        else if (character.characterCamp == CharacterCampEnum.Enemy)
        {
            listRival = listPlayerCharacter;
        }
        if (CheckUtil.ListIsNull(listRival))
        {
            return null;
        }
        float mindistance = float.MaxValue;
        Character rivalCharacter = null;
        for (int i = 0; i < listRival.Count; i++)
        {
            Character itemRivalCharacter = listRival[i];
            //选择距离最近的对手
            float tempDis = Vector3.Distance(character.transform.position, itemRivalCharacter.transform.position);
            if (tempDis < mindistance)
            {
                rivalCharacter = itemRivalCharacter;
                mindistance = tempDis;
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
    public GameObject GetCharacterBaseModel(CharacterCampEnum characterCamp)
    {
        string baseModelName = "";

        switch (characterCamp)
        {
            case CharacterCampEnum.Player:
                baseModelName = "PlayerCharacter";
                break;
            case CharacterCampEnum.Enemy:
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
        if (character.characterCamp == CharacterCampEnum.Enemy)
        {
            listEnemyCharacter.Add(character);
        }
        else if (character.characterCamp == CharacterCampEnum.Player)
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
        if (character.characterCamp == CharacterCampEnum.Enemy)
        {
            if (listEnemyCharacter.Contains(character))
                listEnemyCharacter.Remove(character);
        }
        else if (character.characterCamp == CharacterCampEnum.Player)
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
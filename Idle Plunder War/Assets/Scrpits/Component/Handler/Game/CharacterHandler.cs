using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterHandler : BaseHandler<CharacterHandler, CharacterManager>
{

    /// <summary>
    /// 创建友军
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="position"></param>
    public void CreatePlayerCharacter(List<long> ids, Vector3 position)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            long characterId = ids[i];
            CharacterInfoBean data = manager.GetCharacterInfoById(characterId);
            CreateCharacter(CharacterCampEnum.Player, data, position);
        }
    }

    /// <summary>
    /// 创建敌人
    /// </summary>
    /// <param name="id"></param>
    /// <param name="position"></param>
    public void CreateEnemyCharacter(long id, Vector3 position)
    {
        CharacterInfoBean data = manager.GetCharacterInfoById(id);
        CreateCharacter(CharacterCampEnum.Enemy, data, position);
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="characterCamp"></param>
    /// <param name="characterInfo"></param>
    /// <param name="position"></param>
    protected void CreateCharacter(CharacterCampEnum characterCamp, CharacterInfoBean characterInfo, Vector3 position)
    {
        //获取模型
        GameObject objBaseModel = manager.GetCharacterBaseModel(characterCamp);
        GameObject objLookModel = manager.GetCharacterLookModel(characterInfo.model_name);
        //实例化
        GameObject objChacater = Instantiate(gameObject, objBaseModel, position);
        Instantiate(objChacater, objLookModel);
        //设置数据
        Character character = objChacater.GetComponent<Character>();
        character.SetData(characterInfo);
    }

}
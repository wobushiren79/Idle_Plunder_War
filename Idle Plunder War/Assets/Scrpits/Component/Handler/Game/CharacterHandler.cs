using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterHandler : BaseHandler<CharacterHandler, CharacterManager>
{

    /// <summary>
    /// 移动友方角色
    /// </summary>
    /// <param name="position"></param>
    public void MovePlayerCharacter(Vector3 position)
    {
        List<Character> listPlayer = manager.listPlayerCharacter;
        for (int i = 0; i < listPlayer.Count; i++)
        {
            Character itemCharacter= listPlayer[i];
            itemCharacter.characterAI.moveTarget = position;
            itemCharacter.characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToTouch);
        }
    }

    /// <summary>
    /// 创建友军
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="position"></param>
    public void CreatePlayerCharacter(long id, Vector3 position)
    {
        CharacterInfoBean data = manager.GetCharacterInfoById(id);
        Character character = CreateCharacter(CharacterCampEnum.Player, data, position, Vector3.zero);

        CharacterForPlayer characterForPlayer = character as CharacterForPlayer;
        characterForPlayer.characterAI.ChangeIntent(AIIntentEnum.CharacterPlayerIdle);
    }

    /// <summary>
    /// 创建敌人
    /// </summary>
    /// <param name="id"></param>
    /// <param name="position"></param>
    public Character CreateEnemyCharacter(long id, Vector3 position, Vector3 eulerAngles)
    {
        CharacterInfoBean data = manager.GetCharacterInfoById(id);
        Character character = CreateCharacter(CharacterCampEnum.Enemy, data, position, eulerAngles);
        return character;
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="characterCamp"></param>
    /// <param name="characterInfo"></param>
    /// <param name="position"></param>
    protected Character CreateCharacter(CharacterCampEnum characterCamp, CharacterInfoBean characterInfo, Vector3 position, Vector3 eulerAngles)
    {
        //获取模型
        GameObject objBaseModel = manager.GetCharacterBaseModel(characterCamp);
        GameObject objLookModel = manager.GetCharacterLookModel(characterInfo.model_name);
        //实例化
        GameObject objChacater = Instantiate(gameObject, objBaseModel, position);
        Instantiate(objChacater, objLookModel);
        objChacater.transform.eulerAngles = eulerAngles;
        //设置数据
        Character character = objChacater.GetComponent<Character>();
        character.SetData(characterCamp, characterInfo);
        //记录进list
        manager.AddCharacter(character);

        return character;
    }

    /// <summary>
    /// 刷新友方角色
    /// </summary>
    public void RefreshPlayerCharacter()
    {
        List<Character> listPlayerCharacter = manager.listPlayerCharacter;
        for (int i = 0; i < listPlayerCharacter.Count; i++)
        {
            Character itemCharacter = listPlayerCharacter[i];
            itemCharacter.RefreshData();
        }
    }
}
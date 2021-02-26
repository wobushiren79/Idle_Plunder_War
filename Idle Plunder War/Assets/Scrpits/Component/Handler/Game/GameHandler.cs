using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameHandler, GameManager>
{
    public void InitGame(Action callBack)
    {
        //初始化游戏数据
        GameBean gameData = manager.InitGameData();

        Action<SceneInfoBean> action = (data) =>
        {
            //创建敌人
            List<EnemyCharacterData> listEnemyData = data.GetListEnemyData();
            InitEnemy(listEnemyData);
            callBack?.Invoke();
        };
        //获取场景数据
        manager.GetSceneInfoById(1, action);
    }

    public void StartGame()
    {
        StartCoroutine(CoroutineForCreatePlayerCharacter());
    }

    public void EndGame()
    {
        StopAllCoroutines();
    }


    public void InitEnemy(List<EnemyCharacterData> listEnemyData)
    {
        if (CheckUtil.ListIsNull(listEnemyData))
            return;
        for (int i = 0; i < listEnemyData.Count; i++)
        {
            EnemyCharacterData enemyData = listEnemyData[i];
            CharacterHandler.Instance.CreateEnemyCharacter(enemyData.enemyId, enemyData.position.GetVector3());
        }
    }

    /// <summary>
    /// 协程-创建友方角色
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoroutineForCreatePlayerCharacter()
    {
        SceneInfoBean sceneInfo = manager.sceneInfoData;
        while (manager.gameData.gameStatus == GameStatusEnum.InGame)
        {
            yield return new WaitForSeconds(sceneInfo.character_build_interval);

        }
    }
}
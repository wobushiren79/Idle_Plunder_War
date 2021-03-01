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
        gameData.SetGameStatus(GameStatusEnum.Init);
        UserDataBean userData = GameDataHandler.Instance.manager.GetUserData();
        if (userData ==null || CheckUtil.StringIsNull(userData.userId))
        {
            userData = GameDataHandler.Instance.CreateNewData();
        }
            
        Action<SceneInfoBean> action = (data) =>
        {
            //创建敌人
            List<EnemyCharacterData> listEnemyData = data.GetListEnemyData();
            CreateEnemy(listEnemyData);
            callBack?.Invoke();
        };
        //获取场景数据
        manager.GetSceneInfoById(1, action);
    }

    public void StartGame()
    {
        manager.gameData.SetGameStatus(GameStatusEnum.InGame);
        StartCoroutine(CoroutineForCreatePlayerCharacter());
    }

    public void EndGame()
    {
        manager.gameData.SetGameStatus(GameStatusEnum.End);
        StopAllCoroutines();
    }

    /// <summary>
    /// 创建敌人
    /// </summary>
    /// <param name="listEnemyData"></param>
    public void CreateEnemy(List<EnemyCharacterData> listEnemyData)
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
    /// 创建友方
    /// </summary>
    /// <param name="userTeam"></param>
    public void CreatePlayer(UserTeamBean userTeam)
    {
        List<long> listMember = userTeam.listMember;
        //获取生成点位置
        Transform positionPlayerBuild= GameSceneHandler.Instance.manager.positionPlayerBuild;
        for (int i = 0; i < listMember.Count; i++)
        {
            long memberId = listMember[i];
            CharacterHandler.Instance.CreatePlayerCharacter(memberId, positionPlayerBuild.position);
        }
    }

    /// <summary>
    /// 协程-创建友方角色
    /// </summary>
    /// <returns></returns>
    public IEnumerator CoroutineForCreatePlayerCharacter()
    {
        SceneInfoBean sceneInfo = manager.sceneInfoData;
        UserDataBean userData = GameDataHandler.Instance.manager.GetUserData();
        while (manager.gameData.gameStatus == GameStatusEnum.InGame)
        {
            CreatePlayer(userData.teamData);
            yield return new WaitForSeconds(sceneInfo.character_build_interval);
        }
    }
}
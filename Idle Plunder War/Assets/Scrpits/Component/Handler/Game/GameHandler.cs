using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameHandler : BaseHandler<GameHandler, GameManager>
{

    public float timeCountdownForCreatePlayer = 0;

    public void InitGame(Action callBack)
    {
        //初始化游戏数据
        GameBean gameData = manager.InitGameData();
        gameData.SetGameStatus(GameStatusEnum.Init);
        UserDataBean userData = GameDataHandler.Instance.manager.GetUserData();
        if (userData == null || CheckUtil.StringIsNull(userData.userId))
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
        UIHandler.Instance.manager.OpenUIAndCloseOther<UIGameMain>(UIEnum.GameMain);
        manager.gameData.SetGameStatus(GameStatusEnum.InGame);
        StartCoroutine(CoroutineForCreatePlayerCharacter());
        StartCoroutine(CoroutineForAddLevelUp());
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
            Character character = CharacterHandler.Instance.CreateEnemyCharacter(enemyData.enemyId, enemyData.position.GetVector3(), enemyData.eulerAngles.GetVector3());

            CharacterForEnemy characterForEnemy = character as CharacterForEnemy;
            characterForEnemy.characterAI.ChangeIntent(AIIntentEnum.CharacterEnemyIdle);
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
        Transform positionPlayerBuild = GameSceneHandler.Instance.manager.positionPlayerBuild;
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
        SceneInfoBean sceneInfo = manager.GetSceneInfo();
        UserDataBean userData = GameDataHandler.Instance.manager.GetUserData();
        GameBean gameData = manager.gameData;
        while (manager.gameData.gameStatus == GameStatusEnum.InGame)
        {
            LevelInfoBean levelInfo = manager.GetLevelInfoForNumber(gameData.levelForNumber);
            levelInfo.GetData(out float levelData);
            int teamNumber = (int)levelData;
            for (int i = 0; i < teamNumber; i++)
            {
                CreatePlayer(userData.teamData);
            }
            timeCountdownForCreatePlayer = sceneInfo.character_build_interval;
            while (timeCountdownForCreatePlayer > 0)
            {
                yield return new WaitForSeconds(1);
                timeCountdownForCreatePlayer -= 1;
            }
        }
    }

    public IEnumerator CoroutineForAddLevelUp()
    {
        GameBean gameData = manager.gameData;
        while (manager.gameData.gameStatus == GameStatusEnum.InGame)
        {
            LevelInfoBean levelInfo = manager.GetLevelInfoForLevelUp(gameData.levelForUp);
            gameData.AddLevelUpPro(levelInfo.pro);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
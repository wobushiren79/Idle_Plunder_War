﻿using System;
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
        GameBean gameData = manager.gameData;
        gameData.SetGameStatus(GameStatusEnum.Init);
        UserDataBean userData = GameDataHandler.Instance.manager.GetUserData();
        if (userData == null || CheckUtil.StringIsNull(userData.userId))
        {
            userData = GameDataHandler.Instance.CreateNewData();
        }
        //清除所有角色
        CharacterHandler.Instance.manager.ClearAllCharacter();
        //清除所有角色
        BuildingHandler.Instance.manager.ClearAllBuilding();
        //清除所有宝藏
        TreasureHandler.Instance.manager.ClearAllTreasure();

        Action<SceneInfoBean> action = (data) =>
        {
            //设置相机
            data.GetCameraPosition(out Vector3 cameraPostion, out Vector3 cameraAngle);
            CameraHandler.Instance.SetCameraPosition(cameraPostion, cameraAngle);
            //创建敌人
            List<EnemyCharacterData> listEnemyCharacter = data.GetListEnemyData();
            CreateEnemy(listEnemyCharacter);
            //创建建筑
            List<EnemyBuildingData> listEnemyBuildings = data.GetListBuildingData();
            CreateBuilding(listEnemyBuildings);
            //创建宝藏
            EnemyTreasureData treasureData = data.GetTreasureData();
            CreateTreasure(treasureData.treasureId, treasureData.position.GetVector3(), treasureData.eulerAngles.GetVector3());
            //初始化完成
            callBack?.Invoke();
        };
        //获取场景数据
        manager.GetSceneInfoById(gameData.gameSceneNumber, action);
    }

    public void StartGame()
    {

        AstarPath.active.Scan();
        UIHandler.Instance.manager.OpenUIAndCloseOther<UIGameMain>(UIEnum.GameMain);
        manager.gameData.SetGameStatus(GameStatusEnum.InGame);
        StartCoroutine(CoroutineForCreatePlayerCharacter());
        StartCoroutine(CoroutineForAddLevelUp());
    }

    public void EndGame()
    {
        manager.gameData.SetGameStatus(GameStatusEnum.End);
        //停止所有携程
        StopAllCoroutines();
        //所有角色休息
        List<Character> listAllCharacter = CharacterHandler.Instance.manager.GetAllCharacter();
        for (int i = 0; i < listAllCharacter.Count; i++)
        {
            Character itemCharacter = listAllCharacter[i];
            itemCharacter.characterAI.ChangeIntent(AIIntentEnum.CharacterRest);
        }
        GameBean gameData = manager.gameData;
        gameData.gameSceneNumber++;
        if (gameData.gameSceneNumber > 5)
        {
            gameData.gameSceneNumber = 1;
        }
        UIGameEnd uiGameEnd = UIHandler.Instance.manager.OpenUIAndCloseOther<UIGameEnd>(UIEnum.GameEnd);
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
    /// 创建建筑
    /// </summary>
    /// <param name="listEnemyData"></param>
    public void CreateBuilding(List<EnemyBuildingData> listBuildingData)
    {
        if (CheckUtil.ListIsNull(listBuildingData))
            return;
        for (int i = 0; i < listBuildingData.Count; i++)
        {
            EnemyBuildingData buildingData = listBuildingData[i];
            Building building = BuildingHandler.Instance.CreateBuilding(buildingData.buildingId, buildingData.position.GetVector3(), buildingData.eulerAngles.GetVector3());
            if (building.buildingInfoData.GetBuildingType() == 0)
            {
                building.buildingAI.ChangeIntent(AIIntentEnum.BuildingIdle);
            }
        }
    }

    public void CreateTreasure(long treasureId, Vector3 position, Vector3 eulerAngles)
    {
        TreasureInfoBean treasureInfo = TreasureHandler.Instance.manager.GetTreasureInfo(treasureId);
        TreasureHandler.Instance.CreateTreasure(treasureInfo, position, eulerAngles);
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

        List<long> listAddCharacterData = sceneInfo.GetPlayerCharacter();
        while (manager.gameData.gameStatus == GameStatusEnum.InGame)
        {
            if (CharacterHandler.Instance.manager.listPlayerCharacter.Count >= gameData.maxPlayerCharacterNumber)
            {

            }
            else
            {
                sceneInfo.GetPlayerPosition(out Vector3 playerPostion, out Vector3 playerAngle);
                //创建默认
                LevelInfoBean levelInfo = manager.GetLevelInfoForNumber(gameData.levelForNumber);
                levelInfo.GetData(out float levelData);
                int teamNumber = (int)levelData;
                List<long> listMember = userData.teamData.listMember;
                int totalNumber = teamNumber * listMember.Count + listAddCharacterData.Count;
                int numberHorizontal = 0;
                int numberVertical = 0;
                float offset = 0.5f;
                for (int i = 0; i < teamNumber; i++)
                {
                    for (int f = 0; f < listMember.Count; f++)
                    {
                        long memberId = listMember[f];
                        CharacterHandler.Instance.CreatePlayerCharacter(memberId, playerPostion + new Vector3(offset * numberHorizontal, 0, offset * numberVertical), playerAngle);

                        numberHorizontal++;
                        if (numberHorizontal > (int)Math.Sqrt(totalNumber))
                        {
                            numberHorizontal = 0;
                            numberVertical++;
                        }
                    }
                }
                //创建场景添加
                for (int i = 0; i < listAddCharacterData.Count; i++)
                {
                    long itemId = listAddCharacterData[i];
                    CharacterHandler.Instance.CreatePlayerCharacter(itemId, playerPostion + new Vector3(offset * numberHorizontal, 0, offset * numberVertical), playerAngle);

                    numberHorizontal++;
                    if (numberHorizontal > totalNumber / 2)
                    {
                        numberHorizontal = 0;
                        numberVertical++;
                    }
                }
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
            yield return new WaitForSeconds(0.01f);
        }
    }
}
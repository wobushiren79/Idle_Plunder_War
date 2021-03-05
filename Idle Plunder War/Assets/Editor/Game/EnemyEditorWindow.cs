using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyEditor : EditorWindow
{

    protected SceneInfoService sceneInfoService;
    protected SceneInfoBean sceneInfo = new SceneInfoBean();

    protected long characterId = 0;
    protected long buildingId = 0;
    //protected static Object objContainer;
    [MenuItem("工具/场景创建")]
    static void CreateWindows()
    {
        EditorWindow.GetWindow(typeof(EnemyEditor));
    }

    private void OnEnable()
    {
        sceneInfoService = new SceneInfoService();
    }

    private void OnDestroy()
    {
        GameObject.DestroyImmediate(CharacterHandler.Instance.gameObject);
        GameObject.DestroyImmediate(BuildingHandler.Instance.gameObject);
    }


    private void OnGUI()
    {
        //objContainer = EditorGUILayout.ObjectField(objContainer, typeof(Object), true, GUILayout.Width(200));
        GUIForScene();
    }

    protected void GUIForScene()
    {
        EditorGUILayout.BeginHorizontal();
        if (EditorUI.GUIButton("获取关卡数据", 150)) { GetSceneInfoData(sceneInfo.id); };
        if (EditorUI.GUIButton("保存关卡数据", 150)) { SetSceneInfoData(sceneInfo); };
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(50);

        EditorGUILayout.BeginHorizontal();
        EditorUI.GUIText("关卡ID");
        sceneInfo.id = EditorUI.GUIEditorText(sceneInfo.id);
        EditorUI.GUIText("友方角色生成间隔", 150);
        sceneInfo.character_build_interval = EditorUI.GUIEditorText(sceneInfo.character_build_interval);
        EditorUI.GUIText("敌人活动范围半径",150);
        sceneInfo.enemy_range = EditorUI.GUIEditorText(sceneInfo.enemy_range);
        EditorUI.GUIText("宝箱ID", 150);
        sceneInfo.treasure_id = EditorUI.GUIEditorText(sceneInfo.treasure_id);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(50);

        EditorGUILayout.BeginHorizontal();
        if (EditorUI.GUIButton("生成单个角色", 150)) { CreateCharacter(characterId, Vector3.zero, Vector3.zero); };
        EditorUI.GUIText("角色ID");
        characterId = EditorUI.GUIEditorText(characterId);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (EditorUI.GUIButton("生成单个建筑", 150)) { CreateBuilding(buildingId, Vector3.zero, Vector3.zero); };
        EditorUI.GUIText("建筑ID");
        buildingId = EditorUI.GUIEditorText(buildingId);
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 获取场景数据
    /// </summary>
    /// <param name="id"></param>
    protected void GetSceneInfoData(long id)
    {
        List<SceneInfoBean> listData = sceneInfoService.QueryDataById(id);
        if (!CheckUtil.ListIsNull(listData))
        {
            sceneInfo = listData[0];
            //敌人ID
            List<EnemyCharacterData> listEnemyCharacterData = sceneInfo.GetListEnemyData();
            CharacterHandler.Instance.manager.ClearAllCharacterInEditor();
            foreach (EnemyCharacterData itemData in listEnemyCharacterData)
            {
                CreateCharacter(itemData.enemyId, itemData.position.GetVector3(), itemData.eulerAngles.GetVector3()); ;
            }
            //建筑ID
            List<EnemyBuildingData> listEnemyBuildingData = sceneInfo.GetListBuildingData();
            BuildingHandler.Instance.manager.ClearAllCharacterInEditor();
            foreach (EnemyBuildingData itemData in listEnemyBuildingData)
            {
                CreateBuilding(itemData.buildingId, itemData.position.GetVector3(), itemData.eulerAngles.GetVector3()); ;
            }
        }
    }

    /// <summary>
    /// 设置场景数据
    /// </summary>
    /// <param name="sceneInfo"></param>
    protected void SetSceneInfoData(SceneInfoBean sceneInfo)
    {
        Character[] allCharacter = CharacterHandler.Instance.manager.GetComponentsInChildren<Character>();
        List<EnemyCharacterData> listEnemyData = new List<EnemyCharacterData>();
        foreach (Character itemData in allCharacter)
        {
            long characterId = itemData.characterInfoData.id;
            EnemyCharacterData enemyData = new EnemyCharacterData();
            enemyData.position = new Vector3Bean(itemData.transform.position);
            enemyData.eulerAngles = new Vector3Bean(itemData.transform.eulerAngles);
            enemyData.enemyId = characterId;
            listEnemyData.Add(enemyData);
        }
        sceneInfo.SetListEnemyData(listEnemyData);

        Building[] allBuilding = BuildingHandler.Instance.manager.GetComponentsInChildren<Building>();
        List<EnemyBuildingData> listBuildingData = new List<EnemyBuildingData>();
        foreach (Building itemData in allBuilding)
        {
            long buildingId = itemData.buildingInfoData.id;
            EnemyBuildingData buildingData = new EnemyBuildingData();
            buildingData.position = new Vector3Bean(itemData.transform.position);
            buildingData.eulerAngles = new Vector3Bean(itemData.transform.eulerAngles);
            buildingData.buildingId = buildingId;
            listBuildingData.Add(buildingData);
        }
        sceneInfo.SetListBuildingData(listBuildingData);

        sceneInfoService.UpdateData(sceneInfo);
    }

    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="position"></param>
    protected void CreateCharacter(long id, Vector3 position, Vector3 eulerAngles)
    {
        CharacterHandler.Instance.manager.InitAllCharacterInfo();
        CharacterHandler.Instance.CreateEnemyCharacter(id, position, eulerAngles);
    }

    /// <summary>
    /// 创建建筑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="position"></param>
    protected void CreateBuilding(long id, Vector3 position, Vector3 eulerAngles)
    {
        BuildingHandler.Instance.manager.InitAllBuildingInfo();
        BuildingHandler.Instance.CreateBuilding(id, position, eulerAngles);
    }

}
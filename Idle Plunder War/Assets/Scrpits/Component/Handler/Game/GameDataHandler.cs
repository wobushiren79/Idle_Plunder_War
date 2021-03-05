using UnityEditor;
using UnityEngine;

public class GameDataHandler : BaseHandler<GameDataHandler, GameDataManager>
{
    public UserDataBean CreateNewData()
    {
        UserDataBean userData = new UserDataBean();
        userData.teamData = new UserTeamBean();
        userData.teamData.population = 5;
        //初始化角色
        BaseDataBean baseDataForInitPlayerCharacter = manager.GetBaseData(BaseDataEnum.InitPlayerCharacter);
        long[] characterIds = StringUtil.SplitBySubstringForArrayLong(baseDataForInitPlayerCharacter.content, ',');
        for (int i = 0; i < characterIds.Length; i++)
        {
            long characterId = characterIds[i];
            userData.teamData.listMember.Add(characterId);
        }
        userData.userId = SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        manager.userData = userData;
        return manager.GetUserData();
    }

}
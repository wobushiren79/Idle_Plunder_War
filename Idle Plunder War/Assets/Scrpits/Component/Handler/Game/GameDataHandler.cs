using UnityEditor;
using UnityEngine;

public class GameDataHandler : BaseHandler<GameDataHandler, GameDataManager>
{
    public UserDataBean CreateNewData()
    {
        UserDataBean userData = new UserDataBean();
        userData.teamData = new UserTeamBean();
        userData.teamData.population = 5;
        userData.teamData.listMember.Add(100001);
        userData.teamData.listMember.Add(100001);
        userData.teamData.listMember.Add(100001);

        userData.userId = SystemUtil.GetUUID(SystemUtil.UUIDTypeEnum.N);
        manager.userData = userData;
        return manager.GetUserData();
    }

}
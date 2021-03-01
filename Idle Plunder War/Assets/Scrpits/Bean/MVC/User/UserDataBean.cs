/*
* FileName: UserData 
* Author: AppleCoffee 
* CreateTime: 2021-03-01-11:02:15 
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
public class UserDataBean : BaseBean
{

    public string userId;

    public UserTeamBean teamData = new UserTeamBean();

}
/*
* FileName: UserData 
* Author: AppleCoffee 
* CreateTime: 2021-03-01-11:02:15 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IUserDataView
{
	void GetUserDataSuccess<T>(T data, Action<T> action);

	void GetUserDataFail(string failMsg, Action action);
}
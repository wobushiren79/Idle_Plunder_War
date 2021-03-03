/*
* FileName: LevelInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-03-10:12:03 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface ILevelInfoView
{
	void GetLevelInfoSuccess<T>(T data, Action<T> action);

	void GetLevelInfoFail(string failMsg, Action action);
}
/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-11:07:11 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface ISceneInfoView
{
	void GetSceneInfoSuccess<T>(T data, Action<T> action);

	void GetSceneInfoFail(string failMsg, Action action);
}
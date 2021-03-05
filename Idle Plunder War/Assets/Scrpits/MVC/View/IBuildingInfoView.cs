/*
* FileName: BuildingInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-15:14:57 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IBuildingInfoView
{
	void GetBuildingInfoSuccess<T>(T data, Action<T> action);

	void GetBuildingInfoFail(string failMsg, Action action);
}
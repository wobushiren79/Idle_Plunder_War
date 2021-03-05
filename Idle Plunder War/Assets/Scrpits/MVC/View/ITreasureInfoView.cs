/*
* FileName: TreasureInfo 
* Author: AppleCoffee 
* CreateTime: 2021-03-04-09:59:26 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface ITreasureInfoView
{
	void GetTreasureInfoSuccess<T>(T data, Action<T> action);

	void GetTreasureInfoFail(string failMsg, Action action);
}
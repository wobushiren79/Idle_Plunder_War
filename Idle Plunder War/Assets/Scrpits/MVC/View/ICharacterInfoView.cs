/*
* FileName: CharacterInfo 
* Author: AppleCoffee 
* CreateTime: 2021-02-26-10:29:33 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface ICharacterInfoView
{
	void GetCharacterInfoSuccess<T>(T data, Action<T> action);

	void GetCharacterInfoFail(string failMsg, Action action);
}
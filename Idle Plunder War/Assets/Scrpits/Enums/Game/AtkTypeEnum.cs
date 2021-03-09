using UnityEditor;
using UnityEngine;

public enum AtkTypeEnum
{
    //普通近战
    Melee = 1,
    //远程射箭-单一目标
    RemoteArcherySingle = 101,
    //远程射箭-单一目标追踪
    RemoteArcherySingleTrace = 102,
    //远程射箭-单一目标 范围
    RemoteArcherySingleRange = 103,
}
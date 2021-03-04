using UnityEditor;
using UnityEngine;

public class GameSceneManager : BaseManager
{
    public Transform _positionPlayerBuild;
    public Transform positionPlayerBuild 
    { 
        get 
        {
            if (_positionPlayerBuild == null)
            {
                _positionPlayerBuild = FindWithTag<Transform>(TagInfo.Tag_PlayerBuild);
            }
            return _positionPlayerBuild;
        } 
    }
}
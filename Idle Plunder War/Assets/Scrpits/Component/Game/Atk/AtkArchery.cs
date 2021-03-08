using UnityEditor;
using UnityEngine;

public class AtkArchery : BaseMonoBehaviour
{
    protected BoxCollider archeryCollider;
    private void Awake()
    {
        archeryCollider = GetComponent<BoxCollider>();
    }

}
using Pathfinding;
using UnityEditor;
using UnityEngine;

public class CharacterMove : BaseMonoBehaviour
{
    protected RichAI richAI;

    private void Awake()
    {
        if (richAI == null)
            richAI = GetComponent<RichAI>();
    }
}
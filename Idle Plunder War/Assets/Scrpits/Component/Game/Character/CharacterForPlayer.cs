using UnityEditor;
using UnityEngine;

public class CharacterForPlayer : Character
{
    public AICharacterEntity characterAI;

    public override void Awake()
    {
        base.Awake();
        characterAI = CptUtil.AddCpt<AICharacterForPlayerEntity>(gameObject);
        characterAI.InitData(this);
    }
}
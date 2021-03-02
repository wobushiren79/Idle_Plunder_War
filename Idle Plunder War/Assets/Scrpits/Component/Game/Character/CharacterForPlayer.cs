using UnityEditor;
using UnityEngine;

public class CharacterForPlayer : Character
{

    public override void Awake()
    {
        base.Awake();
        characterAI = CptUtil.AddCpt<AICharacterForPlayerEntity>(gameObject);
        characterAI.InitData(this);
    }
}
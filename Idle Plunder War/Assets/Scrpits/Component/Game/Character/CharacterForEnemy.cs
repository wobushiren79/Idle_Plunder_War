using UnityEditor;
using UnityEngine;

public class CharacterForEnemy : Character
{
    public AICharacterEntity characterAI;

    public override void Awake()
    {
        base.Awake();
        characterAI = CptUtil.AddCpt<AICharacterForEnemyEntity>(gameObject);
        characterAI.InitData(this);
    }
}
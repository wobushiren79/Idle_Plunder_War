using UnityEditor;
using UnityEngine;

public class CharacterForEnemy : Character
{
    public Vector3 positionForStart;
    public Vector3 eulerAnglesForStart;
     
    public override void Awake()
    {
        base.Awake();
        characterAI = CptUtil.AddCpt<AICharacterForEnemyEntity>(gameObject);
        characterAI.InitData(this);
    }

    public override void SetData(CampEnum characterCamp, CharacterInfoBean characterInfoData)
    {
        base.SetData(characterCamp, characterInfoData);
        //设置初始化位置
        positionForStart = transform.position;
        eulerAnglesForStart = transform.eulerAngles;
    }
}
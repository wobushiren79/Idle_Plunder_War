using UnityEditor;
using UnityEngine;

public class Character : BaseMonoBehaviour
{
    public CharacterInfoBean characterInfoData;
    public CharacterCampEnum characterCamp;

    public CharacterMove characterMove;
    public Rigidbody characterRB;

    public virtual void Awake()
    {
        characterMove = GetComponent<CharacterMove>();
        characterRB = GetComponent<Rigidbody>();
        characterRB.drag = float.MaxValue;
        characterRB.angularDrag = float.MaxValue;
        //characterRB.maxDepenetrationVelocity = 1f;
    }

    public void SetData(CharacterCampEnum characterCamp,CharacterInfoBean characterInfoData)
    {
        this.characterCamp = characterCamp;
        this.characterInfoData = characterInfoData;
    }

}
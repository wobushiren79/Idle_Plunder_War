using UnityEditor;
using UnityEngine;

public class Character : BaseMonoBehaviour
{
    public CharacterInfoBean characterInfoData;

    protected CharacterMove characterMove;

    private void Awake()
    {
        characterMove = GetComponent<CharacterMove>();
    }

    public void SetData(CharacterInfoBean characterInfoData)
    {
        this.characterInfoData = characterInfoData;
    }
}
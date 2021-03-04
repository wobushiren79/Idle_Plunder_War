using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Character : BaseMonoBehaviour
{
    public CharacterInfoBean characterInfoData;
    public AICharacterEntity characterAI;
    public CharacterCampEnum characterCamp;

    public int currentMaxLife;
    public int currentLife;
    public int currentAtk;

    public CharacterMove characterMove;
    protected Rigidbody characterRB;
    protected SphereCollider characterCollider;

    public virtual void Awake()
    {
        characterMove = GetComponent<CharacterMove>();
        characterRB = GetComponent<Rigidbody>();
        characterCollider = GetComponent<SphereCollider>();

        characterRB.drag = 0f;
        characterRB.angularDrag = 360;
        characterRB.maxDepenetrationVelocity = 1f;
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="characterCamp"></param>
    /// <param name="characterInfoData"></param>
    public virtual void SetData(CharacterCampEnum characterCamp, CharacterInfoBean characterInfoData)
    {
        this.characterCamp = characterCamp;
        this.characterInfoData = characterInfoData;

        RefreshData();
    }

    public void RefreshData()
    {
        GameBean gameData = GameHandler.Instance.manager.gameData;
        LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPower(gameData.levelForPower);
        levelInfo.GetData(out float value1, out float value2);
        currentMaxLife = characterInfoData.attribute_life + (int)value1;
        currentLife = characterInfoData.attribute_life + (int)value1;
        currentAtk = characterInfoData.attribute_atk + (int)value2;
        characterMove.SetSpeed(characterInfoData.attribute_speed);
    }

    /// <summary>
    /// 改变生命值
    /// </summary>
    /// <param name="addLife"></param>
    public int ChangeLife(int addLife)
    {
        currentLife += addLife;
        if (currentLife < 0)
        {
            currentLife = 0;
        }
        return currentLife;
    }

    /// <summary>
    /// 设置角色死亡
    /// </summary>
    public void SetCharacterDead()
    {
        currentLife = 0;
        gameObject.layer = LayerInfo.Dead;
        characterMove.ClosePath();
        transform
            .DOLocalRotate(new Vector3(90, 0, 0), 1, RotateMode.LocalAxisAdd)
            .OnComplete(() => { Destroy(gameObject); });
        //characterRB.useGravity = true;
        //characterRB.freezeRotation = false;
    }



}
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

        characterRB.drag = 1f;
        characterRB.angularDrag = 360f;
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
        float addLife = 0;
        float addAtk = 0;
        //运行时查询数据
        if (Application.isPlaying)
        {
            GameBean gameData = GameHandler.Instance.manager.gameData;
            LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPower(gameData.levelForPower);
            levelInfo.GetData(out addLife, out addAtk);
        }
        currentMaxLife = characterInfoData.attribute_life + (int)addLife;
        currentLife = characterInfoData.attribute_life + (int)addLife;
        currentAtk = characterInfoData.attribute_atk + (int)addAtk;
        if (characterMove != null)
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
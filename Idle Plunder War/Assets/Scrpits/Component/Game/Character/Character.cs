﻿using DG.Tweening;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class Character : GameBaseItem
{
    public CharacterInfoBean characterInfoData;
    public AICharacterEntity characterAI;

    public CharacterMove characterMove;
    public CharacterAnim characterAnim;

    protected Rigidbody characterRB;
    protected SphereCollider characterCollider;
    protected Renderer _characterRenderer;
    protected Renderer characterRenderer
    {
        get
        {
            if (_characterRenderer == null)
            {
                _characterRenderer = GetComponentInChildren<Renderer>();
            }
            return _characterRenderer;
        }
    }

    public virtual void Awake()
    {
        characterMove = GetComponent<CharacterMove>();
        characterAnim= GetComponent<CharacterAnim>();
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
    public virtual void SetData(CampEnum characterCamp, CharacterInfoBean characterInfoData)
    {
        this.camp = characterCamp;
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
    /// 收到攻击 
    /// </summary>
    /// <param name="objAtk"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public override int UnderAttack(GameBaseItem atk, int damage)
    {
        int life = base.UnderAttack(atk, damage);
        if (life <= 0)
        {
            //如果是敌人 增加金币
            if (camp == CampEnum.Enemy)
            {
                GameBean gameData = GameHandler.Instance.manager.gameData;
                LevelInfoBean levelInfo = GameHandler.Instance.manager.GetLevelInfoForPrice(gameData.levelForPrice);
                levelInfo.GetData(out float levelData);
                gameData.AddGold((long)(characterInfoData.price * levelData));
            }
            characterAI.ChangeIntent(AIIntentEnum.CharacterDead);
        }
        else
        {
            if (characterAI.currentIntent.aiIntent == AIIntentEnum.CharacterPlayerIdle
                || characterAI.currentIntent.aiIntent == AIIntentEnum.CharacterEnemyIdle
                || characterAI.currentIntent.aiIntent == AIIntentEnum.CharacterEnemyBack)
            {
                //如果是人
                if (atk is Character)
                {
                    characterAI.rivalCharacter = atk as Character;
                    characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToRival);
                }
                else if (atk is Building)
                {
                    //如果是建筑
                    characterAI.targetBuilding = atk as Building;
                    characterAI.ChangeIntent(AIIntentEnum.CharacterMoveToBuilding);
                }
            }
        }

        //characterAnim.PlayHit();
        Material[] mats= characterRenderer.materials;
        if (mats.Length >= 2)
        {
            Material material = mats[1];
            material.DOKill();
            material.color = new Color(1,1,1,0);
            material.DOFade(0.5f, 0.5f).From();
        }

        return life;
    }

    /// <summary>
    /// 设置角色死亡
    /// </summary>
    public void SetCharacterDead()
    {
        currentLife = 0;
        gameObject.layer = LayerInfo.Dead;
        characterMove.ClosePath();
        characterAnim.PlayDead();
        StartCoroutine(CoroutineForDeadDestory(2));
        //transform
        //    .DOLocalRotate(new Vector3(90, 0, 0), 1, RotateMode.LocalAxisAdd)
        //    .OnComplete(() => { Destroy(gameObject); });
        //characterRB.useGravity = true;
        //characterRB.freezeRotation = false;
    }

    /// <summary>
    /// 携程-死亡删除
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator CoroutineForDeadDestory(float time)
    {
        yield return new WaitForSeconds(time);
        transform.DOLocalMoveY(-1, 2).OnComplete(() => { Destroy(gameObject); });
    }

}
using UnityEditor;
using UnityEngine;

public class CharacterAnim : BaseMonoBehaviour
{
    protected Animator _characterAnim;

    public Animator characterAnim
    {
        get
        {
            if (_characterAnim == null)
            {
                _characterAnim = GetComponentInChildren<Animator>();
            }
            return _characterAnim;
        }
    }

    public void PlayIdle()
    {
        characterAnim.Play("idle", 0, 0.05f);
    }

    public void PlayWalk()
    {
        characterAnim.Play("walk", 0, 0.05f);
    }

    public void PlayAttack()
    {
        characterAnim.Play("attack", 0, 0.05f);
    }

    public void PlayHit()
    {
        characterAnim.Play("hit", 0, 0.05f);
    }

    public void PlayDead()
    {
        characterAnim.Play("dead", 0, 0.05f);
    }

    public void PlayWin()
    {
        characterAnim.Play("win", 0, 0.05f);
    }
}
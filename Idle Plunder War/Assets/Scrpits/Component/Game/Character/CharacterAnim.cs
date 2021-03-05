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
        characterAnim.Play("idle");
    }

    public void PlayWalk()
    {
        characterAnim.Play("walk");
    }

    public void PlayAttack()
    {
        characterAnim.Play("attack");
    }

    public void PlayHit()
    {
        characterAnim.Play("hit");
    }

    public void PlayDead()
    {
        characterAnim.Play("dead");
    }

    public void PlayWin()
    {
        characterAnim.Play("win");
    }
}
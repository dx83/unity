using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCharacterMonster_backup : ControlCharacterBase
{
    public enum MonsterAni { Idle = 0, Attack, Hurt, Death }

    void Start()
    {
        hashAni = new int[4];
        hashAni[0] = Animator.StringToHash("Lizard_Idle");
        hashAni[1] = Animator.StringToHash("Lizard_Attack");
        hashAni[2] = Animator.StringToHash("Lizard_Hurt");
        hashAni[3] = Animator.StringToHash("Lizard_Death");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation(MonsterAni.Attack);
        }


    }

    public void PlayAnimation(MonsterAni Ani)
    {
        changeAni = (int)Ani;
        if ((int)Ani == 0)
            AnimationClipPlayIdle();
        else if ((int)Ani == 1)
            AnimationClipPlayAttack();
        else if ((int)Ani == 2)
            AnimationClipPlayTakeHit();
        else if ((int)Ani == 3)
            AnimationClipPlayDeath();
    }

    bool bDeath = false, bTakeHit = false, bAttack = false;
    int changeAni = 0;

    public void AnimationClipPlayIdle()
    {
        Ani.Play(hashAni[0]);
    }
    public void AnimationClipPlayAttack()
    {
        if (bAttack)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (changeAni != 2 || changeAni != 3)
                    Ani.Play(hashAni[0]);
                bAttack = false;
            }
            return;
        }

        if (changeAni == 1)
        {
            Ani.Play(hashAni[1]);
            bAttack = true;
            changeAni = 0;
        }
    }
    public void AnimationClipPlayTakeHit()
    {
        if (bTakeHit)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (changeAni != 2 || changeAni != 3)
                    Ani.Play(hashAni[0]);
                bTakeHit = false;
            }
            return;
        }

        if (changeAni == 2)
        {
            Ani.Play(hashAni[2]);
            bTakeHit = true;
            changeAni = 0;
        }
    }

    public void AnimationClipPlayDeath()
    {
        if (bDeath)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Ani.Play(hashAni[0]);
                    bDeath = false;
                    changeAni = 0;
                }
            }
            return;
        }

        if (changeAni == 3)
        {
            Ani.Play(hashAni[3]);
            bDeath = true;
            bAttack = false;
            bTakeHit = false;
        }
    }

    public void AnimationClipDeathEffect()
    {
        Color red = new Color(1.0f, 0.0f, 0.0f);

        if (Sprite.color == red)
            Sprite.color = new Color(1.0f, 1.0f, 1.0f);
        else
            Sprite.color = red;
    }
}

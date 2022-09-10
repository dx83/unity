using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCharacterMonster_backup1 : ControlCharacterBase
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
        //if (Input.GetKeyDown(KeyCode.Space))
        //    PlayAnimation(MonsterAni.Attack);
        //if (Input.GetKeyDown(KeyCode.Return))
        //    PlayAnimation(MonsterAni.Hurt);
        //if (Input.GetKeyDown(KeyCode.D))
        //    PlayAnimation(MonsterAni.Death);

        AnimationClipPlayCheck();
    }

    public void PlayAnimation(MonsterAni Ani)
    {
        changeAni = (int)Ani;
    }

    bool bDeath = false, bTakeHit = false, bAttack = false;
    int changeAni = 0;

    public void AnimationClipPlayCheck()
    {
        if (bAttack)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Ani.Play(hashAni[0]);
                bAttack = false;
                return;
            }
            else if (changeAni != 3)
                return;
        }
        if (bTakeHit)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Ani.Play(hashAni[0]);
                bTakeHit = false;
                return;
            }
            else if (changeAni != 3)
                return;
        }
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

        Ani.Play(hashAni[changeAni]);
        bAttack = (changeAni == 1);
        bTakeHit = (changeAni == 2);
        bDeath = (changeAni == 3);
        changeAni = 0;
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

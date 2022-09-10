using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlCharacterHero_backup : ControlCharacterBase
{
    public enum HeroAni { Idle = 0, Run, Dash, Attack, TakeHit, Death }
    public enum HeroState { Advance, Battle }
    public enum BattleState {  }
    public enum CollideObj { monster = 0, skill, item, stageEnd }

    public HeroState CurrentState;

    public float moveSpeed;

    private Rigidbody2D rgBody;

    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        hashAni = new int[6];
        hashAni[0] = Animator.StringToHash("Hero_Idle");
        hashAni[1] = Animator.StringToHash("Hero_Run");
        hashAni[2] = Animator.StringToHash("Hero_Dash");
        hashAni[3] = Animator.StringToHash("Hero_Attack");
        hashAni[4] = Animator.StringToHash("Hero_TakeHit");
        hashAni[5] = Animator.StringToHash("Hero_Death");
    }

    void Update()
    {
        StateProcess();
    }


    bool isCollided;
    CollideObj collideObj;
    void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
        if (other.tag == "Monster")
            collideObj = CollideObj.monster;
        else if (other.tag == "Skill")
            collideObj = CollideObj.skill;
        else if (other.tag == "Item")
            collideObj = CollideObj.item;
        else if (other.tag == "StageEnd")
            collideObj = CollideObj.stageEnd;
    }
    void OnTriggerExit2D(Collider2D other) { isCollided = false; }


    void StateProcess()
    {
        switch (CurrentState)
        {
            case HeroState.Advance:
                {
                    rgBody.velocity = moveSpeed * new Vector2(1, 0);

                    if (isCollided)
                    {
                        CollideProcess();
                    }
                    else
                        PlayAnimation(HeroAni.Run);

                    break;
                }

            case HeroState.Battle:
                {
                    rgBody.velocity = Vector2.zero;

                    if (isCollided == false)
                    {
                        CurrentState = HeroState.Advance;
                    }
                    else
                        PlayAnimation(HeroAni.Attack);
                    //   CombatStateProcess();
                    break;
                }
        }
    }

    void CollideProcess()
    {
        switch (collideObj)
        {
            case CollideObj.monster:
                CurrentState = HeroState.Battle;
                break;

            case CollideObj.skill:
                break;

            case CollideObj.item:
                break;

            case CollideObj.stageEnd:
                break;
        }
    }


    public void PlayAnimation(HeroAni Ani)
    {
        changeAni = (int)Ani;
        //if ((int)Ani < 4)
        //    AnimationClipPlayLoop((int)Ani);
        //else if ((int)Ani == 4)
        //    AnimationClipPlayTakeHit();
        //else if ((int)Ani == 5)
        //    AnimationClipPlayDeath();
    }

    bool bDeath = false, bTakeHit = false;
    int prevAni = 0, changeAni = 0;

    public void AnimationClipPlayLoop(int index)    //idle run dash attack
    {
        prevAni = index;
        Ani.Play(hashAni[index]);
    }
    public void AnimationClipPlayTakeHit()
    {
        if (bTakeHit)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {   // takeHit 중 death가 들어오면 prevAni로 돌아가지 않도록...
                if (changeAni != 5)
                    Ani.Play(hashAni[prevAni]);
                bTakeHit = false;
            }
            return;
        }

        if (changeAni == 4)
        {
            Ani.Play(hashAni[4]);
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
                    prevAni = changeAni = 0;
                }
            }
            return;
        }

        if (changeAni == 5)
        {
            Ani.Play(hashAni[5]);
            bDeath = true;
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

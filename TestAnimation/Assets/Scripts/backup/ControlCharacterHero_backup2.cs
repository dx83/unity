using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlCharacterHero_backup2 : ControlCharacterBase
{
    public enum HeroAni { Idle = 0, Run, Dash, Attack, TakeHit, Death }
    public enum HeroState { Advance = 0, Battle }
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CurrentState = HeroState.Advance;
            currentAni = HeroAni.Run;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentState = HeroState.Battle;
            currentAni = HeroAni.TakeHit;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CurrentState = HeroState.Battle;
            currentAni = HeroAni.Death;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentAni = HeroAni.Idle;
            changeAni = 0;
            bDeath = false;
        }
        
        AnimationClipPlayCheck();
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


    HeroAni currentAni;
    void StateProcess()
    {
        switch (CurrentState)
        {
            case HeroState.Advance:
                {
                    if (isCollided)
                    {
                        CollideProcess();
                    }
                    else//if (CurrentState != HeroState.Battle)
                    {
                        AdvanceProcess();
                    }

                    break;
                }

            case HeroState.Battle:
                {
                    rgBody.velocity = Vector2.zero;

                    if (collideObj != CollideObj.monster)
                    {
                        CurrentState = HeroState.Advance;
                    }
                    else
                    {
                        BattleProcess();
                    }
                    break;
                }
        }
    }

    void AdvanceProcess()
    {
        if (currentAni != HeroAni.Idle ||
            currentAni != HeroAni.Run ||
            currentAni != HeroAni.Dash)
            currentAni = HeroAni.Run;

        switch (currentAni)
        {
            case HeroAni.Idle:
                rgBody.velocity = Vector2.zero;
                PlayAnimation(HeroAni.Idle);
                break;
            case HeroAni.Run:
                rgBody.velocity = moveSpeed * new Vector2(1, 0);
                PlayAnimation(HeroAni.Run);
                break;
            case HeroAni.Dash:
                rgBody.velocity = moveSpeed * new Vector2(5, 0);
                PlayAnimation(HeroAni.Dash);
                break;
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

    void BattleProcess()
    {
        if (currentAni != HeroAni.Attack ||
            currentAni != HeroAni.TakeHit ||
            currentAni != HeroAni.Death)
            currentAni = HeroAni.Attack;

        switch (currentAni)
        {
            case HeroAni.Attack:
                rgBody.velocity = Vector2.zero;
                PlayAnimation(currentAni);
                break;
            case HeroAni.TakeHit:
                rgBody.velocity = moveSpeed * new Vector2(-50, 0);
                PlayAnimationInstantly(currentAni);
                break;
            case HeroAni.Death:
                rgBody.velocity = Vector2.zero;
                PlayAnimationInstantly(currentAni);
                break;
        }
    }


    public void PlayAnimation(HeroAni Ani)
    {
        prevAni = changeAni;
        changeAni = (int)Ani;
    }
    public void PlayAnimationInstantly(HeroAni ani)
    {
        if (bDeath == false)
        {
            CurrentState = HeroState.Advance;
            Ani.Play(hashAni[(int)ani]);
            bTakeHit = (HeroAni.TakeHit == ani);
        }
        else
        {
            Ani.Play(hashAni[(int)ani]);
            bDeath = (HeroAni.Death == ani);
        }
    }

    bool bTakeHit = false, bDeath = false;
    int prevAni = 0, changeAni = 0;
    public void AnimationClipPlayCheck()
    {
        if (bTakeHit)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Ani.Play(hashAni[prevAni]);
                bTakeHit = false;
                return;
            }
            else if (changeAni != 5)
                return;
        }
        if (bDeath)
        {
            if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Ani.Play(hashAni[prevAni]);
                    bDeath = false;
                }
            }
            return;
        }

        Ani.Play(hashAni[changeAni]);
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

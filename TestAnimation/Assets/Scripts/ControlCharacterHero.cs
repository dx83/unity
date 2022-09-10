using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlCharacterHero : ControlCharacterBase
{
    public enum HeroAni { Idle, Run, Dash, Attack, TakeHit, Death }
    public enum CollideObj { Monster = 0, Skill, Item, None }

    public float moveSpeed;

    private Rigidbody2D rgBody;

    bool isCollided;
    CollideObj collideObj;

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
        
        collideObj = CollideObj.None;
        isCollided = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StageManager.changeState = InGameState.Start;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StageManager.changeState = InGameState.TakeHit;
            knockBackDistance = -10.0f;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(IdleDelayCoroutine(2.0f, InGameState.Run));
        }

        StateProcess();
    }


    void StateProcess()
    {
        // Auto Exit / check
        if (StageManager.inGameState == StageManager.changeState)
        {
            switch (StageManager.inGameState)
            {
                case InGameState.Start:
                    if (rgBody.position.x > -0.8)
                    {
                        rgBody.velocity = Vector2.zero;
                        rgBody.position = new Vector2(-0.8f, 0.0f);
                        StartCoroutine(IdleDelayCoroutine(1.5f, InGameState.Run));
                    }
                    break;

                case InGameState.End:
                    break;

                case InGameState.Run:
                    CollideProcess();
                    break;

                case InGameState.Attack:
                    CollideProcess();
                    break;

                case InGameState.TakeHit:
                    if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        StageManager.changeState = InGameState.Idle;
                    break;

                case InGameState.Idle:
                    CollideProcess();
                    break;

                case InGameState.Death:
                    break;
            }
            return;
        }

        //state Enter
        switch (StageManager.changeState)
        {
            case InGameState.Start:
                Ani.Play(hashAni[(int)HeroAni.Dash]);
                rgBody.velocity = moveSpeed * new Vector2(3.0f, 0.0f);
                break;

            case InGameState.End:
                // º¸½º»ç¸Á/¿µ¿õ»ç¸Á½Ã Idle·Î º¸³»°í delayTime°ú toGameState ³Ö±â
                Ani.Play(hashAni[(int)HeroAni.Dash]);
                rgBody.velocity = moveSpeed * new Vector2(3.0f, 0.0f);
                break;

            case InGameState.Run:
                Ani.Play(hashAni[(int)HeroAni.Run]);
                rgBody.velocity = moveSpeed * new Vector2(1.0f, 0.0f);
                break;

            case InGameState.Attack:
                rgBody.velocity = Vector2.zero;
                Ani.Play(hashAni[(int)HeroAni.Attack]);
                break;

            case InGameState.TakeHit:
                Ani.Play(hashAni[(int)HeroAni.TakeHit]);
                break;

            case InGameState.Idle:
                rgBody.velocity = Vector2.zero;
                Ani.Play(hashAni[(int)HeroAni.Idle]);
                break;

            case InGameState.Death:
                rgBody.velocity = Vector2.zero;
                Ani.Play(hashAni[(int)HeroAni.Death]);
                break;
        }
        StageManager.inGameState = StageManager.changeState;
    }

    bool isIdle;
    public IEnumerator IdleDelayCoroutine(float delayTime, InGameState toGameState)
    {
        isIdle = true;
        StageManager.changeState = InGameState.Idle;
        yield return WaitSec(delayTime);
        isIdle = false;
        StageManager.changeState = toGameState;
    }

    void CollideProcess()
    {
        if (isCollided)
        {
            switch (collideObj)
            {
                case CollideObj.Monster:
                    StageManager.changeState = InGameState.Attack;
                    break;

                case CollideObj.Skill:
                    break;

                case CollideObj.Item:
                    break;
            }
        }
        else if (isIdle == false)
            StageManager.changeState = InGameState.Run;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
        if (other.tag == "Monster")
            collideObj = CollideObj.Monster;
        else if (other.tag == "Skill")
            collideObj = CollideObj.Skill;
        else if (other.tag == "Item")
            collideObj = CollideObj.Item;
    }
    void OnTriggerExit2D(Collider2D other)
    { 
        isCollided = false;
        collideObj = CollideObj.None;
    }

    public void AnimationClipDeathEffect()
    {
        Color red = new Color(1.0f, 0.0f, 0.0f);

        if (Sprite.color == red)
            Sprite.color = new Color(1.0f, 1.0f, 1.0f);
        else
            Sprite.color = red;
    }

    // TakeHit - KnockBack
    public static float knockBackDistance = 0.0f;// PlayData
    public void AnimationClipKnockBackStart()
    {
        StartCoroutine(KnockbackProcess());
    }

    public IEnumerator KnockbackProcess()
    {
        rgBody.velocity = moveSpeed * new Vector2(knockBackDistance, 0.0f);
        yield return WaitSec(0.1f);
        rgBody.velocity = Vector2.zero;
    }
    //===========================================================

    public void HittingToEnemyEvent()
    {
        StageManager.hitting = true;
    }
}

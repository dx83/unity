using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlCharacterHero : ControlCharacterBase
{
    public enum HeroAni { Idle, Run, Dash, Attack, TakeHit, Death }
    public enum CollideObj { Monster = 0, Skill, Item, Stop, None }

    private Rigidbody2D rgBody;

    public Transform stagePos;

    bool isCollided;
    CollideObj collideObj;

    bool isIdle;            // Stage start/end, stun effect
    bool flagAttack;        // Idle After Attack

    void Start()
    {
        injectionObj.Inject(this);

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
        isIdle = false;

        pd.inGameState = InGameState.None;
        pd.changeState = InGameState.Start;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(IdleDelayCoroutine(1.0f, InGameState.End));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pd.changeState = InGameState.TakeHit;
            pd.knockBackDistance = -6.0f;
        }

        StateProcess();
        pd.heroPosX = this.transform.position.x;
    }

    void StateProcess()
    {
        // Auto Exit / check
        if (pd.inGameState == pd.changeState)
        {
            switch (pd.inGameState)
            {
                case InGameState.Start:
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, stagePos.position, 0.05f);
                    CollideProcess();
                    break;

                case InGameState.End:
                    transform.position = Vector3.MoveTowards(gameObject.transform.position, stagePos.position, 0.05f);
                    CollideProcess();
                    break;

                case InGameState.Run:
                    CollideProcess();
                    break;

                case InGameState.Attack:
                    CollideProcess();
                    if (collideObj == CollideObj.None && flagAttack &&
                        Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        flagAttack = false;
                        StartCoroutine(IdleDelayCoroutine(0.5f, InGameState.Run));
                    }
                    break;

                case InGameState.TakeHit:
                    if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                        pd.changeState = InGameState.Idle;
                    break;

                case InGameState.Idle:
                    if (isIdle == false)
                        pd.changeState = InGameState.Run;
                    break;

                case InGameState.Death:
                    break;
            }
            return;
        }

        //state Enter
        switch (pd.changeState)
        {
            case InGameState.Start:
                Ani.Play(hashAni[(int)HeroAni.Dash]);
                break;

            case InGameState.End:
                stagePos.gameObject.SetActive(true);
                stagePos.position = new Vector2(pd.stageEndPosX + Sprite.size.x, stagePos.position.y);
                Ani.Play(hashAni[(int)HeroAni.Dash]);
                break;

            case InGameState.Run:
                Ani.Play(hashAni[(int)HeroAni.Run]);
                rgBody.velocity = ud.heroSpeed * new Vector2(1.0f, 0.0f);
                break;

            case InGameState.Attack:
                flagAttack = true;
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
        pd.inGameState = pd.changeState;
    }

    public IEnumerator IdleDelayCoroutine(float delayTime, InGameState toGameState)
    {
        isIdle = true;
        pd.changeState = InGameState.Idle;
        yield return WaitSec(delayTime);
        isIdle = false;
        pd.changeState = toGameState;
    }

    void CollideProcess()
    {
        if (isCollided)
        {
            switch (collideObj)
            {
                case CollideObj.Monster:
                    pd.changeState = InGameState.Attack;
                    break;

                case CollideObj.Skill:
                    break;

                case CollideObj.Item:
                    break;

                case CollideObj.Stop:
                    if (pd.inGameState == InGameState.Start &&
                        rgBody.position.x >= stagePos.position.x)
                    {
                        StartCoroutine(IdleDelayCoroutine(1.0f, InGameState.Run));
                        stagePos.gameObject.SetActive(false);
                    }
                    else if (pd.inGameState == InGameState.End &&
                        rgBody.position.x >= stagePos.position.x)
                    {
                        //pd.inGameState = InGameState.None;
                        //pd.changeState = InGameState.None;
                        Ani.Play(hashAni[(int)HeroAni.Idle]);
                    }
                    break;
            }
        }
    }


    ControlCharacterMonster monster;
    void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
        if (other.tag == "Monster")
        {
            collideObj = CollideObj.Monster;
            monster = other.GetComponent<ControlCharacterMonster>();
            // ???????? ?????????? ?????? ???? ?????????? Hurt?? ?????? ?? ??????...
        }
        else if (other.tag == "Skill")
            collideObj = CollideObj.Skill;
        else if (other.tag == "Item")
            collideObj = CollideObj.Item;
        else if (other.tag == "Stop")
            collideObj = CollideObj.Stop;
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
    public void AnimationClipKnockBackStart()
    {
        StartCoroutine(KnockbackProcess());
    }

    public IEnumerator KnockbackProcess()
    {
        rgBody.velocity = ud.heroSpeed * new Vector2(pd.knockBackDistance, 0.0f);
        yield return WaitSec(0.1f);
        rgBody.velocity = Vector2.zero;
    }
    //===========================================================

    public void HittingToEnemyEvent()
    {
        if (collideObj == CollideObj.Monster)
            StartCoroutine(monster.Hurt());
    }
}

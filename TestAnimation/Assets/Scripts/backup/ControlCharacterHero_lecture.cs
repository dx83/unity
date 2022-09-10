using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 영웅은 트랜지션 사용안함 / 몬스터는 트랜지션 사용
// 클래스/프로퍼티를 사용하여 상태 진입 / 상태 퇴장 설정

// disable로 지연시키는 방법(스테이지 첫등장)
public class ControlCharacterHero_lecture : ControlCharacterBase
{
    public enum HeroAni { Run, Dash, Attack, TakeHit, Death, Idle }
    public enum HeroState { Advance = 0, Battle }
    public enum CollideObj { monster = 0, skill, item, stageEnd }



    HeroState _HeroState;

    public HeroState CurrentState
    {
        get
        {
            return _HeroState;

        }
        set
        {
            if (_HeroState == value)
            {
                return;
            }
            //state exit
            switch (_HeroState)
            {
                case HeroState.Advance:
                    Ani.SetBool(hashAni[(int)HeroAni.Run], false);
                    break;
                case HeroState.Battle:
                    break;
            }


            //state enter
            switch (value)
            {
                case HeroState.Advance:
                    Ani.SetBool(hashAni[(int)HeroAni.Run], true);
                    break;
                case HeroState.Battle:
                   
                    break;
            }

            _HeroState = value;
        }
    }

    public float moveSpeed;

    private Rigidbody2D rgBody;

    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        hashAni = new int[5];
        hashAni[0] = Animator.StringToHash("hero_run");
        hashAni[1] = Animator.StringToHash("hero_dash");
        hashAni[2] = Animator.StringToHash("hero_attack");
        hashAni[3] = Animator.StringToHash("hero_takeHit");
        hashAni[4] = Animator.StringToHash("hero_death");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Ani.GetBool(hashAni[(int)HeroAni.Run]))
                Ani.SetBool(hashAni[(int)HeroAni.Run], false);
            else
                Ani.SetBool(hashAni[(int)HeroAni.Run], true);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Ani.GetBool(hashAni[(int)HeroAni.Attack]))
                Ani.SetBool(hashAni[(int)HeroAni.Attack], false);
            else
                Ani.SetBool(hashAni[(int)HeroAni.Attack], true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Ani.SetTrigger(hashAni[(int)HeroAni.TakeHit]);
            //Ani.SetBool(hashAni[(int)HeroAni.TakeHit], true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Ani.SetTrigger(hashAni[(int)HeroAni.Death]);
        }
        //StateProcess();
#if AAA
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
#endif 
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




    HeroAni currentAni = HeroAni.Idle;
    void StateProcess()
    {
        switch (CurrentState)
        {
            case HeroState.Advance:
                {
                    Ani.SetBool(hashAni[(int)HeroAni.Run], true);
                    AdvanceProcess();
                    if (isCollided)
                    {
                        CollideProcess();
                    }
                    break;
                }

            case HeroState.Battle:
                {
          

                    rgBody.velocity = Vector2.zero;

                    BattleProcess();
                    if (collideObj != CollideObj.monster)
                    {
                        CurrentState = HeroState.Advance;
                    }
                    break;
                }
        }
    }

    void AdvanceProcess()
    {
        switch (currentAni)
        {
            case HeroAni.Idle:
                rgBody.velocity = Vector2.zero;
                Ani.SetBool(hashAni[(int)HeroAni.Run], false);
                Ani.SetBool(hashAni[(int)HeroAni.Dash], false);
                Ani.SetBool(hashAni[(int)HeroAni.Attack], false);
                break;
            case HeroAni.Run:
                rgBody.velocity = moveSpeed * new Vector2(1, 0);
                PlayAnimation(currentAni);
                break;
            case HeroAni.Dash:
                rgBody.velocity = moveSpeed * new Vector2(5, 0);
                PlayAnimation(currentAni);
                break;
        }
    }

    void CollideProcess()
    {
        switch (collideObj)
        {
            case CollideObj.monster:
                CurrentState = HeroState.Battle;
                currentAni = HeroAni.Attack;
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
        rgBody.velocity = Vector2.zero;
        switch (currentAni)
        {
            case HeroAni.Attack:
                break;
            case HeroAni.TakeHit:
                break;
            case HeroAni.Death:
                break;
        }
        PlayAnimation(currentAni);
    }


    public void PlayAnimation(HeroAni ani)
    {
        //prevAni = changeAni;
        //changeAni = (int)Ani;
        if ((int)ani < 3)
            Ani.SetBool(hashAni[(int)ani], true);
        else
            Ani.SetTrigger(hashAni[(int)ani]);
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

    public void AnimationClipKnockBackStart()
    {
        StartCoroutine(KnockbackProcess());
     //   Ani.SetBool(hashAni[(int)HeroAni.Attack], false);
       // rgBody.velocity = moveSpeed * new Vector2(-5.0f, 0.0f);
    }

    WaitForSeconds knockbackTime= new WaitForSeconds(0.1f);

    //IEnumerator WaitSec(float waitSec)
    //{
    //    float t = 0;
    //    while (t < waitSec)
    //    {
    //        yield return null;
    //        t += Time.deltaTime;
    //    }
    //
    //}




    public IEnumerator KnockbackProcess()
    {
        Ani.SetBool(hashAni[(int)HeroAni.Attack], false);
        rgBody.velocity = moveSpeed * new Vector2(-5.0f, 0.0f);


        yield return WaitSec(0.1f);


        rgBody.velocity = Vector2.zero;
        //Ani.SetBool(hashAni[(int)HeroAni.TakeHit], false);
    }


    public void AnimationClipKnockBackEnd()
    {
        rgBody.velocity = Vector2.zero;
        Ani.SetBool(hashAni[(int)HeroAni.TakeHit], false);
    }
}

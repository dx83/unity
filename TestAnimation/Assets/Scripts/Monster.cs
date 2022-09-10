using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : ControlCharacterBase
{
    public enum MonsterAni { Idle = 0, Attack, Hurt, Death }
    public enum MonsterState { Idle = 0, Attack, Hurt, Death }

    public MonsterState monsterState, changeState;

    bool isCollided;

    void Start()
    {
        hashAni = new int[4];
        hashAni[0] = Animator.StringToHash("Lizard_Idle");
        hashAni[1] = Animator.StringToHash("Lizard_Attack");
        hashAni[2] = Animator.StringToHash("Lizard_Hurt");
        hashAni[3] = Animator.StringToHash("Lizard_Death");

        delayTime = 3.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hashAni[0] = Animator.StringToHash("Medusa_Idle");
            hashAni[1] = Animator.StringToHash("Medusa_Attack");
            hashAni[2] = Animator.StringToHash("Medusa_Hurt");
            hashAni[3] = Animator.StringToHash("Medusa_Death");
        }
        StateCheck();
    }


    void StateCheck()
    {
        if (isCollided)
        {
            if (monsterState == changeState)
            {
                switch (monsterState)
                {
                    case MonsterState.Idle:
                        if (StageManager.hitting)
                            changeState = MonsterState.Hurt;
                        MonsterAttackTime();
                        break;

                    case MonsterState.Attack:
                        if (StageManager.changeState != InGameState.TakeHit &&
                            Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                        {
                            StageManager.hitting = false;
                            StageManager.changeState = InGameState.TakeHit;
                            ControlCharacterHero.knockBackDistance = 0.0f;
                        }

                        if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                            changeState = MonsterState.Idle;
                        break;

                    case MonsterState.Hurt:
                        MonsterAttackTime();
                        break;

                    case MonsterState.Death:
                        break;
                }
                return;
            }

            switch (changeState)
            {
                case MonsterState.Idle:
                    Ani.Play(hashAni[(int)MonsterAni.Idle]);
                    break;

                case MonsterState.Attack:
                    Ani.Play(hashAni[(int)MonsterAni.Attack]);
                    break;

                case MonsterState.Hurt:
                    Ani.Play(hashAni[(int)MonsterAni.Hurt]);
                    break;

                case MonsterState.Death:
                    Ani.Play(hashAni[(int)MonsterAni.Death]);
                    break;
            }

            monsterState = changeState;
        }
        else if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Ani.Play(hashAni[(int)MonsterAni.Idle]);
            monsterState = changeState = MonsterState.Idle;
            timer = 0;
        }
    }

    void MonsterAttackTime()
    {
        timer += Time.deltaTime;
        if (timer > delayTime)
        {
            timer = 0.0f;
            changeState = MonsterState.Attack;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isCollided = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollided = false;
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

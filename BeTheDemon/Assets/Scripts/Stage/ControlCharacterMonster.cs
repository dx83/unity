using System.Collections;
using UnityEngine;

public class ControlCharacterMonster : ControlCharacterBase
{
    public enum MonsterAni { Idle = 0, Attack, Hurt, Death }
    public enum MonsterState { Idle = 0, Attack, Hurt, Death }

    public MonsterState monsterState, changeState;

    bool isCollided;

    public Material colorShaderMat;
    [HideInInspector] Material defaultMat;


    public void MonsterInitialize()
    {
        injectionObj.Inject(this);

        hashAni = new int[4];
        hashAni[0] = Animator.StringToHash("Idle");
        hashAni[1] = Animator.StringToHash("Attack");
        hashAni[2] = Animator.StringToHash("Hurt");
        hashAni[3] = Animator.StringToHash("Death");

        defaultMat = this.Sprite.sharedMaterial;

        delayTime = 3.0f;
        isCollided = false;
    }

    void Update()
    {
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
                        //if (StageManager.hitting)
                        //    changeState = MonsterState.Hurt;
                        MonsterAttackTime();
                        break;

                    case MonsterState.Attack:
                        if (pd.changeState != InGameState.TakeHit &&
                            Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                        {
                            //pd.hitting = false;
                            pd.changeState = InGameState.TakeHit;
                            pd.knockBackDistance = -3.0f;
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
                    //StartCoroutine(Hurt());
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

    public IEnumerator Hurt()
    {
        this.Sprite.sharedMaterial = colorShaderMat;
        yield return WaitSec(0.1f);
        this.Sprite.sharedMaterial = defaultMat;
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

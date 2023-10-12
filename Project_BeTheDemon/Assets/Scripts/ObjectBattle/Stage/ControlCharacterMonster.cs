using System.Collections;
using UnityEngine;


public class ControlCharacterMonster : ControlCharacterBase
{
    int dummyNum;

    MonsterState monsterState, changeState;

    bool isCollided;

    public Material colorShaderMat;
    [HideInInspector] Material defaultMat;

    void Awake()
    {
        isCollided = false;
        isDeath = true;
    }

    void Update()
    {
        StateCheck();
    }

    public void InsertDummyNumber(int n) => dummyNum = n;

    public void MonsterInitialize(int hitPoint)
    {
        injectionObj.Inject(this);

        if (defaultMat == null)
            defaultMat = this.Sprite.sharedMaterial;

        monsterState = changeState = MonsterState.Idle;

        timer = 0.0f;
        this.hitPoint = hitPoint;
        isDeath = false;
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
                    Ani.Play(AniHash.MID[(int)MonsterAni.Idle]);
                    break;

                case MonsterState.Attack:
                    Ani.Play(AniHash.MID[(int)MonsterAni.Attack]);
                    break;

                case MonsterState.Hurt:
                    Ani.Play(AniHash.MID[(int)MonsterAni.Hurt]);
                    //StartCoroutine(Hurt());
                    break;

                case MonsterState.Death:
                    Ani.Play(AniHash.MID[(int)MonsterAni.Death]);
                    break;
            }

            monsterState = changeState;
        }
        else if (Ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            Ani.Play(AniHash.MID[(int)MonsterAni.Idle]);
            monsterState = changeState = MonsterState.Idle;
            timer = 0;
            this.Sprite.sharedMaterial = defaultMat;
        }
    }

    public IEnumerator Hurt()
    {
        this.Sprite.sharedMaterial = colorShaderMat;
        yield return WaitSec(0.1f);
        this.Sprite.sharedMaterial = defaultMat;
        
        if (--hitPoint == 0)
        {
            this.gameObject.SetActive(false);
            isDeath = true;
        }
    }

    void MonsterAttackTime()
    {
        if (isCollided)
        {
            timer += Time.deltaTime;
            if (timer > attackDelayTime)
            {
                timer = 0.0f;
                changeState = MonsterState.Attack;
            }
        }
        else
            timer = 0.0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isCollided = true;
    }
    void OnTriggerExit2D(Collider2D collision)
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

    public bool MonsterDeath() => isDeath;
}

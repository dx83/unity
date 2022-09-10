using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_lecture : MonoBehaviour
{
    Animator ani;
    SpriteRenderer sprite;


    Rigidbody2D body;

    public bool isCollided;

    public float moveSpeed;

    public enum State
    {
        Run,
        Combat
    }

    public enum CombatState
    {

    }

    public State CurrentState;

    int[] hashAni;

    void Start()
    {
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        //ani.runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load("Animations/Ani_Hero/Hero_Animator.controller", typeof(RuntimeAnimatorController)));

        hashAni = new int[6];
        hashAni[0] = Animator.StringToHash("Hero_Idle");
        hashAni[1] = Animator.StringToHash("Hero_Run");
        hashAni[2] = Animator.StringToHash("Hero_Dash");
        hashAni[3] = Animator.StringToHash("Hero_Attack");
        hashAni[4] = Animator.StringToHash("Hero_TakeHit");
        hashAni[5] = Animator.StringToHash("Hero_Death");
        prevAni = 0;
    }

    public enum HeroAni
    {
        Idle=0,
        Run,
        Dash,
        Attack,
        TakeHit,
        Death
    }

    public void PlayAnimation(HeroAni ani)
    {
        AnimationClipPlayLoop((int)ani);
    }


    bool IsMobInFront()
    {
        return isCollided;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isCollided = false;
    }

    void StateProcess()
    {
        switch (CurrentState)
        {
            case State.Run:
                body.velocity = moveSpeed * new Vector2(1, 0);
                PlayAnimation( HeroAni.Run);

                if (IsMobInFront())
                {
                    CurrentState = State.Combat;
                }
                break;
            case State.Combat:
                body.velocity = Vector2.zero; ;
                if (IsMobInFront()==false)
                {
                    CurrentState = State.Run;
                }

                PlayAnimation(HeroAni.Attack);
             //   CombatStateProcess();
                break;
        }
    }

    void CombatStateProcess()
    {

    }




    void Update()
    {
        StateProcess();


#if AAA
        AnimationClipPlayDeath();
        
        AnimationClipPlayTakeHit();
        if (changeAni == 5) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
            AnimationClipPlayLoop(3);
        
        if (Input.GetKeyDown(KeyCode.Return))
            changeAni = 4;
        
        if (Input.GetKeyDown(KeyCode.D))
            changeAni = 5;
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
            AnimationClipPlayLoop(1);
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            AnimationClipPlayLoop(2);
        
        if (Input.GetKeyDown(KeyCode.Escape))
            AnimationClipPlayLoop(0);
#endif
        
    }

    bool bDeath = false, bTakeHit = false;
    int prevAni = 0, changeAni = 0;

    public void AnimationClipPlayLoop(int index)    //idle run dash attack
    {
        prevAni = index;
        ani.Play(hashAni[index]);
    }
    public void AnimationClipPlayTakeHit()
    {
        if (bTakeHit)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // takeHit 중 death가 들어오면 prevAni로 돌아가지 않도록...
                if (changeAni != 5)
                    ani.Play(hashAni[prevAni]);
                bTakeHit = false;
            }
            return;
        }

        if (changeAni == 4)
        {
            ani.Play(hashAni[4]);
            bTakeHit = true;
            changeAni = 0;
        }
    }

    public void AnimationClipPlayDeath()
    {
        if (bDeath)
        {
            if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ani.Play(hashAni[0]);
                    bDeath = false;
                    prevAni = changeAni = 0;
                }
            }
            return;
        }

        if (changeAni == 5)
        {
            ani.Play(hashAni[5]);
            bDeath = true;
        }
    }

    public void AnimationClipDeathEffect()
    {
        Color red = new Color(1.0f, 0.0f, 0.0f);

        if (sprite.color == red)
            sprite.color = new Color(1.0f, 1.0f, 1.0f);
        else
            sprite.color = red;
    }
}

using System.Collections;
using UnityEngine;


public class ControlCharacterBase : MonoBehaviour
{
    [Inject] protected PlayData pd = new PlayData();
    [Inject] protected UserData ud = new UserData();
    protected InjectionObj injectionObj = new InjectionObj();

    protected float timer, attackDelayTime;
    protected int hitPoint;
    protected bool isDeath;

    private Animator cachedAni;

    protected Animator Ani
    {
        get
        {
            if (cachedAni == null)
                cachedAni = GetComponent<Animator>();
            return cachedAni;
        }
    }

    private SpriteRenderer cachedSprite;
    protected SpriteRenderer Sprite
    {
        get
        {
            if (cachedSprite == null)
                cachedSprite = GetComponent<SpriteRenderer>();
            return cachedSprite;
        }
    }

    protected IEnumerator WaitSec(float waitSec)
    {
        float t = 0;
        while (t < waitSec)
        {
            yield return null;
            t += Time.deltaTime;
        }
    }

    public void InsertAttackDelayTime(float sec)
    {
        attackDelayTime = sec;
    }
}

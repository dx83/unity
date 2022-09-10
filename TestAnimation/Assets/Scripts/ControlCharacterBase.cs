using System.Collections;
using UnityEngine;


public class ControlCharacterBase : MonoBehaviour
{
    protected int[] hashAni;

    protected float timer = 0.0f, delayTime = 0.0f;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGameState_lecture : MonoBehaviour
{
    public enum InGameState { Start = 0, End, Advance, Battle, Idle, Death }

    private InGameState inGameState, changeState;

    void Start()
    {

    
    }

    // ½¦ÀÌ´õ·Î ±ô¹ÚÀÓ ±¸Çö??
    //IEnumerator HitFlash()
    //{
    //    SpriteRenderer spRenderer=null;
    //
    //
    //    var defaultMat = spRenderer.sharedMaterial;
    //    spRenderer.sharedMaterial = flashMat ;
    //    yield return new WaitForSeconds(0.2f);
    //
    //    spRenderer.sharedMaterial = defaultMat;
    //}


    void Update()
    {
        StateProcess();
    }

    void StateProcess()
    {
        switch (inGameState)
        {
            case InGameState.Start:
                
                break;
            case InGameState.End:
                break;
            case InGameState.Advance:
                break;
            case InGameState.Battle:
                break;
            case InGameState.Idle:
                break;
            case InGameState.Death:
                break;
        }

        if (inGameState == changeState) return;
    }
}

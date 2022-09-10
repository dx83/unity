using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    // Play Data에 있을 변수
    public static InGameState inGameState, changeState;
    public static bool hitting;

    void Awake()
    {
        inGameState = InGameState.None;
        changeState = InGameState.None;
        hitting = false;
    }

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
            case InGameState.Run:
                break;
            case InGameState.Attack:
                break;
            case InGameState.TakeHit:
                break;
            case InGameState.Idle:
                break;
            case InGameState.Death:
                break;
            case InGameState.Restart:
                break;
        }
    }
}

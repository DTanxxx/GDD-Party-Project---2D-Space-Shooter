using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Fire,
    Idle,
    Dash
}

public class BossStateMachine : MonoBehaviour
{
    private BossState state = BossState.Fire;

    private void Update()
    {
        switch (state)
        {
            case BossState.Fire:
                break;

            default:
                break;
        }
    }

    public BossState GetCurrentState()
    {
        return state;
    }
}

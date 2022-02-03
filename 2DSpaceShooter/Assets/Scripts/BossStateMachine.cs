using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the BossStateMachine which is attached to the Boss game object.
/// </summary>
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
        // We will introduce state transitioning once more states are added.
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

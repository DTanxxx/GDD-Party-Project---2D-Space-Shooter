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
    [SerializeField] private float timeForFireState = 10.0f;
    [SerializeField] private float timeForIdleState = 2.0f;
    [SerializeField] private float timeForDashState = 1.0f;
    [SerializeField] private float dashSpeed = 1.0f;

    private BossState state = BossState.Fire;
    private float timerToSwitchState = 0.0f;

    private void Start()
    {
        timerToSwitchState = timeForFireState;
    }

    private void Update()
    {
        timerToSwitchState -= Time.deltaTime;
        if (timerToSwitchState > 0.0f) { return; }
        switch (state)
        {
            case BossState.Fire:
                Debug.Log("Switch to idle state");
                state = BossState.Idle;
                timerToSwitchState = timeForIdleState;
                break;

            case BossState.Idle:
                Debug.Log("Switch to dash state");
                state = BossState.Dash;
                timerToSwitchState = timeForDashState;
                break;

            case BossState.Dash:
                Debug.Log("Switch to fire state");
                state = BossState.Fire;
                timerToSwitchState = timeForFireState;
                break;

            default:
                break;
        }
    }

    public BossState GetCurrentState()
    {
        return state;
    }

    public float GetDashSpeed()
    {
        return dashSpeed;
    }
}

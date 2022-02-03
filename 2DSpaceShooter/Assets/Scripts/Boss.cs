using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains extra Boss logic.
/// </summary>
public class Boss : Enemy
{
    private BossStateMachine stateMachine;

    override protected void Start()
    {
        base.Start();
        stateMachine = GetComponent<BossStateMachine>();
    }

    override protected void Update()
    {
        // Unlike Enemy, the Boss should fire only if its current state is Fire (which is also our only state so far).
        if (stateMachine.GetCurrentState() == BossState.Fire)
        {
            // Deduct firingCountDown.
            firingCountDown -= Time.deltaTime;
            // If count down goes below 0, call Fire() and reset count down.
            if (firingCountDown <= 0.0f)
            {
                // Call Fire() from the parent class.
                Fire(-1, 1);
                firingCountDown = Random.Range(minFireInterval, maxFireInterval);
            }
        }
    }
}

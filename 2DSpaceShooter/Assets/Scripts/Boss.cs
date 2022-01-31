using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

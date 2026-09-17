using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounetrAttackState : PlayerState
{
    public PlayerCounetrAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack",false);
    }


    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.attackRadius);

        foreach (var hit in colliders)
        {
            var enemy = hit.GetComponent<Enemy>();
            if ( enemy!= null)
            {
                if (enemy.CanBeStunned())
                {
                    stateTimer = 10f; // any value big than 1
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }

        if (stateTimer <= 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

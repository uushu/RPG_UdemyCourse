using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState :PlayerGroundState
{
    
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.zeroVelocity();
    }

    public override void Update()
    {
        base.Update();
        if(xInput!=0 && !player.isBusy)
            stateMachine.ChangeState(player.walkState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

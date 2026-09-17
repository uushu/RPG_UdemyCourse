using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalkState : SkeletonGroundState
{
    public SkeletonWalkState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animBoolName, _skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        
    }
    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(skeleton.moveSpeed*skeleton.facingDir,skeleton.rb.velocity.y);
        if (skeleton.IsWallDetected() || !skeleton.IsGroundDetected())
        {
            skeleton.Flip();
            StateMachine.ChangeState(skeleton.idleState);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animBoolName, _skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
            StateMachine.ChangeState(skeleton.walkState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.stunnedDuration;
        skeleton.fx.InvokeRepeating("RedColorBlink",0,0.1f);
        skeleton.rb.velocity=new Vector2(-skeleton.facingDir*skeleton.stunnedDirection.x,skeleton.stunnedDirection.y);
    }


    public override void Exit()
    {
        base.Exit();
        skeleton.fx.Invoke("CancelRedBlink",0);
    }
    public override void Update()
    {
        base.Update();
        if(stateTimer<=0)
            StateMachine.ChangeState(skeleton.idleState);
        
    }
}

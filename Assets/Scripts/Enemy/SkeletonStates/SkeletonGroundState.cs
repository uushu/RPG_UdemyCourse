using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    
    protected Enemy_Skeleton skeleton;
    private Transform player;
    public SkeletonGroundState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName,Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, _animBoolName)
    {
        this.skeleton=_skeleton;
    }
   

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (skeleton.IsPlayerDetected() || Vector2.Distance(skeleton.transform.position,player.position) < skeleton.playerCheckDistance )
        {
            StateMachine.ChangeState(skeleton.battleState);
        }
    }
}

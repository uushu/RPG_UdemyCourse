using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton skeleton;
    
    public SkeletonAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        skeleton = _skeleton;
    }

    

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastAttackTime = Time.time;
    }
    
    public override void Update()
    {
        base.Update();
        skeleton.SetZeroVelocity();
        
        if(triggerCalled)
            StateMachine.ChangeState(skeleton.battleState);
    }
    
   
}

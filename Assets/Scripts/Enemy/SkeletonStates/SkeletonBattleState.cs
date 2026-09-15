using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton skeleton;
    private int moveDir;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName ,Enemy_Skeleton _skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = _skeleton;

    }

    public override void Enter()
    {
        base.Enter();
        player= GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (skeleton.IsPlayerDetected())
        {
            stateTimer = skeleton.battleTime;
            if (skeleton.IsPlayerDetected().distance <= skeleton.attackDistance)
            {
                if(CanAttack())
                    StateMachine.ChangeState(skeleton.attackState);
            }
            
        }
        else
        {
            if( stateTimer <= 0 || Vector2.Distance(skeleton.transform.position,player.position) > 7)
                StateMachine.ChangeState(skeleton.idleState);
        }
        
        
        if (skeleton.transform.position.x < player.position.x)
            moveDir = 1;
        else if (skeleton.transform.position.x > player.position.x)
            moveDir = -1;
        
        skeleton.SetVelocity(skeleton.moveSpeed*moveDir*1.5f,skeleton.rb.velocity.y);

    }

    public bool CanAttack()
    {
        if (Time.time >= skeleton.lastAttackTime + skeleton.attackCooldown)
        {
            return true;
        }
       
        return false;
    }
}

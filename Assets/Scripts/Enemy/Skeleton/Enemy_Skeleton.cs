using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_Skeleton : Enemy
{
    
    #region States
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonWalkState walkState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        walkState = new SkeletonWalkState(this, stateMachine, "Walk", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Walk",this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack",this);
        stunnedState =   new SkeletonStunnedState(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
<<<<<<< HEAD:Assets/Scripts/Enemy/Enemy_Skeleton.cs
=======
        
        if(Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(stunnedState);
        
>>>>>>> 8304eb08c271afbdc072ba49e6058888a69e708b:Assets/Scripts/Enemy/Skeleton/Enemy_Skeleton.cs
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }
<<<<<<< HEAD:Assets/Scripts/Enemy/Enemy_Skeleton.cs
    

=======
>>>>>>> 8304eb08c271afbdc072ba49e6058888a69e708b:Assets/Scripts/Enemy/Skeleton/Enemy_Skeleton.cs
}

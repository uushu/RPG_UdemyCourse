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
    public SkeletonHitState    hitState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        walkState = new SkeletonWalkState(this, stateMachine, "Walk", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Walk",this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack",this);
        hitState =   new SkeletonHitState(this, stateMachine, "Hit", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        
        if(Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(hitState);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private float lastTimerAttacked;
    private float comboWindow = 2f;
    private int comboCounter = 0;
    
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter>2 || Time.time>lastTimerAttacked+comboWindow)
            comboCounter = 0;
        
        player.anim.SetInteger("ComboCounter", comboCounter);

        #region Choose Attack Direction

        float attackDir = player.facingDir;
        if(xInput!=0)
            attackDir = xInput;

        #endregion

        player.SetVelocity(player.attackMovements[comboCounter].x * attackDir, player.attackMovements[comboCounter].y);
        
        stateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            player.zeroVelocity();
        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        comboCounter++;
        lastTimerAttacked=Time.time;
        
    }
}

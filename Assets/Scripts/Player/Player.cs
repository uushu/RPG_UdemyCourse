
using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
<<<<<<< HEAD
    [Header(("Attack Info"))]
=======
    [Header(("Attack Info"))] 
>>>>>>> 8304eb08c271afbdc072ba49e6058888a69e708b
    public float counterAttackDuration;
    public Vector2[] attackMovements;
    public bool isBusy { get; private set; }
    
    
    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;
    
    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    [SerializeField]private float dashCooldown;
    private float dashcoolTimer;
    public float dashDir { get; private set; }
    
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    
    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkState walkState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState  airState  { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState  wallJumpState  { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
<<<<<<< HEAD
    public PlayerCounterAttackState counterAttackState { get; private set; }
=======
    public PlayerCounetrAttackState counterAttackState { get; private set; }
>>>>>>> 8304eb08c271afbdc072ba49e6058888a69e708b
    #endregion

   

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        walkState = new PlayerWalkState(this, stateMachine, "Walk");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
<<<<<<< HEAD
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        
        
=======
        counterAttackState = new PlayerCounetrAttackState(this, stateMachine, "CounterAttack");


>>>>>>> 8304eb08c271afbdc072ba49e6058888a69e708b
    }

    
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        GetDashInput();
    }

    public void GetDashInput()
    {
        if (IsWallDetected()) return;
        dashcoolTimer-= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashcoolTimer <= 0)
        {
            dashcoolTimer = dashCooldown;
            dashDir=Input.GetAxisRaw("Horizontal");
            if(dashDir==0)
                dashDir=facingDir;
            stateMachine.ChangeState(dashState);
        }
            
    }
    
    

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy= true;
        
        yield return new WaitForSeconds(_seconds);
        
        isBusy = false;
    }
}

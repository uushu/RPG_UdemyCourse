
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header(("Attack Info"))]
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

    public float facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [Space]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    
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
    #endregion

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        walkState = new PlayerWalkState(this, stateMachine, "Walk");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");

        anim=GetComponentInChildren<Animator>();
        rb=GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
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
    
    #region Velocity
    public void zeroVelocity() => rb.velocity = Vector2.zero;
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Flip

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void FlipController(float _xVelocity)
    {
        if(_xVelocity > 0 && !facingRight)
            Flip();
        else if(_xVelocity < 0 && facingRight)
            Flip();
    }


    #endregion

    #region Collision

    public bool IsWallDetected() =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    public bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+wallCheckDistance, wallCheck.position.y));
    }

    #endregion
    

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy= true;
        
        yield return new WaitForSeconds(_seconds);
        
        isBusy = false;
    }
}

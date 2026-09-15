using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    public EntityFX fx { get; private set; }
    public  Animator anim { get; private set; }
    public  Rigidbody2D rb { get; private set; }
    #endregion
    
    #region Collisions
    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackRadius;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [Space]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    #endregion
    
    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;
    
    public float facingDir { get; private set; } = 1;
    private bool facingRight = true;

    protected virtual void Awake()
    {
        fx = GetComponentInChildren<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name+" was damaged!");
    }

    public virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * (-facingDir), knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }
    
    
    #region Velocity
    public void SetZeroVelocity()
    {
        if (isKnocked) return;
        rb.velocity = Vector2.zero;
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if(isKnocked) return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Flip

    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public virtual void FlipController(float _xVelocity)
    {
        if(_xVelocity > 0 && !facingRight)
            Flip();
        else if(_xVelocity < 0 && facingRight)
            Flip();
    }


    #endregion

    #region Collision

    public virtual bool IsWallDetected() =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    public virtual bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere( attackCheck.position,attackRadius);
    }

    #endregion

    
}

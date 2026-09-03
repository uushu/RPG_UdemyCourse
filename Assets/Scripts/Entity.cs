using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    
    protected float facingDir = 1f;
    protected bool facingRight = true;
    
    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [Space]
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayer;
    protected bool isGrounded ;
    protected bool isTouchingWall;
    
    protected virtual void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponentInChildren<Animator>();
        
        if (wallCheck == null)
            wallCheck = transform;
    }

    protected virtual void Update()
    {
        CollisionChecks();
    }
    
    protected virtual void CollisionChecks()
    {
        isGrounded=Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,groundLayer);
        isTouchingWall=Physics2D.Raycast(wallCheck.position,Vector2.right*facingDir,wallCheckDistance,groundLayer);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+wallCheckDistance*facingDir,wallCheck.position.y));
    }
    
    protected virtual void Flip()
    {
        facingDir=facingDir*-1;
        facingRight=!facingRight;
        transform.Rotate(0f,180f,0f);
    }
}

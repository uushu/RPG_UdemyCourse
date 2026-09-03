using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_Skeleton : Entity
{
    [Header("Move Info")]
    [SerializeField] private float moveSpeed ;
    
    [Header("PlayerDetection Info")] 
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask playerLayer;
    private RaycastHit2D isPlayerDetected;

    private bool isAttacking;
    
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isPlayerDetected)
        {
            if(isPlayerDetected.distance > 1)
            {
                isAttacking = false;
            }
            else
            {
                isAttacking = true;
                //Debug.Log("Attacking "+isPlayerDetected.collider.gameObject.name);
            }
        }
        if(isTouchingWall)
        {
            Flip();
        }
        
        //Movement();
        
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right , playerCheckDistance* facingDir, playerLayer);
        
    }

    private void Movement()
    {
        if(!isAttacking)
        {
            rb.velocity=new Vector2(facingDir*moveSpeed,rb.velocity.y);
        }
    }
    
    
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x+playerCheckDistance*facingDir,transform.position.y));
    }
}

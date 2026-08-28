
using UnityEngine;

public class Player : MonoBehaviour
{
    private float fps = 0f;
    private float timer = 0f;
    private int frameCount = 0;
    
    private float xInput = 0f;
    private float facingDir = 1f;
    private bool facingRight = true;
    
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float jumpForce = 5f;

    [Header("Dash Info")] 
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTimer;
    
    [SerializeField] private float dashCooldown;
    private float dashCoolTimer;
    
    

    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded ;
    
    
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        //模块化
        ShowFPS();
        
        CollisionChecks();
        CheckInput();
       
        dashCoolTimer-=Time.deltaTime;
        dashTimer-=Time.deltaTime;
        
        Movement();
        AnimatorController();
        FlipController();
        
    }
    private void CheckInput()
    {
        xInput= Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    private void Jump()
    {
        if(isGrounded)
            rb.velocity=new Vector2(rb.velocity.x,jumpForce);
    }

    private void DashAbility()
    {
        if (dashCoolTimer < 0)
        {
            dashCoolTimer = dashCooldown;
            dashTimer = dashDuration;
        }
    }

    private void CollisionChecks()
    {
        isGrounded=Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y-groundCheckDistance));
    }

    private void Flip()
    {
        facingDir=facingDir*-1;
        facingRight=!facingRight;
        transform.Rotate(0f,180f,0f);
    }
    
    private void FlipController()
    {
        if(facingRight && xInput<0)
        {
            Flip();
        }
        else if(!facingRight && xInput>0)
        {
            Flip();
        }
    }

    

    private void Movement()
    {
        if (dashTimer > 0)
            rb.velocity=new Vector2(dashSpeed * facingDir,0);
        else
            rb.velocity=new Vector2(moveSpeed * xInput,rb.velocity.y);
    }

    private void ShowFPS()
    {
        frameCount++;
        timer+= Time.deltaTime;
        if (timer >= 1f)
        {
            fps = frameCount/ timer; // 平均 FPS
            frameCount = 0;
            timer = 0f;
        }
    }

    

    void AnimatorController()
    {
        bool isMoving=rb.velocity.x!=0;
        animator.SetFloat("yVelocity",rb.velocity.y);
        animator.SetBool("isMoving",isMoving);
        animator.SetBool("isGrounded",isGrounded);
        animator.SetBool("isDashing",dashTimer>0);
        
    }

    void OnGUI()
    {
        // UI样式和渲染必须放在外面，保证每帧绘制
        GUIStyle style = new GUIStyle();
        style.fontSize = 36;
        style.normal.textColor= Color.red;
        // 持续在屏幕左上角显示最新的 fps 值
        GUI.Label(new Rect(10, 10, 200, 50), Mathf.RoundToInt(fps)+"FPS" , style);
        
    }
}

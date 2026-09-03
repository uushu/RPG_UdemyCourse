
using UnityEngine;

public class Player : Entity
{
    [Header("FPS Info")]
    private float fps = 0f;
    private float timer = 0f;
    private int frameCount = 0;
    
    [Header("Move Info")]
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float jumpForce = 5f;

    [Header("Dash Info")] 
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration; //冲刺持续时间
    private float dashTimer;
    [SerializeField] private float dashCooldown; //冲刺冷却时间
    private float dashCoolTimer;

    [Header("Attack Info")] 
    [SerializeField] private float comboTimeWindow; //连击时间窗口
    private float comboTimer;
    private bool isAttacking = false;
    private int comboCounter = 0;
    
    private float xInput = 0f;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        //模块化
        ShowFPS();
        CheckInput();
       
        dashCoolTimer-=Time.deltaTime;
        dashTimer-=Time.deltaTime;
        comboTimer-=Time.deltaTime;
        
        if(comboTimer<0)
        {
            comboCounter=0;
        }
        
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
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackAbility();
        }
    }

    private void Movement()
    {
        if (isAttacking)
            rb.velocity = new Vector2(0, 0);
        else if (dashTimer > 0)
            rb.velocity=new Vector2(dashSpeed * facingDir,0);
            //rb.velocity=new Vector2(dashSpeed * xInput,0);
        else
            rb.velocity=new Vector2(moveSpeed * xInput,rb.velocity.y);
    }

    void AnimatorController()
    {
        bool isMoving=rb.velocity.x!=0;
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetBool("isMoving",isMoving);
        anim.SetBool("isGrounded",isGrounded);
        anim.SetBool("isDashing",dashTimer>0);
        anim.SetBool("isAttacking",isAttacking);
        anim.SetInteger("comboCounter",comboCounter);
        
    }
    
    private void AttackAbility()
    {
        if(!isGrounded)
            return;
        if (comboTimer < 0)
            comboCounter = 0;
        if(comboCounter>2)
            comboCounter=0;

        isAttacking = true;
        comboTimer = comboTimeWindow;
        
    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
    }
    
    private void DashAbility()
    {
        if (!isAttacking && dashCoolTimer < 0 )
        {
            dashCoolTimer = dashCooldown;
            dashTimer = dashDuration;
        }
    }
    
    private void Jump()
    {
        if(isGrounded)
            rb.velocity=new Vector2(rb.velocity.x,jumpForce);
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

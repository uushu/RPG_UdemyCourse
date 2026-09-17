
using UnityEngine;                               

public class Enemy : Entity
{
    [Header("Stunned Info")] 
    public float stunnedDuration;
    public Vector2 stunnedDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;
    
    [Header("Move Info")]
    public  float moveSpeed ;
    public  float idleTimer;
    public float battleTime;
    
    [Header("Attack Info")]
    public float attackDistance ;
    public float attackCooldown;
    public float playerCheckDistance;
    [HideInInspector] public float lastAttackTime;
    [SerializeField] protected LayerMask whatIsPlayer;

    
    
    
    public EnemyStateMachine stateMachine { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
        
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected()=>Physics2D.Raycast(transform.position, Vector2.right*facingDir, playerCheckDistance, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x+attackDistance*facingDir,transform.position.y));
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x+playerCheckDistance*facingDir,transform.position.y));
    }
}

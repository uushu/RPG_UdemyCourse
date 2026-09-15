using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
   public EnemyStateMachine StateMachine { get; private set; }
   public Enemy enemy { get; private set; }

   protected bool triggerCalled;
   protected float stateTimer;
   private string animBoolName;

   public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
   {
      enemy = _enemyBase;
      StateMachine = _stateMachine;
      animBoolName = _animBoolName;
   }

   public virtual void Update()
   {
      stateTimer -= Time.deltaTime;
   }

   public virtual void Enter()
   {
      triggerCalled = false;
      
      enemy.anim.SetBool(animBoolName, true);
   }

   public virtual void Exit()
   {
      enemy.anim.SetBool(animBoolName, false);

   }

   public virtual void AnimationFinishTrigger()
   {
      triggerCalled = true;
   }
}

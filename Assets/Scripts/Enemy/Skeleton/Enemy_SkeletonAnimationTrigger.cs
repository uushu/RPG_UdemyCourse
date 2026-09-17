using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTrigger : MonoBehaviour
{
    private Enemy_Skeleton enemy=>GetComponentInParent<Enemy_Skeleton>();
    

    private void AnimationTrigger() => enemy.AnimationFinishTrigger();

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.attackRadius);

        foreach (var hit in colliders)
        {
            var player = hit.GetComponent<Player>();
            if(player != null)
                player.Damage();
            
        }
    }

    private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
<<<<<<< HEAD:Assets/Scripts/Enemy/EnemyAnimationTrigger.cs
    
=======
>>>>>>> 8304eb08c271afbdc072ba49e6058888a69e708b:Assets/Scripts/Enemy/Skeleton/Enemy_SkeletonAnimationTrigger.cs
}

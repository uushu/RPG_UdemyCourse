using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player;
    
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.attackRadius);

        foreach (var hit in colliders)
        {
            var enemy = hit.GetComponent<Enemy>();
            if ( enemy!= null)
            {
                enemy.Damage();
            }
        }
    }
}

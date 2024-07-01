using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask playerLayer;
    public int playerDamageAmount = 4;

   

    void Update()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            Playerhealth playerhealth = player.GetComponent<Playerhealth>();

            if(playerhealth != null)
            {
                playerhealth.DealDamage(playerDamageAmount);
            }
        }

       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject player;
    public float detectingRange;

    public float timeBetweenAttacks = 0.5f;
    private Animator anim;

    public float moveSpeed;
    public float attackRange = 2f;
    private bool isAttacking = false;

    //health
    public int currentHealth;
    public int maxHealth;
    public int damageAmount;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
       // Debug.Log(distance);

        if (distance < detectingRange)
        {
            anim.SetBool("PlayerInRange", true);

            if(distance<=attackRange && !isAttacking)
            {
                StartCoroutine(AttackAfterDelay());
            }

            else if (!isAttacking)
            {
                MoveTowardsPlayer();
            }
        }

        else
        {
            anim.SetBool("PlayerInRange", false);
        }
      
    }

    IEnumerator AttackAfterDelay()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }



    private void MoveTowardsPlayer()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, transform.position.y);
        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;

        FlipSprite(direction.x);

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }


    private void FlipSprite(float directionX)
    {
        if (directionX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }

        else if (directionX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void TakeDamage()
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
  
    void Die()
    {
        anim.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f);
        

        Debug.Log("Boss is Dead");
        
    }
}

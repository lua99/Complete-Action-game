using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private float horizontalInput;

    public float jumpForce;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayers;
    private bool isGrounded;

    private SpriteRenderer sr;

    private Animator anim;

    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayers;
    public LayerMask bossLayers;

   


    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    //dash
    private bool canDash = true;
    private bool isDashing;
    public float dashingSpeed = 15f;
    public float dashTime = 0.3f;
    public float dashdelay = 1f;
    public TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
       

        if (!PauseMenu.instance.isPaused)
        {



            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);

            horizontalInput = Input.GetAxis("Horizontal");

            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
               // Audio.instance.PlaySfx(3);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash());
            }

            if (rb.velocity.x < 0)
            {
                //sr.flipX = true;
                transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);


            }

            else if (rb.velocity.x > 0)
            {
                // sr.flipX = false;
                transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);

            }

            if (Input.GetMouseButtonDown(0) && Time.time>=nextAttackTime)
            {
                Attack();
                BossAttack();
                nextAttackTime = Time.time + 1f / attackCooldown;
            }
        }

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("Speed", Mathf.Abs( rb.velocity.x));

    }
    
    void Attack()
    {
        anim.SetTrigger("Attack");

       Collider2D[] hitEnemies=   Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage();
            
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

   void BossAttack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitBossenemies= Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, bossLayers);

        foreach(Collider2D bossenemy in hitBossenemies)
        {
            bossenemy.GetComponent<Boss>().TakeDamage();
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float oggravity = rb.gravityScale;
        rb.gravityScale = 0;
        float dashingDirection =Mathf.Sign( transform.localScale.x);
        rb.velocity = new Vector2(dashingDirection * dashingSpeed, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.gravityScale = oggravity;
        isDashing = false;
        yield return new WaitForSeconds(dashdelay);
        canDash = true;

    }
 
}

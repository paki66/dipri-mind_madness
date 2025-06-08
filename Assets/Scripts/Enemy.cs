using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnDestroy;
    [SerializeField] float health, maxHealth = 1f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] public int attackPoints = 1; 
    Rigidbody2D rb;
    [SerializeField] EnemyRangeCollider rangeCollider;
    [SerializeField] EnemyStopCollider stopCollider;
    Vector2 moveDirection;
    [SerializeField] Animator animator;
    private bool isAlive = true;
    [SerializeField] float size;


    [SerializeField] FloatingHealthBar healthBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        if (rangeCollider.follow == true && stopCollider.follow == true && isAlive)
        {
            Vector3 direction = (rangeCollider.target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            moveDirection = direction;
            animator.SetBool("Follow", true);
            if (direction.x > 0)
            {
                rb.transform.localScale = new Vector3(1*size, 1*size, 1);
            }
            else
            {
                rb.transform.localScale = new Vector3(-1*size, 1*size, 1);
            }
        }
        else
        {
            moveDirection = Vector3.zero;
            animator.SetBool("Follow", false);
            if (stopCollider.follow == false)
            {
                animator.SetTrigger("Attack");
            }
        }
    }





    private void FixedUpdate()
    {
        if (rangeCollider.target)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        //healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            isAlive = false;
            Die();
        }
        DamagePopup.Create(rb.transform.position, damageAmount);
    }

    public void Die()
    {
        animator.SetTrigger("Dead");
        rb.bodyType = RigidbodyType2D.Kinematic;

        Invoke("DestroyObject", 3f);
        //OnDestroy?.Invoke(this);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}

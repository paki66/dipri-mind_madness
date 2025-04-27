using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnDestroy;
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] float moveSpeed = 2f;
    Rigidbody2D rb;
    [SerializeField] EnemyRangeCollider rangeCollider;
    [SerializeField] EnemyStopCollider stopCollider;
    Vector2 moveDirection;


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
        if (rangeCollider.follow == true && stopCollider.follow == true)
        {
            Vector3 direction = (rangeCollider.target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
        else
        {
            moveDirection = Vector3.zero;
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
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
        DamagePopup.Create(rb.transform.position, damageAmount);
    }

    public void Die()
    {
        Destroy(gameObject);
        //OnDestroy?.Invoke(this);
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D body;

    public Animator animator;
    public bool isMovingLeft = true;
    public int attack = 1;
    [SerializeField] FloatingHealthBar healthBar;
    Vector2 moveDirection;
    [SerializeField] float health, maxHealth = 3f;
    bool isDead = false;
    bool animationDiePlayed = false;

    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfPressAttack = 0;
    float lastPressedTime = 0;
    float maxComboDelay = 1;

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        body = GetComponent<Rigidbody2D>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;

        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (!animationDiePlayed)
            {
                animator.Play("Die");
            }
            if (animationDiePlayed && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) 
            {
                animator.enabled = false;
            }
            animationDiePlayed=true;
            return;
        }

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (isMovingLeft)
        {
            body.transform.localScale = new Vector3(0.4f, 0.4f, 1);
        }
        else
        {
            body.transform.localScale = new Vector3(-0.4f, 0.4f, 1);
        }

        moveDirection = new Vector2(xInput, yInput).normalized;
        body.linearVelocity = moveDirection * speed;
        animator.SetFloat("moveSpeed", moveDirection.magnitude);


        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetBool("Attack1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            animator.SetBool("Attack2", false);
            noOfPressAttack = 0;
        }

        if (Time.time - lastPressedTime > maxComboDelay)
        {
            noOfPressAttack = 0;
        }


        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }

        if (xInput > 0)
        {
            isMovingLeft = false;
        }
        if (xInput < 0)
        {
            isMovingLeft = true;
        }

    }

    void Attack()
    {
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        lastPressedTime = Time.time;
        noOfPressAttack++;
        if (noOfPressAttack == 1)
        {
            animator.SetBool("Attack1", true);
        }
        noOfPressAttack = Mathf.Clamp(noOfPressAttack, 0, 2);

        if (noOfPressAttack >= 2)
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", true);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            body.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        }
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<Enemy>();
        if (atm != null)
        {
            atm.TakeDamage(attack);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        animator.Play("Hit");

        if (health <= 0 && !isDead)
        {
            isDead = true;
        }

        DamagePopup.Create(body.position, damageAmount);
        
        if (isDead)
        {
            body.bodyType = RigidbodyType2D.Kinematic;

            body.linearVelocity = Vector3.zero; 

            GameOver();
        }

        void GameOver()
        {
            Debug.Log("Game is over!");
        }
    }

}

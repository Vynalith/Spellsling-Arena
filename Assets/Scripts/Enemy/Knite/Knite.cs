using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knite : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float detectRange = 5f;
    public Transform player;
    public GameObject heartPowerUp;
    public int maxHealth = 8;
    private int currentHealth;

    private bool isMoving;
    private bool isAttacking;
    private Vector3 initialPosition;

    void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            Idle();
        }
    }

    void MoveTowardsPlayer()
    {
        isMoving = true;
        isAttacking = false;
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);

        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        FacePlayer();
    }

    void Idle()
    {
        isMoving = false;
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        // Add your attack logic here, e.g., dealing damage to the player
        // Example: player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        yield return new WaitForSeconds(1f); // Attack cooldown
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        // Drop heart power-up
        Instantiate(heartPowerUp, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f); // Delay to play death animation
    }
}
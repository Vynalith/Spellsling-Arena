using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public Animator animator;
    public GameObject heart;
    public GameObject damageEffect;

    // Player awareness values
    private Transform aim;
    private GameObject aimTarget;
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }
    public float playerAwarenessDistance;
    private GameObject playerTarget;

    // Movement values
    public Transform player;
    public GameObject dumbPlayer;
    public float speed;
    public Rigidbody2D rigidbody;
    private Vector2 targetDirection;
    public GameObject sprite;
    public GameObject anchor;

    void Start()
    {
        // Initialize player awareness
        playerTarget = GameObject.Find("Aim");
        player = playerTarget.transform;

        // Initialize movement
        dumbPlayer = GameObject.Find("Player");
        player = dumbPlayer.transform;
        anchor = GameObject.Find("EnemyAnchor");
        rigidbody = GetComponent<Rigidbody2D>();

        speed = 4f;
    }

    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector;

        AwareOfPlayer = enemyToPlayerVector.magnitude <= playerAwarenessDistance;
    }

    public void HurtMe(int damage)
    {
        Instantiate(damageEffect, transform.position, transform.rotation);
        health -= damage;

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    public void LightningHurtMe(int ouchie) => HurtMe(ouchie + 1);
    public void FireHurtMe(int ouchie) => HurtMe(ouchie);
    public void IceHurtMe(int ouchie) => HurtMe(ouchie);
    public void EarthHurtMe(int ouchie) => HurtMe(ouchie);

    private void HandleDeath()
    {
        int heartOrNo = Random.Range(0, 4);
        if (heartOrNo >= 2)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        CurrentRoom.SendMessage("RoomClear");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
            HurtMe(1);
            GameObject explo = Instantiate(damage, transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }

        if (other.CompareTag("Earth") || other.CompareTag("Lightning") || other.CompareTag("Ice"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            animator.Play("GoopAttack");
            other.gameObject.SendMessage("HurtMe", 1);
        }
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
        sprite.transform.rotation = anchor.transform.rotation;
    }

    private void UpdateTargetDirection()
    {
        targetDirection = AwareOfPlayer ? DirectionToPlayer : Vector2.zero;
    }

    private void RotateTowardsTarget()
    {
        if (targetDirection == Vector2.zero) return;

        // If you have specific rotation logic, implement it here
        rigidbody.transform.rotation = sprite.transform.rotation;
    }

    private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rigidbody.velocity = Vector2.zero;
        }
        else
        {
            rigidbody.velocity = transform.up * speed;
        }
    }
}

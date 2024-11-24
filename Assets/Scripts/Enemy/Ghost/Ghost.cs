using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private GameObject damagePrefab; // Assign in the Inspector
    [SerializeField] private GameObject heartPrefab; // Prefab for the heart
    [SerializeField] private Transform anchor;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float playerAwarenessDistance = 5f; // Awareness range
    [SerializeField] private int health = 10;

    private Transform player; // Cached reference to the player
    private bool awareOfPlayer = false;
    private Vector2 directionToPlayer;
    private Animator animator;
    private Rigidbody2D rb;

    public GameObject playertarget; // Assigned in Start

    void Start()
    {
        // Cache components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Find references (can be assigned through the Inspector instead for better performance)
        playertarget = GameObject.Find("Aim");
        player = GameObject.Find("Player")?.transform;
        anchor = GameObject.Find("EnemyAnchor")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Calculate direction to the player
        Vector2 enemyToPlayerVector = player.position - transform.position;
        directionToPlayer = enemyToPlayerVector;

        // Check if player is within awareness distance
        awareOfPlayer = enemyToPlayerVector.magnitude <= playerAwarenessDistance;
    }

    void FixedUpdate()
    {
        if (awareOfPlayer)
        {
            RotateTowardsTarget();
            MoveTowardsPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop movement if unaware
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 direction = directionToPlayer.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle; // Rotate the Rigidbody
    }

    private void MoveTowardsPlayer()
    {
        rb.velocity = directionToPlayer.normalized * speed; // Move the Rigidbody
    }

    // Damage the ghost
    public void DamageGhost(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DropHeart();
            Destroy(gameObject);
            SendRoomClearMessage();
        }
    }

    private void DropHeart()
    {
        int heartOrNo = Random.Range(0, 4);
        if (heartOrNo >= 2 && heartPrefab != null)
        {
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
    }

    private void SendRoomClearMessage()
    {
        GameObject currentRoom = GameObject.Find("CurrentRoom");
        currentRoom?.SendMessage("RoomClear");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
            DamageGhost(1);

            if (damagePrefab != null)
            {
                GameObject explosion = Instantiate(damagePrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 1f);
            }
        }
        else if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
            DamageGhost(2);
        }
        else if (other.gameObject.CompareTag("Ice"))
        {
            Destroy(other.gameObject);
            DamageGhost(1);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("GoopAttack");
            // Handle player collision
        }
    }

    // Method to get the player target
    public GameObject GetPlayerTarget()
    {
        return playertarget;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEnemy : MonoBehaviour
{
    public GameObject[] Waypoints;
    public int currentWaypoint;
    private float randomStart;
    public GameObject lastWaypoint;
    public GameObject nextWaypoint;

    public GameObject laserBeam;

    public float timer;
    public float movementCooldown = 5f;
    public float movementTimer;

    public int health;
    public int maxHealth = 4;
    public GameObject CurrentRoom;
    public GameObject heart;
    public GameObject damageEffect;

    // Movement variables
    public bool isMoving;
    public float moveSpeed;
    public Vector3 newDirection;
    public Rigidbody2D rb;
    public string nextWaypointString;
    public bool waypointReached;

    void Start()
    {
        randomStart = Mathf.Round(Random.Range(0f, Waypoints.Length - 1));
        currentWaypoint = Mathf.FloorToInt(randomStart);
        transform.position = Waypoints[currentWaypoint].transform.position;
        health = maxHealth;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (movementTimer < movementCooldown)
        {
            movementTimer += Time.deltaTime;
        }
        else
        {
            MoveToNextWaypoint();
            movementTimer = 0f;
        }

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypoint >= Waypoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint++;
        }

        if (currentWaypoint == 0)
        {
            lastWaypoint = Waypoints[Waypoints.Length - 1];
        }
        else
        {
            lastWaypoint = Waypoints[currentWaypoint - 1];
        }
        nextWaypoint = Waypoints[currentWaypoint];
        nextWaypointString = nextWaypoint.name;

        if (!isMoving)
        {
            isMoving = true;
            newDirection = (nextWaypoint.transform.position - lastWaypoint.transform.position);
            rb.AddForce(newDirection * moveSpeed, ForceMode2D.Impulse);
        }
    }

    public void CoilChecker(GameObject other)
    {
        if (other == nextWaypoint)
        {
            isMoving = false;
            rb.AddForce(-newDirection * moveSpeed, ForceMode2D.Impulse);
            transform.position = nextWaypoint.transform.position;
        }
    }

    public void HurtMe(int damage)
    {
        health -= damage;
        Instantiate(damageEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    public void LightningHurtMe(int ouchie)
    {
        if (health < maxHealth)
        {
            health += ouchie;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
        if (!isMoving)
        {
            moveSpeed += ouchie * 0.25f;
        }

        if (health <= 0)
        {
            HandleDeath();
        }
    }

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("HurtMe", 1);
        }

        if (other.CompareTag("FILLERTEXT"))
        {
            CoilChecker(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    public Vector3 userDirection = Vector3.one;
    public float moveSpeed = 1;
    public float moveTimer;
    public bool flaring = false;
    public Animator animator;
    public float flareTime;
    public int randomX;
    public int randomY;

    public SpriteRenderer spriteRenderer;
    public GameObject puddle;
    public GameObject steam;

    public int health;
    public int maxHealth;

    public bool isLooking;
    public float lookingTimer;
    public float lookingTimeTotal;

    public float timer;

    public Vector3 looking;
    public bool isMoving;
    public bool readyToMove;
    public Vector3 movement;

    public GameObject fire;
    public bool isFireballBig;

    public GameObject currentRoom;
    public GameObject heart;

    public Transform currentScale;

    void Start()
    {
        maxHealth = 4;
        health = maxHealth;
        readyToMove = true;
        flaring = false;
    }

    void Update()
    {
        if (readyToMove)
        {
            moveSpeed = 1;
            movement = userDirection.normalized;

            transform.Translate(movement * moveSpeed * Time.deltaTime);
            isMoving = true;
            animator.SetBool("IsMoving", true);

            if (Mathf.Abs(randomX) > Mathf.Abs(randomY))
            {
                animator.SetInteger("Direction", 1);
            }
            else if (Mathf.Abs(randomY) > Mathf.Abs(randomX))
            {
                animator.SetInteger("Direction", 2);
            }

            moveTimer += Time.deltaTime;
        }

        if (moveTimer >= 1f)
        {
            readyToMove = false;
            isMoving = false;
            animator.SetBool("IsMoving", false);
            moveTimer = 0;
            moveSpeed = 0;

            userDirection = Vector3.zero;
            flaring = true;
            animator.Play("Flare up");
        }

        if (flaring)
        {
            flareTime += Time.deltaTime;
            if (flareTime >= 1f)
            {
                flareTime = 0f;
                RandomizeDirection();
                flaring = false;
                readyToMove = true;
            }
        }

        currentScale = transform;
        if (health <= 0 || currentScale.localScale.x <= 0.5f)
        {
            Destroy(gameObject);
        }

        ResetZ();
    }

    void ResetZ()
    {
        if (transform.position.z > 10 || transform.position.z < -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
    }

    void RandomizeDirection()
    {
        randomX = Random.Range(-1, 2);
        randomY = Random.Range(-1, 2);

        animator.SetInteger("movementX", randomX);
        animator.SetInteger("movementY", randomY);

        if (randomX == 0 && randomY == 0)
        {
            RandomizeDirection();
        }

        userDirection = new Vector3(randomX, randomY, 0f);
    }

    public void GetFireSize()
    {
        fire.SendMessage("FireEnemyGetSize", gameObject);
    }

    public void SetFireballSize(bool otherSize)
    {
        isFireballBig = otherSize;
    }

    public void HurtMe(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);
            if (heartOrNo >= 2)
            {
                Instantiate(heart, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            currentRoom.SendMessage("RoomClear");
        }
    }

    public void LightningHurtMe(int ouchie)
    {
        health -= ouchie;
        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);
            if (heartOrNo >= 2)
            {
                Instantiate(heart, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            currentRoom.SendMessage("RoomClear");
        }
    }

    public void FireHurtMe(int ouchie)
    {
        health += ouchie;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        float newScale = Mathf.Min(transform.localScale.x + (health - maxHealth) * 0.05f, 3f);
        transform.localScale = new Vector3(newScale, newScale, transform.localScale.z);

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);
            if (heartOrNo >= 2)
            {
                Instantiate(heart, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            currentRoom.SendMessage("RoomClear");
        }
    }

    public void IceHurtMe(int ouchie)
    {
        health -= ouchie;
        if (health <= 0 || transform.localScale.x <= 0.5f)
        {
            int heartOrNo = Random.Range(0, 4);
            if (heartOrNo >= 2)
            {
                Instantiate(heart, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            currentRoom.SendMessage("RoomClear");
        }
    }

    public void EarthHurtMe(int ouchie)
    {
        health -= ouchie;
        if (health <= 0 || transform.localScale.x <= 0.5f)
        {
            int heartOrNo = Random.Range(0, 4);
            if (heartOrNo >= 2)
            {
                Instantiate(heart, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            currentRoom.SendMessage("RoomClear");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("EarthWall"))
        {
            movement = -movement;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EarthWall") || other.gameObject.CompareTag("Wall"))
        {
            isLooking = true;
            isMoving = false;
            animator.SetBool("IsMoving", false);
        }

        if (other.gameObject.CompareTag("IceWall") || other.gameObject.CompareTag("Ice") || other.gameObject.CompareTag("Puddle"))
        {
            Instantiate(steam, other.transform.position, steam.transform.rotation);
            Destroy(other.gameObject);
            HurtMe(1);
            transform.localScale = new Vector3(Mathf.Max(transform.localScale.x - 0.15f, 0.5f), Mathf.Max(transform.localScale.y - 0.15f, 0.5f), transform.localScale.z);
        }

        if (other.gameObject.CompareTag("Fire"))
        {
            fire = other.gameObject;
            GetFireSize();
            if (!isFireballBig)
            {
                FireHurtMe(1);
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("BigFire"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Earth"))
        {
            transform.localScale = new Vector3(Mathf.Max(transform.localScale.x - 0.35f, 0.5f), Mathf.Max(transform.localScale.y - 0.35f, 0.5f), transform.localScale.z);
        }
    }
}

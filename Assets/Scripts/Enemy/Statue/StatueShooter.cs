using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueShooter : MonoBehaviour
{
    public float cooldown;
    public float cooldownCount;
    public GameObject Projectile;
    public float shotForce = 20f;
    public bool playerInRange;
    public GameObject aimer;
    public Animator animator;
    Vector2 aimDirection;
    Vector3 aimSpawner;
    Vector2 newAim;
    public float animTimer;
    public bool startAnim;


    //Room clear stuff
    public GameObject currentRoom;
    public bool roomClear;


    //2 is down, 4 is left, 6 is right, 8 is up
    //public int direction;

    // Start is called before the first frame update
    void Start()
    {
        cooldownCount = 0;
        playerInRange = false;
        aimDirection.x = 0f;
        aimDirection.y = aimer.transform.position.y;
        aimSpawner = this.transform.position;
        aimSpawner.y = this.transform.position.y - 2f;
        roomClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(roomClear == false)
        {
            if (playerInRange)
            {
                if (cooldownCount >= cooldown && startAnim == false)
                {
                    animator.Play("StatueDownSHOOT");
                    startAnim = true;
                    animTimer = cooldownCount + .8f;
                    
                }
                if (cooldownCount >= animTimer && cooldownCount >= cooldown)
                {
                    Shoot();
                    cooldownCount = 0;
                    animTimer = 0f;
                }

            }
            cooldownCount += Time.deltaTime;
            if (!playerInRange)
            {
                startAnim = false;
                animTimer = 0f;
            }

        }

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print(other.gameObject);
            playerInRange= true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange= false;
        }
    }


    public void Shoot()
    {
        animator.Play("StatueDownSHOOT");

        GameObject arrow = Instantiate(Projectile, this.transform.position, this.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        //rb.AddForce(aimer.transform.position * shotForce);
        //Vector3 aimdirection = (0f, aimer.transform.position.y, 0f);
        //all projectiles have a built-in 100f force applied to X
        aimDirection.x = -5f;
        //print(aimDirection);
        newAim.x = -100f;
        newAim.y = (aimDirection.y - 35) * shotForce;
        rb.AddForce(newAim);
        startAnim = false;

    }


    public void RoomCleared()
    {
        roomClear = true;
    }


}

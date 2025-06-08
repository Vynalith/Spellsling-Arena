using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueShooter : MonoBehaviour
{
    public float cooldown;
    private float cooldownCount;
    public GameObject Projectile;
    public float shotForce = 20f;
    public bool playerInRange;
    public GameObject aimer;
    public Animator animator;

    //2 is down, 4 is left, 6 is right, 8 is up
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        cooldownCount = 0;
        playerInRange = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange=true)
        {
           if (cooldownCount >= cooldown)
                {
                    //animator.Play("StatueDownSHOOT");

                    Shoot();
                    cooldownCount = 0;
                } 
        }
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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
        rb.AddForce(aimer.transform.position * shotForce * 25f);

    }

}

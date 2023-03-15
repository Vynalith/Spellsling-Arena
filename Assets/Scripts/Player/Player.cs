using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //private Rigidbody2D r2d;
    public float speed;
    public int health;

    Vector2 movement;
    Vector2 mousePos;

    public Rigidbody2D rb;
    public Rigidbody2D rb2;
    //public GameObject fireBallPrefab;
    public GameObject damage;
    public Camera camera;
    public Animator animator;
    public GameObject Shooter;

    //public Animation lightningAttack;
    private Vector3 flashback;
   public int element;
   private bool Playing;
    
    public GameObject gameUI;




    // Start is called before the first frame update
    void Start()
    {
        Playing=true;
        element = 1;
        animator.SetInteger("element", element);
    }

    // Update is called once per frame
    void Update()
    {
        if(Playing==true)
        {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
         
        
        if( Input.GetButtonDown("lightning"))
        {
            gameUI.SendMessage("ActiveElement", 1);
            
            element = 1;
            animator.SetInteger("element", element);
        }
        
        if( Input.GetButtonDown("fire"))
        {
            gameUI.SendMessage("ActiveElement", 2);
            element = 2;
            animator.SetInteger("element", element);
        }
        if( Input.GetButtonDown("ice"))
        {
            gameUI.SendMessage("ActiveElement", 3);
            element = 3;
            animator.SetInteger("element", element);
        }
         if( Input.GetButtonDown("earth"))
        {
            gameUI.SendMessage("ActiveElement", 4);
            element = 4;
            animator.SetInteger("element", element);
        }

        //attack    Now Done in Shooter
        //if( Input.GetButtonDown("Fire2"))
        //{
            //Instantiate(fireBallPrefab, this.transform.position, Quaternion.identity);

       
        //}
        }
        flashback = this.transform.position;
        //print(flashback);
    }

    void FixedUpdate()
    {
        if(Playing==true)
        {
        //movement
        rb.MovePosition(rb.position + movement * speed *Time.fixedDeltaTime);
        rb2.MovePosition(rb.position + movement * speed *Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2.rotation = angle;
        
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HurtMe(1);
            //this.transform.position -= this.transform.position - other.transform.position;
        }
        else if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            health = health-1;

             if(health >= 1)
            {
                if(element == 1)
                {
                    animator.Play("LightningDamage");
                }
                if(element == 2)
                {
                    animator.Play("FireDamage");
                }
                  if(element == 3)
                {
                    animator.Play("IceDamage");
                }
                 if(element == 4)
                {
                    animator.Play("EarthDamage");
                }
            }


            //print(health);
            Destroy(other.gameObject);
        }
        
    }


    public void Heal()
    {
        if(health < 5)
        {
            health = health+1;
        }
    }

    private void HurtMe(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Playing = false;
            Shooter.gameObject.SendMessage("Death");
            animator.Play("DEATH");
        }
        else if (health >= 1)
        {
            if (element == 1)
            {
                animator.Play("LightningDamage");
            }
            if (element == 2)
            {
                animator.Play("FireDamage");
            }
            if (element == 3)
            {
                animator.Play("IceDamage");
            }
            if (element == 4)
            {
                animator.Play("EarthDamage");
            }
        }
    }

    public void LightningAttacks()
    {
        
        animator.Play("Lightning m1");
        
    }

    public void FireAttacks()
    {
        
        animator.Play("Fire m1");
        
    }

     public void IceAttacks()
    {
        
        animator.Play("Ice m1");
        
    }

    public void EarthAttacks()
    {
        
        animator.Play("Earth m1");
        
    }

    public void Win()
    {
        Playing = false;
        Shooter.gameObject.SendMessage("Win");
        animator.Play("WIN!");
   }
}

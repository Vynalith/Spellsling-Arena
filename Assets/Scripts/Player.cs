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
            element = 1;
            animator.SetInteger("element", element);
        }
        
        if( Input.GetButtonDown("fire"))
        {
            element = 2;
            animator.SetInteger("element", element);
        }
        if( Input.GetButtonDown("ice"))
        {
            element = 3;
            animator.SetInteger("element", element);
        }
         if( Input.GetButtonDown("earth"))
        {
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
            print("Moving from ");
            print(this.transform.position);
            print(" to ");
            print(flashback);
            this.transform.position = flashback;
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            print("Collision");
            health -= 1;
            if (health <= 0)
            {
                Playing = false;
                Shooter.gameObject.SendMessage("Death");
                animator.Play("DEATH");
            }

            //this.transform.position -= this.transform.position - other.transform.position;
        }
        else if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            print("hit");
            health -= 1;
            print(health);
            if (health <= 0)
            {
                Playing = false;
                Shooter.gameObject.SendMessage("Death");
                animator.Play("DEATH");
            }
            Destroy(other.gameObject);
        }
        else
        {
            print("fuck");
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

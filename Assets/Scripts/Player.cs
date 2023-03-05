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
    public GameObject damage;
    public Camera Camera;
    public Animator animator;

    public GameObject EnemyShooter;

    //public Animation lightningAttack;
   
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

        mousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

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

    public void EnemyCollide()
    {
        health=health-1; 
        //GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
        Destroy(explo, 1f);
        if(health <= 0)
        {
            Playing=false;
            Shooter.gameObject.SendMessage("Death");
            animator.Play("DEATH");
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

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
    public GameObject fireBallPrefab;
    public GameObject damage;
    public Camera camera;
    public Animator animator;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
         
        

        //attack    Now Done in Shooter
        //if( Input.GetButtonDown("Fire2"))
        //{
            //Instantiate(fireBallPrefab, this.transform.position, Quaternion.identity);

       
        //}
        
    }

    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * speed *Time.fixedDeltaTime);
        rb2.MovePosition(rb.position + movement * speed *Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2.rotation = angle;
        
        
    }

    public void EnemyCollide()
    {
        health=health-1; 
        GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
        Destroy(explo, 1f);
        if(health <= 0)
        {
          
            this.gameObject.SetActive(false);
        }
    }
}

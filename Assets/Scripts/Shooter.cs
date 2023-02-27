using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float speed;

    Vector2 movement;
    public Rigidbody2D rb;
    public GameObject player;

    public Transform ShooterThing;
    private GameObject Projectile;
    public GameObject firemagic;
    public GameObject lightningmagic;
    public GameObject icemagic;
    public GameObject earthmagic;
    public GameObject Aim;
    

    private bool lightning;
    private bool fire;
    private bool ice;
    private bool earth;


    public bool Playing;

    public float shotForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Playing=true;

       Projectile = lightningmagic;
       lightning = true;
       fire = false;
       ice = false;
       earth = false;

    }

    public void Death()
    {
        Playing=false;
    }

    
    public void Win()
    {
        Playing=false;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

          if( Input.GetButtonDown("lightning"))
        {
           Projectile = lightningmagic;
           lightning = true;
            
            fire = false;
            ice = false;
            earth = false;
        }
        if( Input.GetButtonDown("fire"))
        {
            Projectile = firemagic;
            fire = true;
            
            lightning = false;
            ice = false;
            earth = false;
        }
         if( Input.GetButtonDown("ice"))
        {
            Projectile = icemagic;
            ice = true;

            fire = false;
            lightning = false;
            earth = false;
            
        }
         if( Input.GetButtonDown("earth"))
        {
            Projectile = earthmagic;
            earth = true;
            
            fire = false;
            lightning = false;
            ice = false;
        }


        //attack
        if( Input.GetButtonDown("Fire2"))
        {
             if(Playing==true)
            {
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        //movement
        //rb.MovePosition(rb.position + movement * speed *Time.fixedDeltaTime);
        
    }
    void Shoot()
    {
        if(lightning == true)
        {
            player.gameObject.SendMessage("LightningAttacks");
        }

        if(fire == true)
        {
            player.gameObject.SendMessage("FireAttacks");
        }

        if(ice == true)
        {
            player.gameObject.SendMessage("IceAttacks");
        }

        if(earth == true)
        {
            player.gameObject.SendMessage("EarthAttacks");
        }
        

        GameObject bullet = Instantiate(Projectile, Aim.transform.position, ShooterThing.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
    }
}

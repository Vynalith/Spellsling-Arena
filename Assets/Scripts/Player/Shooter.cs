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
    private GameObject Projectile2;
    public GameObject firemagic;
    public GameObject lightningmagic;
    public GameObject icemagic;
    public GameObject earthmagic;


    public GameObject firemagic2;
    public GameObject lightningmagic2;
    public GameObject icemagic2;
    public GameObject earthmagic2;


    public GameObject Aim;
    

    private bool lightning;
    private bool fire;
    private bool ice;
    private bool earth;


    public bool Playing;

    public float shotForce = 20f;
    public float shotForce2 = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Playing=true;

       Projectile = lightningmagic;
       Projectile2 = lightningmagic2;
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

            Projectile2 = lightningmagic2;

            shotForce = 20f;
           
        }
        if( Input.GetButtonDown("fire"))
        {
            Projectile = firemagic;
            Projectile2 = firemagic2;
            fire = true;
            
            lightning = false;
            ice = false;
            earth = false;

             shotForce = 20f;
        }
         if( Input.GetButtonDown("ice"))
        {
            Projectile = icemagic;
            Projectile2 = icemagic2;
            ice = true;

            fire = false;
            lightning = false;
            earth = false;

            shotForce = 20f;
            shotForce2 = 0f;
            
        }
         if( Input.GetButtonDown("earth"))
        {
            Projectile = earthmagic;
            Projectile2 = earthmagic2;
            earth = true;
            
            fire = false;
            lightning = false;
            ice = false;

            shotForce = 10f;
        }


        //attack
        if( Input.GetButtonDown("Fire2"))
        {
            print ("M1");
             if(Playing==true)
            {
                Shoot();
            }
        }

            if( Input.GetButtonDown("M2"))
        {
            print ("M2");
             if(Playing==true)
            {
                Shoot2();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Projectile, Aim.transform.position, ShooterThing.rotation);

        if (lightning == true)
        {
            player.gameObject.SendMessage("LightningAttacks");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
        }

        if(fire == true)
        {
            player.gameObject.SendMessage("FireAttacks");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
        }

        if(ice == true)
        {
            player.gameObject.SendMessage("IceAttacks");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
        }

        if(earth == true)
        {
            player.gameObject.SendMessage("EarthAttacks");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
        }
        

        
        
    }

    void Shoot2()
    {
        GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, ShooterThing.rotation);
        
        if(lightning == true)
        {
            player.gameObject.SendMessage("LightningAttacks");
        }

        if(fire == true)
        {
            player.gameObject.SendMessage("FireAttacks");
            Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
            rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
        }

        if(ice == true)
        {
            player.gameObject.SendMessage("IceAttacks");
        }

        if(earth == true)
        {
            player.gameObject.SendMessage("EarthAttacks");
        }
        

        
        
    }


    public void PuddleHurtMe(int damage)
    {
        //stupid puddle sends to this instead of player
        player.SendMessage("HurtMe", 1);
    }
}


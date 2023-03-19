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


    //magic SFX
    public AudioSource IceM1;
    //public AudioSource IceM2;
    public AudioSource FireM1;
    public AudioSource FireM2;
    public AudioSource LightningM1;
    //public AudioSource LightningM2;
    public AudioSource EarthM1;
    //public AudioSource EarthM2;



    //cooldowns
    private float iceCooldown;
    private float fireCooldown;
    private float earthCooldown;
    private float lightningCooldown;
    private bool iceReady;
    private bool fireReady;
    private bool earthReady;
    private bool lightningReady;
    public GameObject gameUI;





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

        lightningReady = true;
        iceReady = true;
        fireReady = true;
        earthReady = true;

    }

    public void Death()
    {
        Playing=false;
    }

    
    public void Win()
    {
        Playing=false;
        Aim.gameObject.SetActive(false);
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
            //print ("M1");
             if(Playing==true)
            {
                Shoot();
            }
        }

            if( Input.GetButtonDown("M2"))
        {
            //print ("M2");
             if(Playing==true)
            {
                Shoot2();
            }
        }




        //still in update()


        //figure out each cooldown
        //assign in shoot or shoot2 in each if statement
        //send message to player.cs
        //have player.cs have the variables and change UI in update()
        //in each shoot statement here, send message to player called "cooldown checker" with the element as an argument
        //have if statement in player.cs cooldown checker, where if selected element isn't off cooldown, send message to shooter.cs cooldown confirm()
        //saying no if it isn't ready, saying yes if it is
        //if its yes, have shoot and add cooldown method in an if statement
        //or maybe have player keep track of each cooldown and send a message saying an element is ready to a shooter.cs boolean

    }


    public void LightningEnable()
    {
        lightningReady = true;
        print("lightning enable");
        print(lightningReady);
    }
    public void IceEnable()
    {
        iceReady = true;
    }
    public void FireEnable()
    {
        fireReady = true;
    }
    public void EarthEnable()
    {
        earthReady = true;
    }


    void Shoot()
    {
        
        
        if (lightning == true)
        {
            if (lightningReady)
            {
            lightningCooldown = .5f;
            GameObject bullet = Instantiate(Projectile, Aim.transform.position, ShooterThing.rotation);
            LightningM1.Play();
            player.gameObject.SendMessage("LightningAttacks");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
            lightningReady = false;
            gameUI.SendMessage("LightningCooldown", lightningCooldown);
            
            }
            else
            {
                print(lightningReady);
            }
            
        }

        if(fire == true)
        {
            if (fireReady)
            {
                GameObject bullet = Instantiate(Projectile, Aim.transform.position, ShooterThing.rotation);
                fireReady = false;
                fireCooldown = .5f;
                gameUI.SendMessage("FireCooldown", fireCooldown);
                FireM1.Play();
                player.gameObject.SendMessage("FireAttacks");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);

            }
            
        }

        if(ice == true)
        {
            if (iceReady)
            {
                GameObject bullet = Instantiate(Projectile, Aim.transform.position, ShooterThing.rotation);
                iceReady = false;
                iceCooldown = .5f;
                gameUI.SendMessage("IceCooldown", iceCooldown);
                IceM1.Play();
                player.gameObject.SendMessage("IceAttacks");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);

            }
            
        }

        if(earth == true)
        {
            if (earthReady)
            {
                GameObject bullet = Instantiate(Projectile, Aim.transform.position, ShooterThing.rotation);
                earthReady = false;
                earthCooldown = .75f;
                gameUI.SendMessage("EarthCooldown", earthCooldown);
                EarthM1.Play();
                player.gameObject.SendMessage("EarthAttacks");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);

            }
            
        }
        

        
        
    }

    void Shoot2()
    {
        
        if(lightning == true)
        {
            if (lightningReady)
            {
                lightningReady = false;
                lightningCooldown = 2f;
                gameUI.SendMessage("LightningCooldown", lightningCooldown);
                //LightningM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, ShooterThing.rotation);
                player.gameObject.SendMessage("LightningAttacks");
            }
            
        }

        if(fire == true)
        {
            if (fireReady)
            {
                fireReady = false;
                fireCooldown = 2f;
                gameUI.SendMessage("FireCooldown", fireCooldown);
                FireM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, ShooterThing.rotation);
                player.gameObject.SendMessage("FireAttacks");
                Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
            }
            
        }

        if(ice == true)
        {
            if (iceReady)
            {
                iceReady = false;
                iceCooldown = 3f;
                gameUI.SendMessage("IceCooldown", iceCooldown);
                //IceM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, ShooterThing.rotation);
                player.gameObject.SendMessage("IceAttacks");
            }
            
        }

        if(earth == true)
        {
            if (earthReady)
            {
                earthReady = false;
                earthCooldown = 5f;
                gameUI.SendMessage("EarthCooldown", earthCooldown);
                //EarthM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, ShooterThing.rotation);
                player.gameObject.SendMessage("EarthAttacks");
            }
            
        }
        

        
        
    }


    public void PuddleHurtMe(int damage)
    {
        //stupid puddle sends to this instead of player
        player.SendMessage("HurtMe", 1);
    }
}


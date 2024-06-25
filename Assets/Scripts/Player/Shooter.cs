using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    float speed;

    Vector2 Movement;
    Rigidbody2D rb;
    GameObject Player;

    Transform Shooter;
    GameObject Projectile;
    GameObject Projectile2;
    GameObject firemagic;
    GameObject lightningmagic;
    GameObject icemagic;
    GameObject earthmagic;

    GameObject firemagic2;
    GameObject lightningmagic2;
    GameObject icemagic2;
    GameObject earthmagic2;


    GameObject Aim;
    bool lightning;
    bool fire;
    bool ice;
    bool earth;


    bool Playing;

    float shotForce = 20f;
    float shotForce2 = 20f;


    magic SFX;
    AudioSource IceM1;
    AudioSource IceM2;
    AudioSource FireM1;
    AudioSource FireM2;
    AudioSource LightningM1;
    AudioSource LightningM2;
    AudioSource EarthM1;
    AudioSource EarthM2;



    //cooldowns
    float iceCooldown;
    float fireCooldown;
    float earthCooldown;
    float lightningCooldown;
    bool iceReady;
    bool fireReady;
    bool earthReady;
    bool lightningReady;
    GameObject GameUI;




    public GameObject needsAimed;
    private Vector2 shooterthingpos;


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

    void Death()
    {
        Playing=false;
    }


    void Win()
    {
        Playing = false;
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
            Projectile2 = lightningmagic2;
            lightning = true;
            fire = false;
            ice = false;
            earth = false;

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
        if( Input.GetButtonDown("M1"))
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
    }


    void LightningEnable()
    {
        lightningReady = true;
        print("lightning enable");
        print(lightningReady);
    }
    void IceEnable()
    {
        iceReady = true;
        print("ice enable");
        print(iceReady);
    }
    void FireEnable()
    {
        fireReady = true;
        print("fire enable");
        print(fireReady);
    }
    void EarthEnable()
    {
        earthReady = true;
        print("earth enable");
        print(earthReady);
    }

    /*
    void GetAim(GameObject other)
    {
        other.SendMessage("GetAim", Shooter.up);
    }
    */

    void GetAim(GameObject other)
    {
        shooterpos = Shooter.up;
        //print("get aim");
        //print("shooter" + Shooter.up);
        //print("shooter" + shooterpos);
        //needsAimed = GameObject.Find("Fireball");
        //print(needsAimed);
        other.SendMessage("GetAim", shooterpos);
        //needsAimed.SendMessage("GetAim", SendMessageOptions.DontRequireReceiver);
    }
    void Shoot()
    {
        if (lightning == true)
        {
            if (lightningReady)
            {
                shotForce = 20f;
                lightningCooldown = 1f;
                GameObject bullet = Instantiate(Projectile, Aim.transform.position, Shooter.rotation);
                LightningM1.Play();
                player.GameObject.SendMessage("LightningAttacks");
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
                shotForce = 5f;
                GameObject bullet = Instantiate(Projectile, Aim.transform.position, Shooter.rotation);
                fireReady = false;
                fireCooldown = 1.5f;
                gameUI.SendMessage("FireCooldown", fireCooldown);
                FireM1.Play();
                player.GameObject.SendMessage("FireAttacks");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);

            }

        }

        if(ice == true)
        {
            if (iceReady)
            {
                shotForce = 15f;
                GameObject bullet = Instantiate(Projectile, Aim.transform.position, Shooter.rotation);
                iceReady = false;
                iceCooldown = .5f;
                gameUI.SendMessage("IceCooldown", iceCooldown);
                IceM1.Play();
                player.GameObject.SendMessage("IceAttacks");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);

            }

        }

        if(earth == true)
        {
            if (earthReady)
            {
                shotForce = 10f;
                GameObject bullet = Instantiate(Projectile, Aim.transform.position, Shooter.rotation);
                earthReady = false;
                earthCooldown = 1.5f;
                gameUI.SendMessage("EarthCooldown", earthCooldown);
                EarthM1.Play();
                player.GameObject.SendMessage("EarthAttacks");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);

            }

        }
    }

    void Shoot2()
    {
        //LightningAttack2
        if(lightning == true)
        {
            if (lightningReady)
            {
                lightningReady = false;
                lightningCooldown = 3f;
                GameUI.SendMessage("LightningCooldown", lightningCooldown);
                //LightningM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, Shooter.rotation);
                player.GameObject.SendMessage("LightningAttacks");
            }

        }
        //FireAttack2
        if(fire == true)
        {
            if (fireReady)
            {
                shotForce = 5f;
                fireReady = false;
                fireCooldown = 3f;
                GameUI.SendMessage("FireCooldown", fireCooldown);
                FireM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, Shooter.rotation);
                player.GameObject.SendMessage("FireAttacks");
                Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
                rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
            }

        }
        //IceAttack2
        if(ice == true)
        {
            if (iceReady)
            {
                iceReady = false;
                iceCooldown = 4f;
                GameUI.SendMessage("IceCooldown", iceCooldown);
                //IceM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, Shooter.rotation);
                player.GameObject.SendMessage("IceAttacks");
            }

        }
        //EarthAttack2
        if(earth == true)
        {
            if (earthReady)
            {
                earthReady = false;
                earthCooldown = 5f;
                GameUI.SendMessage("EarthCooldown", earthCooldown);
                //EarthM2.Play();
                GameObject bullet2 = Instantiate(Projectile2, Aim.transform.position, Shooter.rotation);
                player.gameObject.SendMessage("EarthAttacks");
            }

        }
        void HurtMe(int damage)
        {
            //stupid puddle sends to this instead of player
            player.SendMessage("HurtMe", 1);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



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

    //for SFX timing and looping
    public AudioSource song1Intro;
    public AudioSource song1Loop;
    public AudioSource bossSongIntro;
    public AudioSource bossSongLoop;
    public float timer;
    private float bossTimer;
    private float songCount = 13.35f;
    private float bossSongCount = 8.15f;
    private int song1Change = 0;
    private int bossSongChange = 0;
    private bool bossSongStarted;
    public AudioSource winSong;

    public GameObject UI;

    
    public float Deadtimer;
    public bool isDead;
    public float countdown;
    public GameObject Reset;

    // Start is called before the first frame update
    void Start()
    {
        Playing=true;
        element = 1;
        animator.SetInteger("element", element);

        UI = GameObject.Find("PlayerUI");
        isDead = false;
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
        
        

        if (timer >= songCount && song1Change == 0)
        {
            
            song1Change = 1;
        }
        else if(song1Change == 0 && timer < songCount)
        {
            timer+= Time.deltaTime;
        }

        if (song1Change == 1)
        {
            
            song1Intro.Stop();
            song1Loop.Play();
            song1Change = 2;
        }

        if (bossSongStarted)
        {
            if (bossTimer >= bossSongCount && bossSongChange == 0)
            {

                bossSongChange = 1;
            }
            else if (bossSongChange == 0 && bossTimer < bossSongCount)
            {
                bossTimer += Time.deltaTime;
            }

            if (bossSongChange == 1)
            {

                bossSongIntro.Stop();
                bossSongLoop.Play();
                bossSongChange = 2;
            }
        }

        if(isDead == true)
        {
            Deadtimer += Time.deltaTime;
        }

        if(Deadtimer>=countdown)
        {
            Reset.SendMessage("LoadScene", "Menu");
        }

        


    }

    void StartBossMusic()
    {
        song1Loop.Stop();
        bossSongIntro.Play();
        bossSongStarted = true;
    }

    void PlayWinSong()
    {
        bossSongLoop.Stop();
        winSong.Play();
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
            //gameUI.SendMessage("Hurt", 1);
            //this.transform.position -= this.transform.position - other.transform.position;
        }
        else if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            HurtMe(1);  
            //gameUI.SendMessage("Hurt", 1);
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


     void Heal()
    {
        if(health < 5)
        {
            health = health+1;
            UI.SendMessage("Heal", SendMessageOptions.DontRequireReceiver);

        }
    }

    private void HurtMe(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Playing = false;
            isDead = true;
            Shooter.gameObject.SendMessage("Death");
            animator.Play("DEATH");
            UI.SendMessage("Hurt", damage);
    
        }
        else if (health >= 1)
        {
            if (element == 1)
            {
                animator.Play("LightningDamage");
                 UI.SendMessage("Hurt",damage);
            }
            if (element == 2)
            {
                animator.Play("FireDamage");
                 UI.SendMessage("Hurt",damage);
            }
            if (element == 3)
            {
                animator.Play("IceDamage");
                 UI.SendMessage("Hurt",damage);
            }
            if (element == 4)
            {
                animator.Play("EarthDamage");
                 UI.SendMessage("Hurt",damage);
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

        if(element == 1)
        {
            animator.Play("WIN! Lightning"); 
        }
        if(element == 2)
        {
            animator.Play("FireWIN"); 
        }
        if(element == 4)
        {
            animator.Play("EarthWIN"); 
        }
        if(element == 3)
        {
            animator.Play("IceWIN"); 
        }
       
   }

   public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Menu");
    }
}

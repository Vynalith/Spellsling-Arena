using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public float cooldown;
    private float cooldownCount;
    public GameObject Projectile;
    public float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 10;
    private Collider2D finalDetected;
    private RaycastHit hit;
    private int layerMask = 1 << 3;

    public Vector3 shootAngle;

    public Animator animator;


    public int heartOrNo;
    public GameObject heart;

    public float Horizontal;
    public float Vertical;

    private float stupidspeed;

    // Start is called before the first frame update
    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        target2 = GameObject.Find("Shooter");
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        start = this.transform.position;
        cooldownCount++;
        direction = (target.transform.position - start).normalized;
        Debug.DrawRay(start, direction * sightDistance);

        if (SightTest() == target.GetComponent<Collider2D>() || SightTest() == target2.GetComponent<Collider2D>())
        {
            if (cooldownCount >= cooldown)
            {
                Shoot();
                cooldownCount = 0;
            }
        }
        finalDetected = null;
        shootAngle = (start - target.transform.position).normalized;
        shootAngle.y *= -1;

        animator.SetFloat("Horizontal", shootAngle.x);
        animator.SetFloat("Vertical", shootAngle.y);
        Horizontal = shootAngle.x;
        Vertical = shootAngle.y;

    }


    public void Shoot()
    {
        //animator.Play("ArcherRightShoot");

        GameObject arrow = Instantiate(Projectile, start, this.transform.rotation);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (direction.x < 0)
        {
            rb.AddForce(direction * shotForce * 25);
        }
        else
        {
            rb.AddForce(direction * shotForce * 15);
        }
    
    }

    /////////////////////////////////////////////////////
    ///Sight test
    /////////////////////////////////////////////////////

    public Collider2D SightTest()
    {
        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, sightDistance, layerMask);
        if (sightTest.collider != null)
        {
            if (sightTest.collider.gameObject != gameObject)
            {
                finalDetected = null;
                //Debug.Log("Rigidbody collider is: " + sightTest.collider);
            }
            finalDetected = sightTest.collider;
        }
        return finalDetected;
    }


    ///////////////////////////////////////////////
    ///Damage check
    ///////////////////////////////////////////////

    public void HurtMe(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            int heartOrNo = Random.Range(0,4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if(heartOrNo >= 2)
                {
                    Instantiate (heart, this.transform.position, Quaternion.identity);
                }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }


    public void LightningHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void FireHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void IceHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void EarthHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }





    ///////////////////////////////
    ///Collider stuff
    ///////////////////////////////
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
            GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
        if (other.gameObject.CompareTag("Earth"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Ice"))
        {

        }
        
    }
    /*
    public void RecieveSpeed(float speed)
    {
        //why do I have to do this weird roundabout way of getting speed?
        //this is dumb
        stupidspeed = speed;
    }
    
    public void OnCollisionEnter(Collision other)
    {
        

        if (other.gameObject.CompareTag("IceWall"))
        {
            other.gameObject.SendMessage("GetSpeed");
            if(stupidspeed > 3)
            {
                HurtMe(3);
            }
            else if(stupidspeed >= 1)
            {
                HurtMe(1);
            }
        }
    }
    */
}
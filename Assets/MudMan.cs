using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMan : MonoBehaviour
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
    private int layerMask = 1 << 3 | 1 << 7 | 1 << 11 | 1 << 12 | 1 << 13;

    public Vector3 shootAngle;

    public Animator animator;


    public int heartOrNo;
    public GameObject heart;

    public float Horizontal;
    public float Vertical;

    private float stupidspeed;
    private Collider2D Collider;

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
            Collider2D.SetActive(true);
            animator.Play("MudRise");
            animator.SetBool("Awake", true);
            
            if (cooldownCount >= cooldown)
            {
                animator.Play("MudATTACK");
                Shoot();

                cooldownCount = 0;
            }
        }
        else{
             Collider2D.SetActive(false);
             animator.SetBool("Awake", false);
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
            rb.AddForce(direction * shotForce * 15);
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
            if (sightTest.Collider2D.gameObject == gameObject)
            {
            }
            else
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
        health -= ouchie - 1;

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

}
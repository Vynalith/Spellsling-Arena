using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydra : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public float cooldown;
    private float cooldownCount;
    public GameObject[] Projectile;
    public Animation[] attack;
    public float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 10;
    private Collider2D finalDetected;
    private RaycastHit hit;
    private int layerMask = 1 << 3;

    private Vector3 shootAngle;

    public Animator animator;

    public Transform anchor;

    public GameObject WIN;


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

    }


    public void Shoot()
    {
        int RandomNum = Random.Range(0,4);

        if(RandomNum == 0)
        {
        animator.Play("HydraLightningTest");
        }
        if(RandomNum == 1)
        {
        animator.Play("HydraFire");
        }
        if(RandomNum == 2)
        {
        animator.Play("HydraIce");
        }
        if(RandomNum == 3)
        {
        animator.Play("HydraEarth");
        }


        GameObject arrow = Instantiate(Projectile[RandomNum], start, this.transform.rotation);

        arrow.transform.rotation = anchor.transform.rotation;

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





    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);

            health = health - 1;
            GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
            Destroy(explo, 1f);

            if (health <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(WIN, this.transform.position, Quaternion.identity);
                CurrentRoom.gameObject.SendMessage("RoomClear");
            }
        }
        if (other.gameObject.CompareTag("Earth"))
        {

            Destroy(other.gameObject);
            health = health - 3;

            if (health <= 0)
            {
                Destroy(this.gameObject);
                CurrentRoom.gameObject.SendMessage("RoomClear");

            }
        }
        if (other.gameObject.CompareTag("FILLERTEXT"))
        {
            Destroy(this.gameObject);
        }
    }

}
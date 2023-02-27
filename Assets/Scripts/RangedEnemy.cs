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
    //public Transform TF2Coconut;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    public float sightDistance = 10;
    private Collider2D finalDetected;
    private RaycastHit hit;
    private int layerMask = 1 << 3;
    

    //private bool sightLine;

    // Start is called before the first frame update
    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        layerMask = ~layerMask;
        //finalDetected = GameObject.Find("Wall").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        start = this.transform.position;
        cooldownCount++;
        direction = (target.transform.position - start).normalized;
        Debug.DrawRay(start, direction * sightDistance);

        if(SightTest() == target.GetComponent<Collider2D>())
        {
            if(cooldownCount >= cooldown)
            {
                print("Shoot!");
                Shoot();
                cooldownCount = 0;
            }
        }
        finalDetected = null;

    }

    public void Shoot()
    {
        //figure out how to add directional force
        GameObject arrow = Instantiate(Projectile, start, this.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(start * (shotForce * -1));
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
            //print(finalDetected);
        }
        return finalDetected;
    }
    
    

    public void enemyShoot()
    {

    }

    public void OnTriggerEnter2D( Collider2D other)
    {
        
        /*if(other.gameObject.CompareTag("Fire"))
        { 
            Destroy(other.gameObject);

            health = health -1;
            GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            
            if(health <= 0)
                {  Destroy(this.gameObject);
                    CurrentRoom.gameObject.SendMessage("RoomClear");
                }
        }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
            

            health = health -10;
            
            if(health <= 0)
                {  Destroy(this.gameObject);
                    
                }
        }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
            Destroy(this.gameObject);     
        }

        if(other.gameObject.CompareTag("Player"))
        {
           
            
           
            other.gameObject.SendMessage("EnemyCollide");
            

        }
        */
    }
}

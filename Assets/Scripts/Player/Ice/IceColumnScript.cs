using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceColumnScript : MonoBehaviour
{

    public GameObject shatter;
    public GameObject melt;
    public GameObject puddle;
    public float iceLifetime = 2f;
    private Rigidbody2D body;
    private Vector3 vel;
    private float speed;

    private GameObject stupidreciever;

    // Start is called before the first frame update
    void Start()
    {
        stupidreciever = GameObject.Find("RangedEnemy");
        body = this.GetComponent<Rigidbody2D>();
        GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, iceLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        vel = body.velocity;
        speed = vel.magnitude;
        //print(speed);
        if(speed > 1)
        {
            //print(speed);
        }
    }
    /*
    public void SendSpeed()
    {
        stupidreciever.SendMessage("RecieveSpeed", speed);
    }
    */

    public void OnTriggerEnter2D( Collider2D other)
    {
        if(other.gameObject.CompareTag("Fire") || other.gameObject.CompareTag("EnemyFire") || other.gameObject.CompareTag("BigFire"))
        {
            Instantiate(puddle, this.transform.position, this.transform.rotation);
            GameObject steam = Instantiate(melt, this.transform.position, melt.transform.rotation);
            Destroy(steam, 3f);
            this.gameObject.SendMessageUpwards("ColumnDestroyed", SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Earth") || other.gameObject.CompareTag("EnemyEarth"))
        {
            Instantiate(shatter, this.transform.position, this.transform.rotation);
            this.gameObject.SendMessageUpwards("ColumnDestroyed", SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);

        }

    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {

        print("Collide");
        if (other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("FILLERTEXT"))
        {
            if (speed > 5)
            {
                print("hit 3");
                other.gameObject.SendMessage("HurtMe", 3);
            }
            else if (speed >= 1)
            {
                print("hit 1");
                other.gameObject.SendMessage("HurtMe", 1);
            }
        }
    }
    
}


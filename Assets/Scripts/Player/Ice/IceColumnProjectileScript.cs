using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceColumnProjectileScript : MonoBehaviour
{

    //public GameObject shatter;
    //public GameObject melt;
    //public GameObject puddle;
    public float iceLifetime = 2f;
    private Rigidbody2D body;
    private Vector3 vel;
    private float speed;

    //private GameObject stupidreciever;

    // Start is called before the first frame update
    void Start()
    {
        //stupidreciever = GameObject.Find("RangedEnemy");
        body = this.GetComponent<Rigidbody2D>();
        //GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, iceLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        vel = body.velocity;
        speed = vel.magnitude;
        this.transform.position = transform.parent.transform.position;



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
        if(other.gameObject.CompareTag("Fire"))
        {
            //Instantiate(puddle, this.transform.position, this.transform.rotation);
            //GameObject steam = Instantiate(melt, this.transform.position, melt.transform.rotation);
            //Destroy(steam, 3f);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Earth"))
        {
            //Instantiate(shatter, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            //Destroy(other.gameObject);

        }
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


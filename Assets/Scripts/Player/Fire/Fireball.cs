using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float shootSpeed = 0f;
    public bool isBig;
    public bool grow;
    public float timer;
    public bool bigPush;
    public GameObject aim;
    Rigidbody2D r2d;
    public Vector2 direction;
    public bool Aimed;
    private GameObject fireE;


    void Start()
    {
        Aimed = false;
        aim = GameObject.Find("Shooter");
        grow = false;
        bigPush = true;
        isBig = false;
        r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,0f));
        //GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 2f);
    }

    public void GetAim(Vector2 other)
    {
        //print("other" + other);
        direction = other;
    }







    public void Update()
    {

        if(Aimed == false)
        {
            Aimed = true;
            aim.SendMessage("GetAim", this.gameObject);
            //aim.SendMessage("GetAim", SendMessageOptions.DontRequireReceiver);
                       
        }
        timer += Time.deltaTime;
        if(timer >= .4f && grow == false)
        {
            grow = true;
            bigPush = false;
        }



        if (bigPush == false && timer >= .4f)
        {
            //print("big");
            //print("direction " + direction);
            isBig = true;
            for(int i = 0; i <= 15; i++)
            {
                //print("speed" + direction);
                r2d.AddForce(direction, ForceMode2D.Impulse);
                //r2d.AddForce(new Vector2(shootSpeed, 10f));
            }

            bigPush = true;
            
        }
    }

    public void FireEnemyGetSize(GameObject other)
    {
        //print(isBig);
        other.SendMessage("SetFireballSize", isBig);
        //why do I have to do this stupid workaround to get what I want
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.GetComponent<Collider2D>());
        //make if statement to delete this if other is a wall
        if (other.gameObject.CompareTag("IceWall") || other.gameObject.CompareTag("EarthWall") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("FILLERTEXT") || other.gameObject.CompareTag("LightningEnemy") && !other.gameObject.CompareTag("FILLERTEXT"))
        {
            //calls HurtMe function, second argument is the damage value
            if (isBig)
            {
                other.gameObject.SendMessage("FireHurtMe", 2);
            }
            
        }
    }

}

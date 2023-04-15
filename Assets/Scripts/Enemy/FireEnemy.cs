using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    //Instantiated stuff
    public GameObject puddle;
    public GameObject steam;

    //HP stuff
    public int health;
    public int maxhealth;

    //used when hitting a wall
    public bool isLooking;
    public float lookingTimer;
    public float lookingTimeTotal;

    //universal timer
    public float timer;

    //Movement stuff
    public Vector3 looking;
    public bool isMoving;


    //fireball for damage/healing
    public GameObject fire;
    public bool isFireballBig;


    /////////////////////////////
    //End of Variable Declaration
    /////////////////////////////
    


    // Start is called before the first frame update
    void Start()
    {
        maxhealth = 4;
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    //stupid method to get whether fireball is big or not
    public void GetFireSize()
    {
        fire.SendMessage("FireEnemyGetSize", SendMessageOptions.DontRequireReceiver);

    }

    //part 2 of stupid method
    public void SetFireballSize(bool othersize)
    {
        isFireballBig = othersize;
    }






    public void HealMe(int toHeal)
    {
        health += toHeal;
        
        if(health > maxhealth)
        {
            print(health - maxhealth);
            this.gameObject.transform.localScale += new Vector3((float)((health - maxhealth)*.05), (float)((health - maxhealth)*.05), 0f);
        }
    }

    public void HurtMe(int ouchie)
    {
        health -= ouchie;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("EarthWall") || other.gameObject.CompareTag("Wall"))
        {
            isLooking = true;
            isMoving = false;
        }

        if (other.gameObject.CompareTag("IceWall"))
        {
            //Instantiate(puddle, other.transform.position, other.transform.rotation);
            Instantiate(steam, other.transform.position, steam.transform.rotation);
            Destroy(other.gameObject);
            HurtMe(1);
            this.gameObject.transform.localScale -= new Vector3(.15f, .15f, 0f);
        }
        
        if (other.gameObject.CompareTag("Ice"))
        {
            Instantiate(steam, other.transform.position, steam.transform.rotation);
            Destroy(other.gameObject);
            HurtMe(1);
            this.gameObject.transform.localScale -= new Vector3(.15f, .15f, 0f);
        }

        if (other.gameObject.CompareTag("Fire"))
        {
            GetFireSize();
            if (fire)
            {
                HealMe(2);
            }
            else
            {
                HealMe(1);
            }
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("BigFire"))
        {
            Destroy(other.gameObject);
            HealMe(4);
        }

        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
            HurtMe(1);
        }
        
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
            HurtMe(1);
        }

        if (other.gameObject.CompareTag("Earth"))
        {
            print("hurt");
            HurtMe(3);
            this.gameObject.transform.localScale -= new Vector3(.35f, .35f, 0f);
        }
    }


}

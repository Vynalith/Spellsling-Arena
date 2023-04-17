using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
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

    //Room
    public GameObject CurrentRoom;
    public GameObject heart;




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
        //print("Fire size called");
        fire.SendMessage("FireEnemyGetSize", this.gameObject);
    }

    //part 2 of stupid method
    public void SetFireballSize(bool othersize)
    {
        //print("Fire size set");
        isFireballBig = othersize;
    }


    
           

    ////////////////////////////////////
    ///Damage Methods
    ////////////////////////////////////
    public void HurtMe(int damage)
    {
        health -= damage;
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
        health += ouchie;


        if (health > maxhealth)
        {
            if (this.gameObject.transform.localScale.x <= (2f))
            {
                this.gameObject.transform.localScale += new Vector3((float)((health - maxhealth) * .05), (float)((health - maxhealth) * .05), 0f);

            }
        }


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
            fire = other.gameObject;
            //print("Fire detected");
            GetFireSize();
            //print("is fire big = " + isFireballBig);
            if (isFireballBig == false)
            {
                //print("small");
                FireHurtMe(1);
            }
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("BigFire"))
        {
            Destroy(other.gameObject);
            //FireHurtMe(4);
        }

        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
            //LightningHurtMe(1);
        }
        
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
            //LightningHurtMe(1);
        }

        if (other.gameObject.CompareTag("Earth"))
        {
            print("hurt");
            //HurtMe(3);
            this.gameObject.transform.localScale -= new Vector3(.35f, .35f, 0f);
        }
    }


}

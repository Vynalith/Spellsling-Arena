using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    Vector3 userDirection = Vector3.one;
    float movespeed = 1;
    float MoveTimer;
    bool flaring = false;
    Animator animator;
    float flaretime;
    int RandomX;
    int RandomY;


    SpriteRenderer SpriteRenderer;
    //Instantiated stuff
    GameObject puddle;
    GameObject steam;

    //HP stuff
    int health;
    int maxhealth;

    //used when hitting a wall
    bool isLooking;
    float lookingTimer;
    float lookingTimeTotal;

    //universal timer
    float timer;

    //Movement stuff
    Vector3 looking;
    bool isMoving;


    //fireball for damage/healing
    GameObject fire;
    bool isFireballBig;

    //Room
    GameObject CurrentRoom;
    GameObject heart;

    // Start is called before the first frame update
    void Start()
    {
        maxhealth = 4;
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(userDirection * movespeed * Time.deltaTime);
        
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        MoveTimer += Time.deltaTime;

        if(MoveTimer >= 3)
        {
            MoveTimer = 0;
            movespeed = 0;
            
            userDirection.x = 0;
            userDirection.y = 0;
            
            
            flaring = true;
            animator.Play("Flare up");
        }

        if(flaring == true)
        {
            flaretime += Time.deltaTime;
            if(flaretime >=1)
            {
                flaretime = 0;
                //int RandomX = Random.Range(-1,1);
                //int RandomY = Random.Range(-1,1);
                randomize();
                userDirection.x= RandomX;
                userDirection.y= RandomY;
                movespeed = 1;
                flaring = false;
            }
        }
    }
    void randomize()
    {
         RandomX = Random.Range(-1,1);
         RandomY = Random.Range(-1,1);
        animator.SetInteger("movementX", RandomX);
        animator.SetInteger("movementY", RandomY);
         
        if(RandomX ==0 && RandomY == 0)
        {
            randomize();
        }
    }


    //stupid method to get whether fireball is big or not
    void GetFireSize()
    {
        //print("Fire size called");
        fire.SendMessage("FireEnemyGetSize", this.gameObject);
    }

    //part 2 of stupid method
    void SetFireballSize(bool othersize)
    {
        //print("Fire size set");
        isFireballBig = othersize;
    }
    ////////////////////////////////////
    ///Damage Methods
    ////////////////////////////////////
    void HurtMe(int damage)
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


    void LightningHurtMe(int ouchie)
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
    void FireHurtMe(int ouchie)
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

    void IceHurtMe(int ouchie)
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
    void EarthHurtMe(int ouchie)
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

    void OnTriggerEnter2D(Collider2D other)
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
        
        if (other.gameObject.CompareTag("Earth"))
        {
            print("hurt");
            //HurtMe(3);
            this.gameObject.transform.localScale -= new Vector3(.35f, .35f, 0f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    public Vector3 userDirection = Vector3.one;
    public float movespeed = 1;
    public float MoveTimer;
    public bool flaring = false;
    public Animator animator;
    public float flaretime;
    public int RandomX;
    public int RandomY;


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
    public bool readyToMove;
    public Vector3 movement;


    //fireball for damage/healing
    public GameObject fire;
    public bool isFireballBig;

    //Room
    public GameObject CurrentRoom;
    public GameObject heart;

    //private Transform Death = (0f,0f,0f);


    /////////////////////////////
    //End of Variable Declaration
    /////////////////////////////
    


    // Start is called before the first frame update
    void Start()
    {
        maxhealth = 4;
        health = maxhealth;
        readyToMove = true;
        flaring = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (readyToMove)
        {
            movement = userDirection;
            movement.x = (1 / movement.x);
            movement.y = (1 / movement.y);

            //transform.Translate(userDirection * movespeed * Time.deltaTime);
            transform.Translate(movement * movespeed * Time.deltaTime);
            isMoving = true;
            MoveTimer += Time.deltaTime;
        }

        if(MoveTimer >= 1.5f)
        {
            readyToMove = false;
            isMoving = false;
            MoveTimer = 0;
            //movespeed = 0;
            
            userDirection.x = 0;
            userDirection.y = 0;
            
            
            flaring = true;
            animator.Play("Flare up");
        }

        if(flaring)
        {
            flaretime += Time.deltaTime;
            if(flaretime >= .5f)
            {
                flaretime = 0f;
                //int RandomX = Random.Range(-1,1);
                //int RandomY = Random.Range(-1,1);
                randomize();
                userDirection.x= RandomX;
                userDirection.y= RandomY;
                //movespeed = 1;
                flaring = false;
                print("Flaring is " + flaring);
                readyToMove = true;
                //MoveTimer = 0f;
            }
        }


        //this.transform.localscale.x <= 0

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    public void randomize()
    {
        
        RandomX = Random.Range(-10,10);
        RandomY = Random.Range(-10,10);

        //these two cause it to stop moving for some reason
        //animator.SetInteger("movementX", RandomX);
        //animator.SetInteger("movementY", RandomY);
         
        if(RandomX == 0 && RandomY == 0)
        {
            randomize();
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
            if (this.gameObject.transform.localScale.x <= (3f))
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



    public void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("EarthWall"))
        {
            movement.x *= -1;
            movement.y *= -1;
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
        
        if (other.gameObject.CompareTag("Puddle"))
        {
            Instantiate(steam, other.transform.position, steam.transform.rotation);
            Destroy(other.gameObject);
            HurtMe(2);
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

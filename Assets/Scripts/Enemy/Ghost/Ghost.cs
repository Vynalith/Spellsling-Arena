using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.MonoBehavior;

public class Ghost : MonoBehavior
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public Animator animator;
    public GameObject heart;


    ////////////////////////////////////////////
    ///PlayerAware values                    ///
    ////////////////////////////////////////////
    private Transform aim;
    private GameObject aimTarget;
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }
    [SerializeField]
    public float playerAwarenessDistance;
    private GameObject playertarget;


    ////////////////////////////////////////////
    ///GoopMovement values                   ///
    ////////////////////////////////////////////
    public Transform player;
    public GameObject dumbplayer;
    [SerializeField]
    private float speed;
    [SerializeField]

    //private float rotationSpeed = 100;
    public RigidBody2D OnTriggerEnter2D;
    //private PlayerAware ThisPlayerAware;
    private Vector2 target;
    public GameObject sprite;
    public GameObject anchor;

    ////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {

        ////////////////////////////////////////
        ///PlayerAware Code
        ////////////////////////////////////////
        playertarget = GameObject.Find("Aim");
        //print(playertarget);
        player = playertarget.transform;


        /////////////////////////////////////////
        ///GoopMovement Code
        /////////////////////////////////////////
        ///print("awake");
        dumbplayer = GameObject.Find("Player");
        player = dumbplayer.transform;
        anchor = GameObject.Find("EnemyAnchor");
        RigidBody = GetComponent<RigidBody2D>();
        //ThisPlayerAware = GetComponent<PlayerAware>();



        speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector;

        //print(enemyToPlayerVector);
        //print(enemyToPlayerVector.magnitude);
        
        

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            //print("Found player");
            AwareOfPlayer = true;
        }
        else
        {
            //print("Lost player");
            AwareOfPlayer = false;
        }
    }


    ///////////////////////////////////////////////
    ///Damage check
    ///////////////////////////////////////////////

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


    public void LightningHurtMe(int dmg)
    {
        health -= dmg + 1;

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

    public void FireHurtMe(int dmg)
    {
        health -= dmg;

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

    public void IceHurtMe(int dmg)
    {
        health -= dmg;

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

    public void EarthHurtMe(int dmg)
    {
        health -= dmg;

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

    public void OnTriggerEnter2D( Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Fire"))
        { 
            Destroy(other.gameObject);
            HurtMe(1);
            GameObject explore = Instantiate(damage, this.transform.position, Quaternion.identity);
            Destroy(explore, 1f);
            
            
        }
        if(other.gameObject.CompareTag("Ghost"))
        { 
                        
            if(health <= 0)
                {  
                    Destroy(this.gameObject);   
                }
        }
        if(other.gameObject.CompareTag("Earth"))
        {

        }
        if(other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);     
        }
        if(other.gameObject.CompareTag("Ice"))
        {
            Destroy(other.gameObject);     
        }

        if(other.gameObject.CompareTag("Player"))
        {
            
            animator.Play("GoopAttack");
            
            //other.gameObject.SendMessage("EnemyCollide");
            

        }
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
        sprite.transform.rotation = anchor.transform.rotation;
        
    }

    private void UpdateTargetDirection()
    {
        //print("UpdateTargetDirection");
        if (AwareOfPlayer)
        {
            TargetDirection = DirectionToPlayer;
        }
        else
        {
            TargetDirection = Vector2.zero;
        }
        //print("TargetDirection = " + TargetDirection);

    }

    private void RotateTowardsTarget()
    {
        //print("RotateTowardsTarget");
        if (TargetDirection == Vector2.zero)
        {
            //print("TargetDirection == Vector2.zero");
            return;
        }

        // Quaternion targetRotation = Quaternion.LookRotation(transform.player, TargetDirection);
        //Quaternion rotation = Quaternion.RotateTowards(player.transform.rotation, TargetDirection, rotationSpeed* Time.deltaTime);
        //RigidBody.transform.rotation = player.transform.rotation;
        //RigidBody.transform.rotation = sprite.transform.rotation;
    }

    private void SetVelocity()
    {
        //print("SetVelocity");
        if (TargetDirection == Vector2.zero)
        {
            //print("no direction");
            this.GetComponent<RigidBody2D>().velocity = Vector2.zero;
            
        }
        else
        {
            this.GetComponent<RigidBody2D>().velocity = transform.up * speed;
            //this.GetComponent<RigidBody2D>().AddForce(transform.up * speed);
            //print("transform.up = " + this.transform.up);
            //print("transform.up = " + transform.up);
            //print("speed = " + speed);
           // print("velocity = " + this.GetComponent<RigidBody2D>().velocity);
            //print("velocity should be = " + transform.up * speed);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ghost
{
    public GameObject health { get; }

    public GameObject damage { get; }

    public GameObject currentRoom { get; }
    public Animator animator { get; }
    public GameObject heart { get; }

    private readonly Transform aim1;

    ///PlayerAware values                    ///
    public GameObject aimTarget { get; }
    public GameObject CurrentRoom { get => currentRoom; set => currentRoom = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public GameObject Heart { get => heart; set => heart = value; }
    public System.Single GetplayerAwarenessDistance() => ghost.playerAwarenessDistance;
    public Vector2 DirectionToPlayer1 { get => directionToPlayer; set => directionToPlayer = value; }
    public GameObject Health1 { get => health1; set => health1 = value; }
    public global::System.Single Rotationspeed { get => rotationspeed; set => rotationspeed = value; }

    public GameObject Getplayertarget() => ghost.playertarget;

    ///GoopMovement values///
    public Transform player;
    public GameObject dumbplayer;
    [SerializeField];
    private float speed;
    [SerializeField];

    private float RigidBody2D OnTriggerEnter2D;
    //private PlayerAware ThisPlayerAware;
    private Vector2 target;
    public GameObject sprite;
    public GameObject anchor;
    private Vector2 directionToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ///PlayerAware Code
        Setplayertarget(GameObject.Find("Aim"));
        //print(playertarget);
        player = Getplayertarget().transform;


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
        //print(enemyToPlayerVector);
        //print(enemyToPlayerVector.magnitude);
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector; 
        
        if (enemyToPlayerVector.magnitude <= GetplayerAwarenessDistance())
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
        Health -= damage;
        if (Health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(Heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }


    public void LightningHurtMe(int dmg)
    {
        Health -= dmg + 1;

        if (Health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(Heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void FireHurtMe(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(Heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void IceHurtMe(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(Heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void EarthHurtMe(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(Heart, this.transform.position, Quaternion.identity);
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
                        
            if(Health <= 0)
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
            
            Animator.Play("GoopAttack");
            
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
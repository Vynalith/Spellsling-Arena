public class Ghost : MonoBehaviour
   {
    public GameObject damage;
    public GameObject currentRoom;

    private GameObject sprite;

    private GameObject PlayerAware;
    public Animator animator;
    public GameObject Enemyaim;
    public Vector2 layerawarenessDistance;
    public DirectionToPlayer;
    public GameObject Heart;
    public GameObject playertarget;
    public GameObject PlayerUI;

    ////////////////////////////////////////////
    ///GoopMovement values//////////////////////
    ////////////////////////////////////////////
    public Transform Player;

    public GameObject Player; [SerializeField];

    private float speed; [SerializeField];

    //private float rotationSpeed = 100;
    //private PlayerAware==ThisPlayerAware;
    private Vector2 targetdirection;
    public GameObject sprite;
    public GameObject EnemyAnchor;
    // Start is called before the first frame update
    void Start()
    {
        ////////////////////////////////////////
        ///PlayerAware Code
        ////////////////////////////////////////
        playertarget = GameObject.Find("Aim");
        
        player = playertarget.transform;

        /////////////////////////////////////////
        ///GhostMovement Code
        /////////////////////////////////////////
        ///print("awake");
        Player = GameObject.Find("Player");
        player = Player.transform;
        anchor = GameObject.Find("EnemyAnchor");
        playerAwarenessDistance = GetComponent<PlayerAware>();
        ThisPlayerAware = GetComponent<PlayerAware>();



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


    public void LightningHurtMe()
    {
        health -= ouchie + 1;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            Instantiate (heart, this.transform.position, Quaternion.identity);

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
            GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            
            
        }
        if(other.gameObject.CompareTag("FILLERTEXT"))
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
            targetdirection = DirectionToPlayer;
        }
        else
        {
            targetdirection = Vector2.zero;
        }
        //print("target direction = " + targetdirection);

    }

    private void RotateTowardsTarget()
    {
        //print("RotateTowardsTarget");
        if (targetdirection == Vector2.zero)
        {
            //print("targetdirection == Vector2.zero");
            return;
        }

        // Quaternion targetRotation = Quaternion.LookRotation(transform.foward, targetdirection);
        //Quaternion rotation = Quaternion.RotateTowards(player.transform.rotation, targetdirection, rotationSpeed* Time.deltaTime);
        //rigidbody.transform.rotation = player.transform.rotation;
        GetComponent<Rigidbody>().
    }

    private void SetVelocity()
    {
        //print("SetVelocity");
        if (targetdirection == Vector2.zero)
        {
            //print("no direction");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            //this.GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
            //print("transform.up = " + this.transform.up);
            //print("transform.up = " + transform.up);
            //print("speed = " + speed);
           // print("velocity = " + this.GetComponent<RigidBody2D>().velocity);
            //print("velocity should be = " + transform.up * speed);
        }
    }
}
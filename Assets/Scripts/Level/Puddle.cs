using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{

    public bool isElectrified;
    public GameObject particle;
    private bool zappersSpawned;
    public GameObject newIce;
    private GameObject zappers;
    

    //used to get more 
    private int playerpuddlecount;
    private int enemypuddlecount;
    private GameObject zappyboi;
    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        playerpuddlecount = 0;
        enemypuddlecount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isElectrified &! zappersSpawned)
        {
            print("is electrified and zappers is not spawned");
            zappersSpawned = true;
            zappers = Instantiate(particle, this.transform.position, this.transform.rotation);
        }

        if (isElectrified == false && zappersSpawned)
        {
            print("is not electrified and zappers spawned");
            zappersSpawned = false;
            Destroy(zappers);
        }


        if (isElectrified)
        {
            if (playerpuddlecount != 0)
            {
                playerpuddlecount++;
            }
            if (enemypuddlecount != 0)
            {
                enemypuddlecount++;
            }

            if (playerpuddlecount % 1000 == 0 && playerpuddlecount != 0)
            {
                //why does this work
                //why does this send to shooter instead of player
                //why
                //theres no reason
                //its stupid
                //im mad
                //and it still does more damage the more puddles there are
                //no
                //stop
                player.gameObject.SendMessage("PuddleHurtMe", 1);
            }

            

            if (zappyboi != null)
            {
                if (enemypuddlecount % 1000 == 0 && enemypuddlecount != 0)
                {   
                    print(zappyboi.tag);
                    zappyboi.SendMessage("HurtMe",SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lightning") || other.gameObject.CompareTag("BigLightning") || other.gameObject.CompareTag("EnemyLightning"))
        {
            
            if(isElectrified == false)
            {
                isElectrified = true;
            }
        }
        if (other.gameObject.CompareTag("EarthWall"))
        {
            if (zappersSpawned)
            {
                zappersSpawned = false;
                Destroy(zappers);
            }
            Destroy(this.gameObject);

        }
        if (other.gameObject.CompareTag("IceWall"))
        {
            if (zappersSpawned)
            {
                zappersSpawned = false;
                Destroy(zappers);
            }
            Instantiate(newIce, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (isElectrified)
            {
                enemypuddlecount = 1;
                zappyboi = other.gameObject;
                other.gameObject.SendMessage("LightningHurtMe", 1);
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if (isElectrified)
            {
                playerpuddlecount = 1;
                //other.gameObject.SendMessage("PuddleHurtMe", 1);
                other.gameObject.SendMessage("HurtMe", 1);
                player = other.gameObject;
            }
        }
        
       
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerpuddlecount = 0;
        }
        if (other.CompareTag("Enemy"))
        {
            enemypuddlecount = 0;
            zappyboi = null;
        }
    }
}

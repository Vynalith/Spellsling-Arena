using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningM2 : MonoBehaviour
{
    
    private List<GameObject> hitList = new List<GameObject>();
    //private List<GameObject> hitList = new List<GameObject>();

    //variables used for raycast, ignore
    /*
    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 20;
    private Collider2D finalDetected;
    private RaycastHit hit;
    private int layerMask = 1 << 7 | 1 << 9;
    
    private Vector3 newStart;
    */
    private int time;

    private bool hitWall = false;

    void Start()
    {
        hitWall = false;
        //can't do it this way, it'd only target the original and not the clones
        //target = GameObject.Find("RangedEnemy");
        //target2 = GameObject.Find("Enemy");
        //layerMask = ~layerMask;
        //GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 1f);

        //code for raycast, ignore
        /*
        GameObject player = GameObject.Find("Player");
        start = player.transform.position;
        print(start);
        GameObject aim = GameObject.Find("Aim");
        print(aim);
        direction = (aim.transform.position - start).normalized;
        print(direction);

        
        if (SightTest() == )
        {
            
        }
        finalDetected = null;
        
        //Debug.DrawRay(start, direction * sightDistance * 10, Color.red, 5f);
        //print(SightTest());
        */
        time = 0;
        
    }

    void Update()
    {
        if(time < 8)
        {
            time += 1;
        }


        if(time == 6)
        {
            //loops for each gameobject in hitlist
            for (int x = 0; x < hitList.Count; x++)
            //for (int x = hitList.Count - 1; x > -1; x--)
            {
                //print("called loop");
                //print("x = " + x);
                print("list x = " + hitList[x]);
                //print("Hitwall says " + hitWall);
                //if a wall is hit, SUPPOSED TO NOT go through this loop
                //does it anyway for some reason
                //but only with level walls, not player created walls
                //something to do with how they're built?
                
                if (hitWall == false)
                {
                    //print("says its false");
                    //if the gameobject in index x of hitList is a wall, set hitWall to true
                    if (hitList[x].gameObject.CompareTag("IceWall") || hitList[x].gameObject.CompareTag("EarthWall") || hitList[x].gameObject.CompareTag("Wall"))
                    {
                        //print("hitWall set to true");
                        hitWall = true;
                    }
                    //if the gameobject in index x of hitList is an enemy, 
                    else if (hitList[x].gameObject.CompareTag("Enemy") && hitWall == false)
                    {
                        //print("hit");
                        hitList[x].gameObject.SendMessage("LightningHurtMe", 2);
                    }
                }
                
            }
        }
        
        //print(SightTest());
        //Debug.DrawRay(start, direction * sightDistance, Color.red, 5f);
        //print("update");
        //just screw all of this, it's just going to hit everything through walls
        //been at this for almost 5 hours now and made zero progress on getting it to work like I want
    }

    
    public void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.GetComponent<Collider2D>());
        //make if statement to delete this if other is a wall
        if(time <= 5)
        {
            if (other.gameObject.CompareTag("IceWall") || other.gameObject.CompareTag("EarthWall") || other.gameObject.CompareTag("Wall"))
            {
                print("adding wall");
                hitList.Add(other.gameObject);
            }
            if (other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("FILLERTEXT"))
            {
                print("adding " + other.gameObject);
                hitList.Add(other.gameObject);
            }
            print(hitList.Count);
        }
     
    }
    





    // || sightTest.collider.gameObject.CompareTag("Wall")

    //IGNORE BELOW
    /*
    public Collider2D SightTest(float start, float distance, gameObject origin)
    {
        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, distance, layerMask);
        if (sightTest.collider != null)
        {
            if (sightTest.collider.gameObject != gameObject)
            {
                finalDetected = null;
                print("Rigidbody collider is: " + sightTest.collider);
            }
            finalDetected = sightTest.collider;
            sightDistance -= Distance(sightTest.collider.gameObject.transform.position, origin.transform.position);
        }
        return finalDetected;
    }
    */
    //maybe save location of sightTEst collision to new variable, subtract distance of it from the total sighTDistance, then make a new raycast?

}

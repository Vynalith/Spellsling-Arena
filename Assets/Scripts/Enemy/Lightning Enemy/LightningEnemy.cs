using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEnemy : MonoBehaviour
{
    public GameObject[] Waypoints;
    public int currentWaypoint;
    private float randomStart;

    public float timer;
    public float movementCooldown = 5f;
    public float movementTimer;



    // Start is called before the first frame update
    void Start()
    {
        randomStart = Mathf.Round(Random.Range(0f, Waypoints.Length - 1));
        currentWaypoint = Mathf.FloorToInt(randomStart);
        this.transform.position = Waypoints[currentWaypoint].transform.position;


    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        if(movementTimer < movementCooldown)
        {
            movementTimer += Time.deltaTime;
        }
        else
        {
            if(currentWaypoint >= Waypoints.Length - 1)
            {
                currentWaypoint = 0;
            }
            else
            {
                currentWaypoint++;
            }
            this.transform.position = Waypoints[currentWaypoint].transform.position;
            movementTimer = 0f;
        }
    }
}

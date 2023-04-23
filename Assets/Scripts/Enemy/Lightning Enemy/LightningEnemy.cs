using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEnemy : MonoBehaviour
{
    public GameObject[] Waypoints;
    public int currentWaypoint;
    private float randomStart;
    public GameObject lastWaypoint;
    public GameObject nextWaypoint;

    public GameObject laserBeam;

    public float timer;
    public float movementCooldown = 5f;
    public float movementTimer;

    public int health;
    public int maxhealth;
    public GameObject CurrentRoom;
    public GameObject heart;




    // Start is called before the first frame update
    void Start()
    {
        randomStart = Mathf.Round(Random.Range(0f, Waypoints.Length - 1));
        currentWaypoint = Mathf.FloorToInt(randomStart);
        this.transform.position = Waypoints[currentWaypoint].transform.position;
        maxhealth = 4;
        health = maxhealth;

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

            ///////////////////////////////////
            ///Saving waypoints
            ///////////////////////////////////
            if(currentWaypoint == 0)
            {
                lastWaypoint = Waypoints[Waypoints.Length - 1];
            }
            else
            {
                lastWaypoint = Waypoints[currentWaypoint - 1];
            }
            nextWaypoint = Waypoints[currentWaypoint];

            //Vector3 targetDirection = (nextWaypoint.transform.position - lastWaypoint.transform.position).normalized;
            //Vector3 newDirection = Vector3.RotateTowards()
            //(target.transform.position - start).normalized;
            //Quaternion target = Quaternion.Euler(targetDirection);
            GameObject zap = Instantiate(laserBeam, (lastWaypoint.transform.position + (nextWaypoint.transform.position - lastWaypoint.transform.position)/2), Quaternion.identity);

            //print(targetDirection);
            //the

            //zap.transform.rotation
            
            //zap.Quaternion.RotateTowards(nextWaypoint.transform.position);
        }


        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }





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
        health += ouchie;

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


}

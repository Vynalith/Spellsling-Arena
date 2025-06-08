using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int roomCount;
    GameObject currentPlacer;
    string combination;
    

    // Start is called before the first frame update
    void Start()
    {
        roomCount = 0;

        for(int i = 1; i < 7; i++)
        {
            combination = "Placer (" + i + ")";
            currentPlacer = GameObject.Find(combination);
            print("CurrentPlacer = " + currentPlacer);
            //print("Combination = " + combination);
            int RandomRoom = Random.Range(0, 5);


            if(i < 6)
            {
                currentPlacer.SendMessage("CreateRoom", RandomRoom);
            }
            else
            {
                currentPlacer.SendMessage("CreateBossRoom", SendMessageOptions.DontRequireReceiver);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("Game Manager Room Count is " + roomCount);
    }

    void IncreaseRoomCount()
    {
        roomCount++;
        //print("Room count increased to " + roomCount);

    }


    void SendRoomCount(GameObject other)
    {
        //print("Recieved room count from " + other);
        //print("Sending roomcount " + roomCount + " to " + other);
        other.SendMessage("GetRoomCount", roomCount);
    }
}
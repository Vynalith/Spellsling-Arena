using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int roomCount;
    public GameObject currentPlacer;
    public string combination;
    //public Vector3[] Offsets;
    public Vector3 offsetTotal;
    private bool needsOffset;
    private int whichOffset;


    //offsets declared here because unity is stupid
    public Vector3 lightningLevel1 = new Vector3(0f, 18.17f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        roomCount = 0;

        for(int i = 1; i < 10; i++)
        {
            combination = "Placer (" + i + ")";
            currentPlacer = GameObject.Find(combination);
            print("Current Placer = " + currentPlacer);
            //print("Combination = " + combination);
            int RandomRoom = Random.Range(0, 6);
            print("Random Room = " + RandomRoom);
            if(RandomRoom == 5)
            {
                needsOffset = true;
                whichOffset = 1;
                print("Random Room = 6 Bool is true");
                print("Lightning level offset = " + lightningLevel1);
                currentPlacer.SendMessage("GetOffset", offsetTotal);
                print("Offset total = " + offsetTotal);

            }
            if (i < 6)
            {
                //currentPlacer.SendMessage("GetOffset", offsetTotal);
                currentPlacer.SendMessage("SetPosition", offsetTotal);
                currentPlacer.SendMessage("CreateRoom", RandomRoom);

            }
            else
            {
                currentPlacer.SendMessage("SetPosition", offsetTotal);
                currentPlacer.SendMessage("CreateBossRoom", SendMessageOptions.DontRequireReceiver);
            }

            if (needsOffset)
            {
                switch (whichOffset)
                {
                    case 1:
                        offsetTotal += lightningLevel1;
                        needsOffset = false;
                        whichOffset = 0;
                        break;
                }

                }
            }



    }

    // Update is called once per frame
    void Update()
    {
        //print("Game Manager Room Count is " + roomCount);
    }

    public void IncreaseRoomCount()
    {
        roomCount++;
        //print("Room count increased to " + roomCount);

    }


    public void SendRoomCount(GameObject other)
    {
        //print("Recieved room count from " + other);
        //print("Sending roomcount " + roomCount + " to " + other);
        other.SendMessage("GetRoomCount", roomCount);
    }




}

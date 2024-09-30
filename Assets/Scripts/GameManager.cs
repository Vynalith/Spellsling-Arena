using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int roomCount;
    public GameObject currentPlacer;
    public string combination;
    public Vector3 offsetTotal;
    private bool needsOffset;
    private int whichOffset;

    public Vector3 lightningLevel1;
    public Vector3 mudLevel1;
    public Vector3 cornerLevel1;

    void Start()
    {
        roomCount = 0;

        for (int i = 1; i < 7; i++)
        {
            combination = "Placer (" + i + ")";
            currentPlacer = GameObject.Find(combination);
            Debug.Log("Current Placer = " + currentPlacer);

            int RandomRoom = Random.Range(0, 8);
            Debug.Log("Random Room = " + RandomRoom);

            switch (RandomRoom)
            {
                case 5:
                    needsOffset = true;
                    whichOffset = 1;
                    Debug.Log("Lightning level offset = " + lightningLevel1);
                    break;
                case 6:
                    needsOffset = true;
                    whichOffset = 2;
                    Debug.Log("Mud level offset = " + mudLevel1);
                    break;
                case 7:
                    needsOffset = true;
                    whichOffset = 3;
                    Debug.Log("Corner level offset = " + cornerLevel1);
                    break;
            }

            if (i < 6)
            {
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
                        break;
                    case 2:
                        offsetTotal += mudLevel1;
                        break;
                    case 3:
                        offsetTotal += cornerLevel1;
                        break;
                }
                needsOffset = false;
                whichOffset = 0;
            }
        }
    }

    void Update()
    {
        // Optional: Add any update logic here
    }

    public void IncreaseRoomCount()
    {
        roomCount++;
        Debug.Log("Room count increased to " + roomCount);
    }

    public void SendRoomCount(GameObject other)
    {
        Debug.Log("Sending room count " + roomCount + " to " + other);
        other.SendMessage("GetRoomCount", roomCount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject bossRoom;
    public Vector3 original;
    public Vector3 offset;
    public int roomCount;
    public GameObject ui;

    void Start()
    {
        // Initialization if needed
    }

    public void GetRoomCount(int count)
    {
        roomCount = count;
        ui.SendMessage("IncreaseRoomCount", SendMessageOptions.DontRequireReceiver);
    }

    public void CreateRoom(int randomRoom)
    {
        Vector3 roomPosition = transform.position + offset;
        Debug.Log(gameObject + " Placing Room at " + roomPosition);
        Instantiate(rooms[randomRoom], roomPosition, Quaternion.identity);
    }

    public void CreateBossRoom()
    {
        Vector3 roomPosition = transform.position + offset;
        Instantiate(bossRoom, roomPosition, Quaternion.identity);
    }

    public void GetOffset(Vector3 newOffset)
    {
        offset += newOffset;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position += newPosition;
        Debug.Log(gameObject + " Setting Position to " + transform.position);
    }

    void Update()
    {
        // Update logic if needed
    }
}

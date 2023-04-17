using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject bossRoom;
    public Vector3 original;
    public Vector3 offset;
    public Transform finalposition;
    public int roomCount;
    public GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        

        /*
        ui = GameObject.Find("GameManager");

        roomCount = 0;
        //print("Sending room count to Game Manager from " + this.gameObject);
        //this is the receiver error
        ui.SendMessage("SendRoomCount", this.gameObject);
        //print("Room count is " + roomCount);
        if(roomCount < 5)
        {  
            //print("Creating regular room...");

            original = this.transform.position;
            int RandomRoom = Random.Range(0, 5);



            if (RandomRoom == 6)
            {
                offset = new Vector3(10f, 4f, 0f);
                //original = this.transform.position;
            }





            Instantiate(rooms[RandomRoom], (original + offset), Quaternion.identity);
        }
        else if(roomCount > 5)
        {
            //offset = new Vector3(10f, 4f, 0f);
            //print("Room Count is " + roomCount);
            //print("Creating boss room...");

            Instantiate(bossRoom, original + offset, Quaternion.identity); 
        }



        
        
        //ui.SendMessage("IncreaseRoomCount", SendMessageOptions.DontRequireReceiver);
        //print("Sending increase room count to Game Manager");
        */
    }


    public void GetRoomCount(int count)
    {
        //print("Setting placer roomCount to Game Manager");
        roomCount = count;
        ui.SendMessage("IncreaseRoomCount", SendMessageOptions.DontRequireReceiver);
    }


    public void CreateRoom(int randomRoom)
    {
        original = this.transform.position;
        print(original);
        Instantiate(rooms[randomRoom], (original), Quaternion.identity);
    }


    public void CreateBossRoom()
    {
        original = this.transform.position;
        Instantiate(bossRoom, original + offset, Quaternion.identity);
    }


    public void GetOffset(Vector3 newOffset)
    {
        offset += newOffset;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

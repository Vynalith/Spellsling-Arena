using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int enemies;
    public GameObject Entrance;
    public GameObject Exit;



    void Start()
    {
        Entrance.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
    }


    public void RoomLock()
    {
        Entrance.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
    }
    
    
    
    public void RoomClear()
    {
        //print ("enemy down");
        enemies= enemies-1;
        if(enemies<=0)
        {
            



            Entrance.gameObject.SetActive(false);
            Exit.gameObject.SetActive(false);
        }
    }

     /* void Update()
    {
        if(enemies<=0)
        {
            Entrance.gameObject.SetActive(false);
            Exit.gameObject.SetActive(false);
        }
    } */

}

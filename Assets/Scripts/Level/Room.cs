using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int enemies;
    public GameObject Entrance;
    public GameObject Exit;
    private int count;
    private int countdown;
    public GameObject[] Enemies;

     public int enter;

    void Start()
    {
        Entrance.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        enter= 0;
    }


    public void RoomLock()
    {
        if(enter < 1)
        {
        Entrance.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);
        enter = enter+1;

            for(int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].SetActive(true);
            }

        }
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

      void Update()
    {
        
    } 

}

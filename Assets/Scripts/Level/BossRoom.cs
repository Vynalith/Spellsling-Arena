using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    public int enemies;
    public GameObject Entrance;
    public GameObject Exit;
    private int count;
    private int countdown;
    public GameObject[] Enemies;
    private GameObject player;
     public int enter;

    void Start()
    {
        Entrance.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        enter= 0;
        player = GameObject.Find("Player");
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

            player.SendMessage("StartBossMusic", SendMessageOptions.DontRequireReceiver);

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

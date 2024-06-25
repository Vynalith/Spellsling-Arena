using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    int enemies;
    GameObject Entrance;
    GameObject Exit;
    int count;
    int countdown;
    GameObject[] Enemies;
    GameObject player;
    int enter;

    void Start()
    {
        Entrance.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);
        enter= 0;
        player = GameObject.Find("Player");
    }

    void Update()
    {
    void RoomLock()
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
    }
    void RoomClear()
    {
        //print ("enemy down");
        enemies= enemies-1;
        if(enemies<=0)
        {
            Entrance.gameObject.SetActive(false);
            Exit.gameObject.SetActive(false);
        }
    }
}
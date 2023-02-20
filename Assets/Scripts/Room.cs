using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int enemies;
    public GameObject Entrance;
    public GameObject Exit;

    public void RoomClear()
    {
        enemies= enemies-1;
        if(enemies<=0)
        {
            Entrance.gameObject.SetActive(false);
            Exit.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocker : MonoBehaviour
{
    public GameObject Room;
    public int enter;
    // Start is called before the first frame update
    void Start()
    {
        enter=0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit2D( Collider2D other)
    {
        if(enter < 1)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                Room.gameObject.SendMessage("RoomLock");
                
            }
        }
    }
}
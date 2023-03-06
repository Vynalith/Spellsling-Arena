using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
public GameObject[] rooms;


    // Start is called before the first frame update
    void Start()
    {
         int RandomRoom = Random.Range(0,10);

        Instantiate(rooms[RandomRoom], this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

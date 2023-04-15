using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
public GameObject[] rooms;

    public Vector3 original;
    public Vector3 offset;
    public Transform finalposition;

    // Start is called before the first frame update
    void Start()
    {

        original = this.transform.position;
         int RandomRoom = Random.Range(0,5);



        if(RandomRoom == 6)
        {
            offset = new Vector3(10f, 4f, 0f);
            //original = this.transform.position;
        }





        Instantiate(rooms[RandomRoom], (original + offset), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

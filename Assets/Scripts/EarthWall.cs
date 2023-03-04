using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnTriggerEnter2D( Collider2D other)
    {
        if(other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
        }
            if (other.gameObject.CompareTag("Earth"))
                {

                    Destroy(other.gameObject);
                   
                }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
                
        }
    }
}

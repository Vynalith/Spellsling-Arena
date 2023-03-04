using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceColumnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2f);
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
            Destroy(this.gameObject);
        }
            if (other.gameObject.CompareTag("Earth"))
                {

                    Destroy(other.gameObject);
                   
                }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
            Destroy(this.gameObject);     
        }
    }
    }


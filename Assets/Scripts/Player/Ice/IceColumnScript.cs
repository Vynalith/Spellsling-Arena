using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceColumnScript : MonoBehaviour
{

    public GameObject shatter;
    public GameObject melt;
    public GameObject puddle;
    public float iceLifetime = 2f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, iceLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D( Collider2D other)
    {
        if(other.gameObject.CompareTag("Fire"))
        {
            Instantiate(puddle, this.transform.position, this.transform.rotation);
            GameObject steam = Instantiate(melt, this.transform.position, melt.transform.rotation);
            Destroy(steam, 3f);
            Destroy(this.gameObject);
        }
            if (other.gameObject.CompareTag("Earth"))
                {
                    Instantiate(shatter, this.transform.position, this.transform.rotation);
                    Destroy(this.gameObject);
                   
                }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
            Destroy(this.gameObject);     
        }
    }
    }


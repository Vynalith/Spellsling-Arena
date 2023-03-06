using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour
{

    public GameObject shatter;
    public float lifeTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
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
                    GameObject explo = Instantiate(shatter, this.transform.position, Quaternion.identity);
                    Destroy(explo, 1f);
                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                   
                }
        if(other.gameObject.CompareTag("FILLERTEXT"))
        { 
                
        }
    }
}

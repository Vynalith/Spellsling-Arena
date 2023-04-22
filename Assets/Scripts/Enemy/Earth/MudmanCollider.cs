using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudmanCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        print("Collided with " + other);
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
            this.gameObject.SendMessageUpwards("FireHurtMe", 1);
        }
        if (other.gameObject.CompareTag("Earth"))
        {
            this.gameObject.SendMessageUpwards("EarthHurtMe", 2);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Ice"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BigFire"))
        {
            this.gameObject.SendMessageUpwards("FireHurtMe", 2);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("BigLightning"))
        {
            this.gameObject.SendMessageUpwards("LightningHurtMe", 1);
            Destroy(other.gameObject);
        }



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthM1 : MonoBehaviour
{
    public float shootSpeed = 100f;
    private GameObject spook;
    private GameObject spoop;
    
    void Start()
    {
        spook = GameObject.Find("Ghost");
        spoop = GameObject.Find("Ghost Spectra");
        Rigidbody2D r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,10f));
        //GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, .4f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.GetComponent<Collider2D>());
        //make if statement to delete this if other is a wall
        if (other.gameObject.CompareTag("IceWall") || other.gameObject.CompareTag("EarthWall") || other.gameObject.CompareTag("Wall"))
        {
            //Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("FILLERTEXT"))
        {
                            //calls HurtMe function, second argument is the damage value
            if(other.gameObject != spook || other.gameObject != spoop)
            {
                other.gameObject.SendMessage("EarthHurtMe", 3);

            }
        }
    }
}

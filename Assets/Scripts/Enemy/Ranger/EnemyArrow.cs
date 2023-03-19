using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public float shootSpeed = 100f;
    
    void Start()
    {
        Rigidbody2D r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,0f));
        GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 2f);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.GetComponent<Collider2D>());
        //make if statement to delete this if other is a wall
        if(other.gameObject.CompareTag("IceWall") || other.gameObject.CompareTag("EarthWall") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

        
    }
    }

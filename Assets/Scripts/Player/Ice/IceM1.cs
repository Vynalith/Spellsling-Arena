using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceM1 : MonoBehaviour
{
    public float shootSpeed = 100f;
    private GameObject start;
    private GameObject target;
    private Vector2 aim;
    
    void Start()
    {
        target = GameObject.Find("Aim");
        start = GameObject.Find("Player");
        aim = target.transform.position - start.transform.position;
        Rigidbody2D r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,0f));
        GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 1f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.GetComponent<Collider2D>());
        //make if statement to delete this if other is a wall
        /*
        if (other.gameObject.CompareTag("IceWall") || other.gameObject.CompareTag("EarthWall") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            //calls HurtMe function, second argument is the damage value
            other.gameObject.SendMessage("HurtMe",2);
        }
        */
        if(other.gameObject.CompareTag("IceWall") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyProjectile"))
        {
            //print("applying force in direction " + aim * 100 + "to object " + other.gameObject);
            Rigidbody2D nerd = other.GetComponent<Rigidbody2D>();
            //print("collider = " + nerd);
            nerd.AddForce(aim * 10f, ForceMode2D.Impulse);
            Destroy(this.gameObject);
        }
    }
}

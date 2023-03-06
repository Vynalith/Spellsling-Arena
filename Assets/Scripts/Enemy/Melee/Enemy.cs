using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtMe(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");

        }
    }

    public void OnTriggerEnter2D( Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
            GameObject explo = Instantiate(damage, this.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
        if (other.gameObject.CompareTag("Earth"))
                {
                    Destroy(other.gameObject);
                }
        if(other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Lightning"))
        {
            //maybe do something like this.addforce(this.transform.position - other.transform.position) to push away from player?

            Destroy(other.gameObject);
        }

        
    }
}

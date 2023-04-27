using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public GameObject heart;
    public SpriteRenderer renderer;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        renderer.color = new Color(0f, 0f, 0f, 1f);
    }

    ///////////////////////////////////////////////
    ///Damage check
    ///////////////////////////////////////////////

    public void HurtMe(int damage)
    {
        health -= damage;
        renderer.color = new Color(0f, 28f, 0f, 1f);
        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }


    public void LightningHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void FireHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void IceHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void EarthHurtMe(int ouchie)
    {
        health -= ouchie;

        if (health <= 0)
        {
            int heartOrNo = Random.Range(0, 4);

            print(heartOrNo);
            //Instantiate (heart, this.transform.position, Quaternion.identity);

            if (heartOrNo >= 2)
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

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

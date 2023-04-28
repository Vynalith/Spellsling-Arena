using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEarth : MonoBehaviour
{
    public float shootSpeed = 100f;
    public GameObject shatter;
    public GameObject iceshatter;
    public GameObject fss;
    public GameObject zappy;
    public GameObject damageEffect;


    public int health;
    public int maxhealth;
    public GameObject CurrentRoom;
    public GameObject heart;


    void Start()
    {
        Rigidbody2D r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,10f));
        //GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, .4f);
        health = maxhealth;
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("HurtMe", 2);
        }

        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
            GameObject firepart = Instantiate(fss, this.transform.position, Quaternion.identity);
        }

        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject);
            GameObject zap = Instantiate(zappy, this.transform.position, Quaternion.identity);
        }

        if (other.gameObject.CompareTag("Ice"))
        {
            Destroy(other.gameObject);
            GameObject icy = Instantiate(iceshatter, this.transform.position, Quaternion.identity);
        }


        if (other.gameObject.CompareTag("Earth") || other.gameObject.CompareTag("BigFire") || other.gameObject.CompareTag("BigLightning"))
        {
            print("boom");
            GameObject explo = Instantiate(shatter, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


    public void HurtMe(int damage)
    {
        Instantiate(damageEffect, this.transform.position, this.transform.rotation);

        health -= damage;
        if (health <= 0)
        {
            //Instantiate(WIN, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }


    public void LightningHurtMe(int ouchie)
    {
        health -= ouchie;
        Instantiate(damageEffect, this.transform.position, this.transform.rotation);

        if (health <= 0)
        {
            //Instantiate(WIN, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void FireHurtMe(int ouchie)
    {
        health -= ouchie;
        Instantiate(damageEffect, this.transform.position, this.transform.rotation);

        if (health <= 0)
        {
            //Instantiate(WIN, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void IceHurtMe(int ouchie)
    {
        health -= ouchie;
        Instantiate(damageEffect, this.transform.position, this.transform.rotation);

        if (health <= 0)
        {
            //Instantiate(WIN, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void EarthHurtMe(int ouchie)
    {
        health -= ouchie;
        Instantiate(damageEffect, this.transform.position, this.transform.rotation);

        if (health <= 0)
        {
            //Instantiate(WIN, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

}

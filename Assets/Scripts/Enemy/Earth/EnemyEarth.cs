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

    void Start()
    {
        Rigidbody2D r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,10f));
        //GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, .4f);
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
}

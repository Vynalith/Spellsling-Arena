using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireM2 : MonoBehaviour
{
    public float shootSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
         Rigidbody2D r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,0f));

        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("LightningEnemy") && !other.gameObject.CompareTag("FILLERTEXT"))
        {
            //calls HurtMe function, second argument is the damage value
            other.gameObject.SendMessage("FireHurtMe", 3);
        }
    }
}

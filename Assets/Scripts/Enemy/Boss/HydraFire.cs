using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraFire : MonoBehaviour
{
    float shootSpeed = 100f;
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
    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("HurtMe", 2);
        }
    }
}

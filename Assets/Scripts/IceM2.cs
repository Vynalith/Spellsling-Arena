using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceM2 : MonoBehaviour
{
    public float shootSpeed = 100f;
    public GameObject iceWall;
    // Start is called before the first frame update
    void Start()
    {
         Rigidbody2D r2d = this.GetComponent<Rigidbody2D>();
        r2d.AddForce(new Vector2(shootSpeed,0f));

        //Instantiate(iceWall, this.transform.position);
        Destroy(this.gameObject, 1f);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


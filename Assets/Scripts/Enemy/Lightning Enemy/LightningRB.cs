using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRB : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTowards (Vector3 newDirection)
    {
        print("Move recieved, adding force in direction " + newDirection);
        rb.AddForce(newDirection * moveSpeed);
    }




}

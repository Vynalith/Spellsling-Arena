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
        // Ensure the Rigidbody2D component is assigned
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Method to move the Rigidbody2D in the specified direction
    public void MoveTowards(Vector3 newDirection)
    {
        Debug.Log("Move received, adding force in direction " + newDirection);
        rb.AddForce(newDirection * moveSpeed);
    }
}

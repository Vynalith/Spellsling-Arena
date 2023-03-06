using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    
    public float speed;

    Vector2 movement;
    public Rigidbody2D rb;

    public Transform ShooterThing;
    public GameObject Projectile;
    public GameObject Aim;

    public GameObject re;
    private Vector3 direction;


    public float shotForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //direction.x = re.SendMessage("GetXDirection", SendMessageOptions.DontRequireReceiver);
        
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");


        //attack
        /*
        if( Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
        */
    }

    void FixedUpdate()
    {
        //movement
        //rb.MovePosition(rb.position + movement * speed *Time.fixedDeltaTime);
        
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(Projectile, Aim.transform.position, ShooterThing.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(ShooterThing.up * shotForce, ForceMode2D.Impulse);
    }
    
}

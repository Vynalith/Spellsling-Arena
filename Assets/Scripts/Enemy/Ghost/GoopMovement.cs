using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopMovement : MonoBehaviour
{
public Transform player;

    /*

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    public Rigidbody2D rigidbody;
    private PlayerAware ThisPlayerAware;
    private Vector2 targetdirection;

    public GameObject sprite;
    public GameObject anchor;

    void Start()
    {
        anchor = GameObject.Find("EnemyAnchor");
    }
    // Start is called before the first frame update
    void Awake()
    {
       rigidbody = GetComponent<Rigidbody2D>();
       ThisPlayerAware = GetComponent<PlayerAware>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sprite.transform.rotation = anchor.transform.rotation;
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (ThisPlayerAware.AwareOfPlayer)
        {
            targetdirection = ThisPlayerAware.DirectionToPlayer;
        }
        else
        {
            targetdirection = Vector2.zero;
        }
    }

     private void RotateTowardsTarget()
    {
        if (targetdirection == Vector2.zero)
        {
            return;
        }

       // Quaternion targetRotation = Quaternion.LookRotation(transform.foward, targetdirection);
        //Quaternion rotation = Quaternion.RotateTowards(player.transform.rotation, targetdirection, rotationSpeed* Time.deltaTime);
        rigidbody.transform.rotation = player.transform.rotation;
    }

     private void SetVelocity()
    {
        if(targetdirection == Vector2.zero)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else{
            GetComponent<Rigidbody2D>().velocity = transform.up*speed;
        }
    }
    */
}

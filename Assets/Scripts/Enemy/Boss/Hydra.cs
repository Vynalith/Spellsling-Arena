using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydra : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public float cooldown;
    private float cooldownCount;
    public GameObject[] Projectile;
    public Animation[] attack;
    public float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 10;
    private Collider2D finalDetected;
    private RaycastHit2D hit;
    private int layerMask = 1 << 3;

    private Vector3 shootAngle;

    public Animator animator;

    public Transform anchor;
    public GameObject anchor;

    public GameObject WIN;

    private int Hydradamage;

    // Start is called before the first frame update
    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        target2 = GameObject.Find("Shooter");
        layerMask = ~layerMask;
        anchor = GameObject.Find("EnemyAnchor");
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Update health and damage variables if necessary

        // If the hydra is in sight range of the player
        if (Vector3.Distance(transform.position, target.transform.position) <= sightDistance)
        {
            // Shoot projectile
            Shoot();
        }

        // Check for win condition
        if (health <= 0)
        {
            WIN.SetActive(true);
        }
    }

    void Shoot()
    {
        // If cooldown has passed
        if (cooldownCount <= 0)
        {
            // Play shooting animation
            animator.Play("Attack");

            // Shoot projectile towards the player
            int index = Random.Range(0, Projectile.Length);
            GameObject shot = Instantiate(Projectile[index], anchor.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().AddForce(direction * shotForce);

            // Reset cooldown
            cooldownCount = cooldown;
        }
        else
        {
            // Decrement cooldown
            cooldownCount -= Time.deltaTime;
        }
    }

    // This function handles damage when the hydra is hit by a projectile
    public void TakeDamage(int damage)
    {
        health -= damage;
        // Instantiate damage text and play damage sound here
    }
}
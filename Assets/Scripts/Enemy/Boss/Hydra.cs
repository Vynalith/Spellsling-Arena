using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydra : MonoBehaviour
{
    int health;
    GameObject damage;
    GameObject CurrentRoom;
    float cooldown;
    float cooldownCount;
    GameObject[] Projectile;
    Animation[] attack;
    float shotForce = 20f;

    Vector3 start;
    Vector3 direction;
    GameObject target;
    GameObject target2;
    float sightDistance = 10;
    Collider2D finalDetected;
    RaycastHit2D hit;
    int layerMask = 1 << 3;

    Vector3 shootAngle;
    
    Animator animator;

    Transform anchor;
    
    GameObject WIN;

    int Hydradamage;

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
        // This function handles damage when the hydra is hit by a projectile
        void TakeDamage(int damage)
        {
            health -= damage;
            // Instantiate damage text and play damage sound here
        }
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
}
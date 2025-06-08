using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydra : MonoBehaviour
{
    public int health;
    public GameObject damage; // Visual effects for damage
    public GameObject CurrentRoom;
    public GameObject WIN; // Object to spawn on Hydra defeat
    public GameObject[] Projectile; // Array of projectiles for different elements
    public Animation[] attack; // Elemental attack animations
    public float cooldown; // Time between attacks
    public float shotForce = 20f; // Force applied to projectiles

    private float cooldownCount; // Tracks cooldown time
    private Vector3 start; // Hydra's position
    private Vector3 direction; // Direction to target
    private float sightDistance; // Sight toward player
    private GameObject target; // Main player target
    private GameObject target2; // Secondary target (e.g., Shooter)
    private Collider2D finalDetected; // Last detected collider
    private int layerMask = 1 << 3; // Layermask for raycasting
    private Vector3 shootAngle; // Angle for projectiles

    public Animator animator; // Animator for Hydra animations
    public Transform Anchor; // Rotation anchor for projectiles
    public Transform EnemyAnchor;

    private bool isSlowed = false; // Tracks if Hydra is affected by ice
    private float slowDuration = 3f; // Duration of slow effect
    private float slowEffectEndTime = 0f; // Time when slow effect ends

    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        target2 = GameObject.Find("Shooter");
        layerMask = ~layerMask; // Invert layermask for raycast
    }

    void Update()
    {
        start = this.transform.position; // Update Hydra's position
        cooldownCount += Time.deltaTime; // Increment cooldown timer
        direction = (target.transform.position - start).normalized; // Calculate direction to the target

        Debug.DrawRay(start, direction * sightDistance); // Visualize ray for debugging

        if (SightTest() == target.GetComponent<Collider2D>() || SightTest() == target2.GetComponent<Collider2D>())
        {
            // Shoot if cooldown is complete
            if (cooldownCount >= cooldown)
            {
                Shoot();
                cooldownCount = 0;
            }
        }

        // Reset slow effect if time has passed
        if (isSlowed && Time.time >= slowEffectEndTime)
        {
            ResetSpeed();
        }

        finalDetected = null; // Reset detection for next frame
    }

    /// <summary>
    /// Shoots a random elemental projectile.
    /// </summary>
    public void Shoot()
    {
        int RandomNum = Random.Range(0, 4); // Randomize attack type

        // Play corresponding attack animation
        switch (RandomNum)
        {
            case 0:
                animator.Play("HydraLightning");
                break;
            case 1:
                animator.Play("HydraFire");
                break;
            case 2:
                animator.Play("HydraIce");
                break;
            case 3:
                animator.Play("HydraEarth");
                break;
        }

        // Instantiate and fire projectile
        GameObject arrow = Instantiate(Projectile[RandomNum], start, this.transform.rotation);
        arrow.transform.rotation = Anchor.transform.rotation;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Tests if the Hydra has a clear line of sight to the target.
    /// </summary>
    public Collider2D SightTest()
    {
        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, sightDistance, layerMask);

        if (sightTest.collider != null && sightTest.collider.gameObject != gameObject)
        {
            finalDetected = sightTest.collider;
        }

        return finalDetected;
    }

    /// <summary>
    /// Handles collisions with magic projectiles.
    /// </summary>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire") || other.gameObject.CompareTag("BigFire"))
        {
            Destroy(other.gameObject);
            TakeDamage(2); // Fire damage
        }
        else if (other.gameObject.CompareTag("Earth"))
        {
            Destroy(other.gameObject);
            TakeDamage(3); // Earth damage
        }
        else if (other.gameObject.CompareTag("Lightning") || other.gameObject.CompareTag("BigLightning"))
        {
            Destroy(other.gameObject);
            TakeDamage(1); // Lightning damage
        }
        else if (other.gameObject.CompareTag("Ice"))
        {
            Destroy(other.gameObject);
            TakeDamage(1); // Ice damage
            IceSlowEffect(); // Apply ice slow effect
        }
    }

    /// <summary>
    /// Reduces Hydra's health and checks for death.
    /// </summary>
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Instantiate(WIN, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject); // Destroy Hydra on death
            CurrentRoom?.SendMessage("RoomClear"); // Notify the room manager
        }
    }

    /// <summary>
    /// Applies a slow effect when hit by ice magic.
    /// </summary>
    public void IceSlowEffect()
    {
        if (!isSlowed) // Only apply slow if not already slowed
        {
            isSlowed = true;
            cooldown *= 1.5f; // Increase cooldown time (slow attack rate)
        }

        slowEffectEndTime = Time.time + slowDuration; // Set time to end slow effect
    }

    /// <summary>
    /// Resets Hydra's speed after slow effect ends.
    /// </summary>
    public void ResetSpeed()
    {
        isSlowed = false;
        cooldown /= 1.5f; // Restore original cooldown
    }
}
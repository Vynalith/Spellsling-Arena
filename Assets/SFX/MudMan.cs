using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMan : MonoBehaviour
{
    // General settings
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public GameObject Projectile;
    public GameObject heart;

    public float cooldown; // Time between attacks
    private float cooldownCount;

    // Attack settings
    public float shotForce = 20f;
    public float sightDistance = 10;

    // Targeting and detection
    private Vector3 start;
    private Vector3 direction;
    private GameObject target; // Main player target
    private GameObject target2; // Shooter or secondary target
    private Collider2D finalDetected;
    private RaycastHit hit;

    private int layerMask = 1 << 3 | 1 << 7 | 1 << 11 | 1 << 12 | 1 << 13;

    // Animator and shooting angles
    public Animator animator;
    public Vector3 shootAngle;
    public float Horizontal;
    public float Vertical;

    // Random drop chance
    private int heartOrNo;

    // Initialization
    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        target2 = GameObject.Find("Shooter");

        // Invert layermask for Raycast filtering
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        start = this.transform.position; // Start position for the enemy
        cooldownCount += Time.deltaTime; // Increment cooldown counter
        direction = (target.transform.position - start).normalized; // Calculate direction to the target

        Debug.DrawRay(start, direction * sightDistance); // Visualize ray for debugging

        if (SightTest() == target.GetComponent<Collider2D>() || SightTest() == target2.GetComponent<Collider2D>())
        {
            ActivateMudMan();

            // Attack when cooldown is ready
            if (cooldownCount >= cooldown)
            {
                animator.Play("MudATTACK");
                Shoot();
                cooldownCount = 0;
            }
        }
        else
        {
            DeactivateMudMan();
        }

        // Reset detection after sight test
        finalDetected = null;

        // Calculate shooting angle
        shootAngle = (start - target.transform.position).normalized;
        shootAngle.y *= -1;

        // Update animator parameters for directional animation
        animator.SetFloat("Horizontal", shootAngle.x);
        animator.SetFloat("Vertical", shootAngle.y);
        Horizontal = shootAngle.x;
        Vertical = shootAngle.y;
    }

    /// <summary>
    /// Handles activating MudMan's animations and collider.
    /// </summary>
    private void ActivateMudMan()
    {
        GetComponent<Collider>().enabled = true; // Enable collider
        animator.Play("MudRise");
        animator.SetBool("Awake", true);
    }

    /// <summary>
    /// Handles deactivating MudMan's animations and collider.
    /// </summary>
    private void DeactivateMudMan()
    {
        GetComponent<Collider>().enabled = false; // Disable collider
        animator.SetBool("Awake", false);
    }

    /// <summary>
    /// Shoots a projectile at the player.
    /// </summary>
    public void Shoot()
    {
        GameObject arrow = Instantiate(Projectile, start, this.transform.rotation);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce * 15, ForceMode2D.Impulse); // Apply force to the projectile
    }

    /// <summary>
    /// Tests if the MudMan has a clear line of sight to the target.
    /// </summary>
    /// <returns>Returns the detected collider.</returns>
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
    /// Damages the MudMan and checks for death.
    /// </summary>
    /// <param name="damage">Amount of damage taken.</param>
    public void HurtMe(int damage)
    {
        health -= damage;
        CheckDeath();
    }

    /// <summary>
    /// Damages the MudMan with reduced damage for lightning.
    /// </summary>
    /// <param name="ouchie">Amount of damage taken.</param>
    public void LightningHurtMe(int ouchie)
    {
        health -= ouchie - 1; // Reduced damage for lightning
        CheckDeath();
    }

    /// <summary>
    /// Handles regular fire damage.
    /// </summary>
    public void FireHurtMe(int ouchie)
    {
        health -= ouchie;
        CheckDeath();
    }

    /// <summary>
    /// Handles regular ice damage.
    /// </summary>
    public void IceHurtMe(int ouchie)
    {
        health -= ouchie;
        CheckDeath();
    }

    /// <summary>
    /// Handles regular earth damage.
    /// </summary>
    public void EarthHurtMe(int ouchie)
    {
        health -= ouchie;
        CheckDeath();
    }

    /// <summary>
    /// Checks if MudMan's health is zero or below, and handles destruction and drops.
    /// </summary>
    private void CheckDeath()
    {
        if (health <= 0)
        {
            heartOrNo = Random.Range(0, 4);

            if (heartOrNo >= 2) // 50% chance to drop a heart
            {
                Instantiate(heart, this.transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject); // Destroy MudMan
            CurrentRoom?.SendMessage("RoomClear"); // Notify room if applicable
        }
    }
}
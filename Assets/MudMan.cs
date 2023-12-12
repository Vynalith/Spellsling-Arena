using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMan : MonoBehaviour
{
    ref public int health;
    ref public GameObject Damage;
    public GameObject CurrentRoom;
    public float cooldown;
    private float cooldownCount;
    public GameObject Projectile;
    public float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 10;
    private Collider2D finalDetected;
    private RaycastHit hit;
    private int layerMask = 1 << 3 | 1 << 7 | 1 << 11 | 1 << 12 | 1 << 13;

    public Vector3 shootAngle;

    public Animator animator;

    public int heartOrNo;
    public GameObject heart;

    public float Horizontal;
    public float Vertical;

    private float stupidspeed;

    public GameObject collider;

    // Start is called before the first frame update
    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        target2 = GameObject.Find("Shooter");
        layerMask = ~layerMask;
}
private float sightDistance = 10f;
private int damage = 10;
private float cooldownCount;
private float cooldownDuration = 2f;

public GameObject target;
public GameObject target2;
public GameObject heartPickup;

private void Start()
{
    cooldownCount = 0;
    target = GameObject.Find("Player");
    target2 = GameObject.Find("Shooter");
    layerMask = ~layerMask;
}

private void Update()
{
    cooldownCount -= Time.deltaTime;

    if (Vector3.Distance(transform.position, target.transform.position) <= sightDistance || Vector3.Distance(transform.position, target2.transform.position) <= sightDistance)
    {
        Shoot();
    }
}

private void Shoot()
{
    if (cooldownCount <= 0)
    {
        cooldownCount = cooldownDuration;

        // Shoot projectile at the nearest player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootAngle, sightDistance, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerController>().TakeDamage(damage);
            }
            else if (hit.collider.CompareTag("Shooter"))
            {
                hit.collider.GetComponent<ShooterController>().TakeDamage(damage);
            }
        }

        // Spawn heart pickup at the collision point
        Instantiate(heartPickup, hit.point, Quaternion.identity);
    }
}
    }

    // Update is called once per frame
    void Update()
    {
        // Add code to update health and damage variables if necessary

        // If the mudman is in sight range of either player
        if (Vector3.Distance(transform.position, target.transform.position) <= sightDistance || Vector3.Distance(transform.position, target2.transform.position) <= sightDistance)
        {
            // Shoot projectile at the nearest player
            Shoot();
        }

        // Add code to check for win condition

    void Shoot()
    {
        // If cooldown has passed
        if (cooldownCount <= 0)
        {
            // Play shooting animation
            animator.Play("Attack");

            // Shoot projectile towards the nearest player
            int index = Random.Range(0, Projectile.Length);
            GameObject shot = Instantiate(Projectile[index], transform.position, Quaternion.identity);
            Vector3 dir = (target.transform.position - transform.position).normalized;
            shot.GetComponent<Rigidbody2D>().AddForce(dir * shotForce);

            // Reset cooldown
            cooldownCount = cooldown;
        }
        else
        {
            // Decrement cooldown
            cooldownCount -= Time.deltaTime;
        }
    }

    // This function handles damage when the mudman is hit by a projectile
    void TakeDamage(int damage)
    {
        health -= damage;
        // Instantiate damage text and play damage sound here
}
}
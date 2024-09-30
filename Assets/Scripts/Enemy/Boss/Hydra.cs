using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydra : MonoBehaviour
{
    public int health;
    public GameObject CurrentRoom;
    public float cooldown;
    private float cooldownCount;
    public GameObject[] Projectiles;
    public float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 10f;
    private Collider2D finalDetected;
    private int layerMask = 1 << 3;

    public Animator animator;
    public GameObject anchor;
    public GameObject WIN;
    public GameObject damageEffect;

    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        target2 = GameObject.Find("Shooter");
        layerMask = ~layerMask;
        anchor = GameObject.Find("EnemyAnchor");
    }

    void Update()
    {
        start = transform.position;
        cooldownCount += Time.deltaTime;
        direction = (target.transform.position - start).normalized;
        Debug.DrawRay(start, direction * sightDistance);

        if (SightTest() == target.GetComponent<Collider2D>() || SightTest() == target2.GetComponent<Collider2D>())
        {
            if (cooldownCount >= cooldown)
            {
                Shoot();
                cooldownCount = 0;
            }
        }
        finalDetected = null;
    }

    public void Shoot()
    {
        int randomIndex = Random.Range(0, Projectiles.Length);
        string[] attackAnimations = { "HydraLightningTest", "HydraFire", "HydraIce", "HydraEarth" };

        animator.Play(attackAnimations[randomIndex]);

        GameObject projectile = Instantiate(Projectiles[randomIndex], start, transform.rotation);
        projectile.transform.rotation = anchor.transform.rotation;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce * 25);
    }

    public Collider2D SightTest()
    {
        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, sightDistance, layerMask);
        if (sightTest.collider != null && sightTest.collider.gameObject != gameObject)
        {
            finalDetected = sightTest.collider;
        }
        return finalDetected;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire") || other.gameObject.CompareTag("BigFire") ||
            other.gameObject.CompareTag("Earth") || other.gameObject.CompareTag("Lightning") ||
            other.gameObject.CompareTag("BigLightning"))
        {
            Destroy(other.gameObject);
        }
    }

    public void HurtMe(int damage)
    {
        Instantiate(damageEffect, transform.position, transform.rotation);
        health -= damage;

        if (health <= 0)
        {
            Instantiate(WIN, transform.position, Quaternion.identity);
            Destroy(gameObject);
            CurrentRoom.gameObject.SendMessage("RoomClear");
        }
    }

    public void LightningHurtMe(int ouchie) => HurtMe(ouchie);
    public void FireHurtMe(int ouchie) => HurtMe(ouchie);
    public void IceHurtMe(int ouchie) => HurtMe(ouchie);
    public void EarthHurtMe(int ouchie) => HurtMe(ouchie);
}

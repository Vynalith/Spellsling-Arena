using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int health;
    public GameObject damage;
    public GameObject CurrentRoom;
    public float cooldown;
    private float cooldownCount;
    public GameObject Projectile;
    public float shotForce = 20f;

    public Vector3 shootAngle;
    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 10f;
    private Collider2D finalDetected;
    private int layerMask = 1 << 3;

    public Animator animator;
    public GameObject heart;
    public GameObject damageEffect;

    public float Horizontal;
    public float Vertical;

    // Start is called before the first frame update
    void Start()
    {
        cooldownCount = 0;
        target = GameObject.Find("Player");
        target2 = GameObject.Find("Shooter");
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        start = transform.position;
        cooldownCount += Time.deltaTime;
        direction = (target.transform.position - start).normalized;
        Debug.DrawRay(start, direction * sightDistance, Color.red);

        if (SightTest() == target.GetComponent<Collider2D>() || SightTest() == target2.GetComponent<Collider2D>())
        {
            if (cooldownCount >= cooldown)
            {
                Shoot();
                cooldownCount = 0;
            }
        }

        finalDetected = null;
        shootAngle = (start - target.transform.position).normalized;
        shootAngle.y *= -1;

        animator.SetFloat("Horizontal", shootAngle.x);
        animator.SetFloat("Vertical", shootAngle.y);
        Horizontal = shootAngle.x;
        Vertical = shootAngle.y;
    }

    public void Shoot()
    {
        GameObject arrow = Instantiate(Projectile, start, transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shotForce);
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

    public void HurtMe(int damage)
    {
        Instantiate(damageEffect, transform.position, transform.rotation);
        health -= damage;
        if (health <= 0)
        {
            HandleDeath();
        }
    }

    public void LightningHurtMe(int ouchie) => HurtMe(ouchie);
    public void FireHurtMe(int ouchie) => HurtMe(ouchie);
    public void IceHurtMe(int ouchie) => HurtMe(ouchie);
    public void EarthHurtMe(int ouchie) => HurtMe(ouchie);

    private void HandleDeath()
    {
        int heartOrNo = Random.Range(0, 4);
        if (heartOrNo >= 2)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        CurrentRoom.SendMessage("RoomClear");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire") || other.CompareTag("Earth") || other.CompareTag("Lightning") || other.CompareTag("Ice"))
        {
            Destroy(other.gameObject);
            if (other.CompareTag("Fire"))
            {
                GameObject explo = Instantiate(damage, transform.position, Quaternion.identity);
                Destroy(explo, 1f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMan : MonoBehaviour
{
    public int health;
    public GameObject damageEffect;
    public GameObject CurrentRoom;
    public float cooldown = 60f;
    private float cooldownCount;
    public GameObject Projectile;
    public float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private GameObject target2;
    public float sightDistance = 10f;
    private Collider2D finalDetected;
    private int layerMask = 1 << 3 | 1 << 7 | 1 << 11 | 1 << 12 | 1 << 13;

    public Vector3 shootAngle;

    public Animator animator;

    public GameObject heart;

    public float Horizontal;
    public float Vertical;

    public GameObject collider; // Ensure this is assigned in the Unity Editor

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
        start = this.transform.position;
        cooldownCount++;
        direction = (target.transform.position - start).normalized;
        Debug.DrawRay(start, direction * sightDistance, Color.red);

        if (SightTest() == target.GetComponent<Collider2D>() || SightTest() == target2.GetComponent<Collider2D>())
        {
            collider.SetActive(true);
            animator.Play("MudRise");
            animator.SetBool("Awake", true);

            if (cooldownCount >= cooldown)
            {
                animator.Play("MudATTACK");
                Shoot();
                cooldownCount = 0;
            }
        }
        else
        {
            collider.SetActive(false);
            animator.SetBool("Awake", false);
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
        GameObject arrow = Instantiate(Projectile, start, this.transform.rotation);
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
        health -= damage;
        Instantiate(damageEffect, this.transform.position, this.transform.rotation);

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        int heartOrNo = Random.Range(0, 4);

        if (heartOrNo >= 2)
        {
            Instantiate(heart, this.transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
        CurrentRoom.gameObject.SendMessage("RoomClear");
    }

    public void LightningHurtMe(int ouchie) => HurtMe(ouchie - 1);
    public void FireHurtMe(int ouchie) => HurtMe(ouchie);
    public void IceHurtMe(int ouchie) => HurtMe(ouchie);
    public void EarthHurtMe(int ouchie) => HurtMe(ouchie);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMan : MonoBehaviour
{
    int health;
    GameManager CurrentRoom;
    float cooldown;
    float cooldownDuration = 2f;
    GameObject Projectile;
    float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    private float sightDistance = 10;
    private Collider2D finalDetected;
    private RaycastHit hit;
    private int layerMask = 1 << 3 | 1 << 7 | 1 << 11 | 1 << 12 | 1 << 13;

    private Vector3 shootAngle;

    private Animator animator;

    private int heartOrNo;
    private GameObject heart;
    private float horizontal;

public void Update()
    {
        cooldownDuration -= Time.deltaTime;

        // Calculate the angle between the shooter and the player
        float angle = CalculateAngle(transform.position, target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) <= sightDistance || Vector3.Distance(transform.position, target2.transform.position) <= sightDistance)
        {
            Shoot(angle);
        }
    }

private float CalculateAngle(Vector3 from, Vector3 to) => Mathf.Atan2(to.y - from.y, to.x - from.x) * Mathf.Rad2Deg;

private void Shoot(float angle)
{
    if (cooldownCount <= 0)
    {
        cooldownCount = Cooldown;

        // Create a projectile at the shooter's position
        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity);

        // Calculate the direction vector
        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

        // Shoot the projectile in the direction vector
        global::System.Object value = projectile.GetComponent<Rigidbody2D>()
            .AddForce(direction * shotForce);
    }

void OnTriggerEnter2D(Collider2D collision)
{
    if (!collision.gameObject.CompareTag("PlayerSpell"))
    {
        return;
    }
    TakeDamage(10);

    // Destroy the projectile
    global::System.Object value = Destroy(collision.GameObject);
}

void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
        // Trigger the game over condition
        global::System.Object value1 = CurrentRoom.GameOver();

        // Change the current room
        global::System.Object value = CurrentRoom.ChangeRoom();
        }
}
}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMan : MonoBehaviour
{   
    health = 100;
    cooldown = 0f;
    cooldownDuration = 2f;
    PlayerProjectile = GameObject.Find("PlayerProjectile");
    shotForce = 20f;
    start = transform.position;
    direction = Vector3.zero;
    target = GameObject.Find("Player");
    sightDistance = 10f;
    finalDetected = null;
    hit = new RaycastHit();
    layerMask = ~layerMask; // Invert the layer mask to ignore specified layers
    shootAngle = Vector3.zero;
    animator = GetComponent<Animator>();
    heartOrNo = 0;
    cooldownCount = 0;
    heart = GameObject.Find("HeartPickup");
    horizontal = 0f;
    vertical = 0f;
    stupidspeed = 5f; // Example speed value
}
public System.Int32 GetHealth1()
    {
        return health;
    }
public void SetHealth1(System.Int32 value)
    {
        health = value;
    }

public Animator GetAnimator();
{
    return animator;
}

public void SetAnimator(Animator value)
{
    animator = value;
}

public System.Int32 GetHeartOrNo()
{
    return heartOrNo;
}

public void SetHeartOrNo(System.Int32 value)
{
    heartOrNo = value;
}

public Vector3 GetShootAngle()
{
    return shootAngle;
}

public void SetShootAngle(Vector3 value)
{
    shootAngle = value;
}

public RaycastHit GetHit()
{
    return hit;
}

public void SetHit(RaycastHit value)
{
    hit = value;
}

public Collider2D GetFinalDetected()
{
    return finalDetected;
}

public void SetFinalDetected(Collider2D value)
{
    finalDetected = value;
}

public Vector3 GetDirection()
{
    return direction;
}

public void SetDirection(Vector3 value)
{
    direction = value;
}

public Vector3 GetStart1()
{
    return start;
}

public void SetStart1(Vector3 value)
{
    start = value;
}

public System.Single GetCooldownDuration()
{
    return cooldownDuration;
}

public void SetCooldownDuration(System.Single value)
{
    cooldownDuration = value;
}

public System.Single GetCooldown()
{
    return cooldown;
}

public void SetCooldown(System.Single value)
{
    cooldown = value;
}

public System.Single GetHorizontal()
{
    return horizontal;
}

public void SetHorizontal(System.Single value)
{
    horizontal = value;
}

public System.Single GetVertical()
{
    return vertical;
}

public void SetVertical(System.Single value)
{
    vertical = value;
}

public System.Single GetStupidspeed()
{
    return stupidspeed;
}

public void SetStupidspeed(System.Single value)
{
    stupidspeed = value;
}

public System.Int32 GetDamage()
{
    return damage;
}

public void SetDamage(System.Int32 value)
{
    damage = value;
}

public GameObject GetHeartPickup()
{
    return heartPickup;
}

public void SetHeartPickup(GameObject value)
{
    heartPickup = value;
}

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
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (!collision.gameObject.CompareTag("PlayerProjectile"))
    {
        return;
    }
    TakeDamage(10);

    // Destroy the projectile
    global::System.Object value = Destroy(collision.gameObject);
}

public void TakeDamage(int damage)
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
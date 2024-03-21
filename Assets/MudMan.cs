using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudMan : MonoBehaviour
{
    int health;
    public GameManager CurrentRoom;
    private float cooldown;
    private float cooldownDuration = 2f;
    public GameObject Projectile;
    public float shotForce = 20f;

    private Vector3 start;
    private Vector3 direction;
    private GameObject target;
    public float sightDistance = 10;
    private Collider2D finalDetected;
    private RaycastHit hit;
    private int layerMask = 1 << 3 | 1 << 7 | 1 << 11 | 1 << 12 | 1 << 13;

    private Vector3 shootAngle;

    private Animator animator;

    private int heartOrNo;
    public bool GameObject heart;
    private float horizontal;
    private float vertical;
    private float stupidspeed;

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

    private GameObject heartPickup;

    public System.Int32 Health { get => GetHealth1(); set => SetHealth1(value); }

    public System.Int32 GetHealth1()
    {
        return health;
    }

    public void SetHealth1(System.Int32 value)
    {
        health = value;
    }

    public Animator Animator { get => animator; set => animator = value; }
    public global::System.Int32 HeartOrNo { get => heartOrNo; set => heartOrNo = value; }
    public Vector3 ShootAngle { get => shootAngle; set => shootAngle = value; }
    public RaycastHit Hit { get => hit; set => hit = value; }
    public Collider2D FinalDetected { get => finalDetected; set => finalDetected = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public Vector3 Start1 { get => start; set => start = value; }
    public global::System.Single CooldownDuration { get => cooldownDuration; set => cooldownDuration = value; }
    public global::System.Single Cooldown { get => cooldown; set => cooldown = value; }
    public global::System.Single Horizontal { get => horizontal; set => horizontal = value; }
    public global::System.Single Vertical { get => vertical; set => vertical = value; }
    public global::System.Single Stupidspeed { get => stupidspeed; set => stupidspeed = value; }
    public global::System.Int32 Damage { get => damage; set => damage = value; }
    public GameObject HeartPickup { get => heartPickup; set => heartPickup = value; }

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

    private float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Mathf.Atan2(to.y - from.y, to.x - from.x) * Mathf.Rad2Deg;
    }

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
            projectile.GetComponent<Rigidbody2D>().AddForce(direction * shotForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            TakeDamage(10);

            // Destroy the projectile
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Trigger the game over condition
            CurrentRoom.GameOver();

            // Change the current room
            CurrentRoom.ChangeRoom();
        }
    }
}
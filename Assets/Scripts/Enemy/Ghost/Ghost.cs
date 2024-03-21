using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public GameObject health;
    public GameObject damage;
    public GameObject currentRoom;
    public Animator animator;
    public GameObject heart;

    private readonly Transform aim1;

    public GameObject aimTarget { get; private set; }
    public GameObject CurrentRoom { get => currentRoom; set => currentRoom = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public GameObject Heart { get => heart; set => heart = value; }
    public float GetPlayerAwarenessDistance() => ghost.playerAwarenessDistance;
    public Vector2 DirectionToPlayer1 { get; set; }
    public GameObject Health1 { get; set; }
    public float Rotationspeed { get; set; }

    public GameObject GetPlayerTarget() => ghost.playertarget;

    public void SetPlayertarget(GameObject target) => aimTarget = target;

    void Start()
    {
        // Set the initial values
        health.GetComponent<Health>().Health = 100;
        damage.GetComponent<Damage>().Damage = 10;
        heart.GetComponent<Heart>().Health = 100;
        SetPlayertarget(GameObject.Find("Aim"));
    }

    void Update()
    {
        if (currentRoom != GetPlayerTarget())
        {
            // Check if the ghost is aware of the player
            if (Vector3.Distance(transform.position, GetPlayerTarget().transform.position) <= ghost.playerAwarenessDistance)
            {
                // Calculate the direction to the player
                DirectionToPlayer1 = (GetPlayerTarget().transform.position - transform.position).normalized;

                // Rotate the ghost towards the player
                float rotationSpeed = ghost.rotationSpeed * Time.deltaTime;
                Quaternion targetRotation = Quaternion.LookRotation(DirectionToPlayer1);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
            }
        }
    }

    public void DamageGhost(int damage)
    {
        // Reduce the ghost's health
        health.GetComponent<Health>().TakeDamage(damage);

        // Check if the ghost has been defeated
        if (health.GetComponent<Health>().Health <= 0)
        {
            // Defeat the ghost
            Debug.Log("Ghost defeated");
        }
    }
}
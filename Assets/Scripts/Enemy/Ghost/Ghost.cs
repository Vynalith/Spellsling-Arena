using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    GameObject health;
    GameObject damage;
    GameObject currentRoom;
    Animator animator;
    GameObject heart;
    GameObject aimTarget1;
    void SetaimTarget(GameObject value) => aimTarget1 = value;
    public GameObject Heart { get => Heart; set => Heart = value; }

    public void SetPlayertarget(GameObject target) => SetaimTarget(target);

    void Start()
    {
        // Set the initial values
        health.GetComponent<Health>().Health = 100;
        damage.GetComponent<Damage>().Damage = 10;
        heart.GetComponent<Heart>().Heart = 100;
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
                DirectionToPlayer1 = (GetPlayerTarget().transform.position
                    - transform.position).normalized;

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
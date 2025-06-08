using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEarth : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 100f; // Adjustable in Inspector
    [SerializeField] private GameObject pummelPrefab; // Assign prefab in Inspector
    [SerializeField] private GameObject iceShatterPrefab; // Assign prefab in Inspector
    [SerializeField] private GameObject firePrefab; // Assign prefab in Inspector
    [SerializeField] private GameObject zappyPrefab; // Assign prefab in Inspector
    [SerializeField] private float selfDestructTime = 0.4f; // How long before the object destroys itself

    private Rigidbody2D r2d;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();

        // Apply a force to the object
        Vector2 shootDirection = new Vector2(1, 0.1f).normalized; // Direction with slight upward angle
        r2d.AddForce(shootDirection * shootSpeed);

        // Destroy the object after a set amount of time
        Destroy(this.gameObject, selfDestructTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with Player
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.HurtMe(2); // Call the HurtMe method directly
            }
        }

        // Check for collision with Fire
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject); // Destroy the fire object
            if (firePrefab != null)
            {
                InstantiateEffect(firePrefab); // Create fire explosion
            }
        }

        // Check for collision with Lightning
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(other.gameObject); // Destroy the lightning object
            if (zappyPrefab != null)
            {
                InstantiateEffect(zappyPrefab); // Create lightning effect
            }
        }

        // Check for collision with Ice
        if (other.gameObject.CompareTag("Ice"))
        {
            Destroy(other.gameObject); // Destroy the ice object
            if (iceShatterPrefab != null)
            {
                InstantiateEffect(iceShatterPrefab); // Create ice shatter effect
            }
        }

        // Check for collision with Earth or other large objects
        if (other.gameObject.CompareTag("Earth") || other.gameObject.CompareTag("BigFire") || other.gameObject.CompareTag("BigLightning"))
        {
            Debug.Log("Boom!"); // Log the collision
            if (pummelPrefab != null)
            {
                InstantiateEffect(pummelPrefab); // Create explosion effect
            }
            Destroy(this.gameObject); // Destroy this object
        }
    }

    // Helper method to instantiate effects and clean up afterward
    private void InstantiateEffect(GameObject effectPrefab)
    {
        GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 1f); // Destroy the effect after 1 second (adjust as needed)
    }
}
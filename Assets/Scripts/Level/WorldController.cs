using System.Collections;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [Header("References")]
    public GameObject destroy; // Object to destroy
    public GameObject player; // Reference to the player

    [Header("Input Settings")]
    public string destroyInput = "Fire1"; // Input to destroy the object
    public string damageInput = "Ouch"; // Input to simulate damage to the player
    public string winInput = "Win"; // Input to trigger a win state

    void Update()
    {
        HandleInputs();
    }

    /// <summary>
    /// Processes user inputs and triggers corresponding actions.
    /// </summary>
    private void HandleInputs()
    {
        // Destroy object when the specified input is pressed
        if (Input.GetButtonDown(destroyInput) && destroy != null)
        {
            Destroy(destroy);
            Debug.Log($"Destroyed object: {destroy.name}");
        }

        // Simulate player taking damage
        if (Input.GetButtonDown(damageInput) && player != null)
        {
            player.SendMessage("EnemyCollide", SendMessageOptions.DontRequireReceiver);
            Debug.Log("Triggered EnemyCollide on the player.");
        }

        // Trigger a win state
        if (Input.GetButtonDown(winInput) && player != null)
        {
            player.SendMessage("Win", SendMessageOptions.DontRequireReceiver);
            Debug.Log("Triggered Win on the player.");
        }
    }
}
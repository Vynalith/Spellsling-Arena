using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private GameObject entrance; // Assign in the Inspector
    [SerializeField] private GameObject exit; // Assign in the Inspector
    [SerializeField] private GameObject[] enemies; // Assign in the Inspector or populate dynamically
    [SerializeField] private GameObject player; // Assign in the Inspector

    private bool roomLocked = false; // Tracks whether the room is locked
    private int remainingEnemies;

    void Start()
    {
        if (entrance != null) entrance.SetActive(false);
        if (exit != null) exit.SetActive(false);

        // If enemies are not assigned manually, find them dynamically
        if (enemies == null || enemies.Length == 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        // Set the initial count of enemies
        remainingEnemies = enemies.Length;

        // Deactivate all enemies initially
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        if (player == null)
        {
            player = GameObject.Find("Player"); // Find player dynamically if not assigned
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!roomLocked && other.gameObject == player)
        {
            RoomLock();
        }
    }

    private void RoomLock()
    {
        roomLocked = true;

        if (entrance != null) entrance.SetActive(true);
        if (exit != null) exit.SetActive(true);

        // Activate all enemies
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }

        // Notify the player to start the boss music (if implemented in the player's script)
        if (player != null)
        {
            player.SendMessage("StartBossMusic", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void RoomClear()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            if (entrance != null) entrance.SetActive(false);
            if (exit != null) exit.SetActive(false);

            Debug.Log("Boss room cleared!");
        }
    }
}

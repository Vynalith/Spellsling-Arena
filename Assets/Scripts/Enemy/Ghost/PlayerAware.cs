using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAware : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float playerAwarenessDistance = 10f; // Adjust the awareness distance as needed

    private Transform player;
    private GameObject playerTarget;

    private void Awake()
    {
        playerTarget = GameObject.Find("Aim");
        if (playerTarget != null)
        {
            player = playerTarget.transform;
        }
        else
        {
            Debug.LogError("Player target not found. Ensure there is a GameObject named 'Aim' in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        AwareOfPlayer = enemyToPlayerVector.magnitude <= playerAwarenessDistance;
    }
}

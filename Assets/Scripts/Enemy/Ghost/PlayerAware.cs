using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAware : MonoBehaviour
{
    public bool AwareOfPlayer {get; private set;}
    public Vector2 DirectionToPlayer {get; private set;}

    [SerializeField]
    private float playerAwarenessDistance;

    public Transform player;
    // Start is called before the first frame update
    private void Awake()
    {
        //player = FindObjectOfType<GameObject>().Transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}

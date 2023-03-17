using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAware : MonoBehaviour
{

    /*
    public bool AwareOfPlayer {get; private set;}
    public Vector2 DirectionToPlayer {get; private set;}

    [SerializeField]
    private float playerAwarenessDistance;

    private Transform player;
    private GameObject playertarget;
    private Vector3 stupid;
    // Start is called before the first frame update

        //doesn't run start for some reason
        /*
    void start()
    {
        playertarget = GameObject.Find("Aim");
        print(playertarget);
        player = playertarget.transform;
        print(player);
        print(player.transform.position);
    }
    /

    private void Awake()
    {
        playertarget = GameObject.Find("Aim");
        //print(playertarget);
        player = playertarget.transform;
        
        //print(player);
        //print(player.transform.position);
        //player = FindObjectOfType<GameObject>().Transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector;
        //print(DirectionToPlayer);

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
        
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public bool isElectrified;
    public GameObject particle;
    private bool zappersSpawned;
    public GameObject newIce;
    private GameObject zappers; // Added missing variable for zappers

    private int playerPuddleCount;
    private int enemyPuddleCount;
    private GameObject zappyBoi;
    public GameObject player;

    void Start()
    {
        playerPuddleCount = 0;
        enemyPuddleCount = 0;
    }

    void Update()
    {
        HandleElectrification();
        HandleDamage();
    }

    private void HandleElectrification()
    {
        if (isElectrified && !zappersSpawned)
        {
            zappersSpawned = true;
            InstantiateParticle();
        }

        if (!isElectrified && zappersSpawned)
        {
            zappersSpawned = false;
            Destroy(zappers);
        }
    }

    private void InstantiateParticle()
    {
        zappers = Instantiate(particle, transform.position, transform.rotation);
    }

    private void HandleDamage()
    {
        if (isElectrified)
        {
            if (playerPuddleCount != 0 && playerPuddleCount % 1000 == 0)
            {
                player.SendMessage("PuddleHurtMe", 1);
            }

            if (zappyBoi != null && enemyPuddleCount != 0 && enemyPuddleCount % 1000 == 0)
            {
                zappyBoi.SendMessage("HurtMe", SendMessageOptions.DontRequireReceiver);
            }

            if (playerPuddleCount != 0)
            {
                playerPuddleCount++;
            }

            if (enemyPuddleCount != 0)
            {
                enemyPuddleCount++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lightning") || other.CompareTag("BigLightning") || other.CompareTag("EnemyLightning"))
        {
            Electrify();
        }
        else if (other.CompareTag("EarthWall"))
        {
            HandleEarthWall();
        }
        else if (other.CompareTag("IceWall"))
        {
            HandleIceWall();
        }
        else if (other.CompareTag("Enemy"))
        {
            HandleEnemyEnter(other);
        }
        else if (other.CompareTag("Player"))
        {
            HandlePlayerEnter(other);
        }
    }

    private void Electrify()
    {
        if (!isElectrified)
        {
            isElectrified = true;
        }
    }

    private void HandleEarthWall()
    {
        if (zappersSpawned)
        {
            zappersSpawned = false;
            Destroy(zappers);
        }
        Destroy(gameObject);
    }

    private void HandleIceWall()
    {
        if (zappersSpawned)
        {
            zappersSpawned = false;
            Destroy(zappers);
        }
        Instantiate(newIce, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void HandleEnemyEnter(Collider2D other)
    {
        if (isElectrified)
        {
            enemyPuddleCount = 1;
            zappyBoi = other.gameObject;
            other.SendMessage("LightningHurtMe", 1);
        }
    }

    private void HandlePlayerEnter(Collider2D other)
    {
        if (isElectrified)
        {
            playerPuddleCount = 1;
            other.SendMessage("HurtMe", 1);
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPuddleCount = 0;
        }
        else if (other.CompareTag("Enemy"))
        {
            enemyPuddleCount = 0;
            zappyBoi = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{
    public int enemies;
    public GameObject Entrance;
    public GameObject Exit;
    public GameObject[] Enemies;
    private GameObject player;
    public int enter;
    public float delayBeforeReset = 3f; // Adjustable delay before resetting the level

    void Start()
    {
        Entrance.SetActive(false);
        Exit.SetActive(false);
        enter = 0;
        player = GameObject.Find("Player");
        enemies = Enemies.Length; // Initialize enemies count
    }

    public void RoomLock()
    {
        if (enter < 1)
        {
            Entrance.SetActive(true);
            Exit.SetActive(true);
            enter++;

            foreach (GameObject enemy in Enemies)
            {
                enemy.SetActive(true);
            }

            player.SendMessage("StartBossMusic", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void RoomClear()
    {
        enemies--;

        if (enemies <= 0)
        {
            Entrance.SetActive(false);
            Exit.SetActive(false);
            StartCoroutine(ReturnToMenu());
        }
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(delayBeforeReset); // Use adjustable delay
        SceneManager.LoadScene("Menu"); // Load the menu scene (replace "MenuScene" with your menu scene name)
    }
}

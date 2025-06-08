using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public GameObject destroy;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("Fire1"))
        {
            Destroy(destroy);
        }

        if( Input.GetButtonDown("Ouch"))
        {
            player.gameObject.SendMessage("EnemyCollide");
        }

         if( Input.GetButtonDown("Win"))
        {
            player.gameObject.SendMessage("Win");
        }

    }
}

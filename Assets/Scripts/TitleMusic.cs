using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusic : MonoBehaviour
{

    AudioSource song1Intro;
    AudioSource song1Loop;
    float timer;
    float songCount = 9.4f;
    int song1Change = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= songCount && song1Change == 0)
        {

            song1Change = 1;
        }
        else if (song1Change == 0 && timer < songCount)
        {
            timer += Time.deltaTime;
        }

        if (song1Change == 1)
        {

            song1Intro.Stop();
            song1Loop.Play();
            song1Change = 2;
        }
    }
}

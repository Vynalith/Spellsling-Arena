using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusic : MonoBehaviour
{

    public AudioSource song1Intro;
    public AudioSource song1Loop;
    private float timer;
    private float songCount = 9.4f;
    private int song1Change = 0;
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

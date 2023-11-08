using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehavior
{
    public float timer;
    public float textTimer;
    public float skipTimer;
    private int movespeed = Vector3.one;
    public Vector3 userDirection = Vector3.one;
    public bool move;

    public bool allowing;
    public GameObject inputTUT;

    public Animator animator;
    public int tutorialNum;
    private GameObject tutscreen1;
    public GameObject tutscreen2;
    public GameObject tutscreen3;
    public GameObject tutscreen4;

    public int randomQuack;
    public bool firstQuack;
    public bool nextQuack;
    public bool isScreaming;
    public AudioSource[] quacks;
    public AudioSource quackDie;
    public AudioSource quackSong;

    public GameObject sceneLoader;
    public GameObject zap;
    public GameObject duckHunt;

    public global::System.Int32 movespeed { get => movespeed; set => movespeed = value; }
    public global::System.Int32 movespeed { get => movespeed; set => movespeed = value; }
    public GameObject Tutscreen1 { get => tutscreen1; set => tutscreen1 = value; }

    //public Transform duckHuntDog;

    // Start is called before the first frame update
    void Start()
    {
        move=true;
        allowing=false;
        tutorialNum=0;
        nextQuack = true;
        isScreaming = true; //just internally for now
    }

    // Update is called once per frame
    void Update()
    { 
        timer += Time.deltaTime;

        if(this.transform.position.x >= 0)
        {
            move=false;
            //print("yay");
            userDirection.x=0;
            //animator.Play("DuckTalk");
            
        }
        if(move == true)
        {
          transform.Translate(userDirection * Movespeed * Time.deltaTime);
        }
        if (timer >= 3.65f && timer < 5f && !firstQuack)
        {
            firstQuack = true;
            nextQuack = true;
            if (nextQuack)
            {
                randomQuack = Random.Range(0, 3);
                quacks[randomQuack].Play();
                nextQuack = false;
            }
            Tutscreen1.SetActive(true);
            inputTUT.SetActive(true);
            animator.Play("DuckTalk");
            allowing = true;

        }

<<<<<<< HEAD
        if(allowing == true)
        {
            if( Input.GetButtonDown("Fire2"))
            {
                tutorialNum += 1;
=======
        if(allowing)
        {
            if( Input.GetButtonDown("Fire2"))
            {
                tutorialNum+=1;
                nextQuack = true;
                if(tutorialNum > 3)
                {
                    Instantiate(zap, duckHunt.transform.position, duckHunt.transform.rotation);
                    tutorialNum = 20;
                    isScreaming = false;
                    skipTimer = timer + 1.25f;
                    quackSong.Stop();

                }
>>>>>>> 5eda4be562a86da600640d4d7213a58e5760fdb5
            }

            if( Input.GetButtonDown("M2"))
            {
                if(timer > 4f && isScreaming)
                {
                    Instantiate(zap, duckHunt.transform.position, duckHunt.transform.rotation);
                    skipTimer = timer + 1.25f;
                    isScreaming = false;
                   tutorialNum = 20;
                    quackSong.Stop();
                }
                
             }

        }

        
        /*if(tutorialNum == 1)
        {
            tutscreen1.SetActive(true);
        }*/
        if(tutorialNum == 1)
        {
            if (nextQuack)
            {
                randomQuack = Random.Range(0, 3);
                quacks[randomQuack].Play();
                nextQuack = false;
            }
            Tutscreen1.SetActive(false);
            tutscreen2.SetActive(true);
        }
           if(tutorialNum == 2)
        {
            if (nextQuack)
            {
                randomQuack = Random.Range(0, 3);
                quacks[randomQuack].Play();
                nextQuack = false;
            }
            tutscreen2.SetActive(false);
            tutscreen3.SetActive(true);
        }

          if(tutorialNum == 3)
        {
            if (nextQuack)
            {
                randomQuack = Random.Range(0, 3);
                quacks[randomQuack].Play();
                nextQuack = false;
            }
            tutscreen3.SetActive(false);
            tutscreen4.SetActive(true);
        }

        if(tutorialNum == 20)
        {
            if (!isScreaming)
            {
                quackDie.Play();
                isScreaming = true;
            }
            animator.Play("DuckOw");
            Tutscreen1.SetActive(false);
            tutscreen2.SetActive(false);
            tutscreen3.SetActive(false);
            tutscreen4.SetActive(false);
            if(timer > skipTimer)
            {
                sceneLoader.SendMessage("LoadScene", "Game");
            }
        }

    }


    public void MoveAim()
    {
        //duckHunt.transform.position = duckHunt.transform.position.down();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tutorial : SceneManagement
{
    public float time;
    public int movespeed = 3;
    public Vector3 userDirection = Vector3.one;
    public bool move;

    public bool allowinput;
    public GameObject inputTUT;

    public Animator animator;
    public int tutorialNum;
    public GameObject tutscreen1;
    public GameObject tutscreen2;
    public GameObject tutscreen3;
    public GameObject tutscreen4; 
    //public Transform duckHuntDog;

    // Start is called before the first frame update
    void Start()
    {
        move=true;
        allowinput=false;
        tutorialNum=0;
    }

    // Update is called once per frame
    void Update()
    { 
        time+=Time.deltaTime;

        if(this.transform.position.x >= 0)
        {
            move=false;
            //print("yay");
            userDirection.x=0;
            //animator.Play("DuckTalk");
            
        }
        if(move=true)
        {
          transform.Translate(userDirection * movespeed * Time.deltaTime);
        }
        if(time >= 4f && time < 5f)
        {
            tutscreen1.SetActive(true);
            inputTUT.SetActive(true);
            animator.Play("DuckTalk");
        }

        if(allowinput=true)
        {
            if( Input.GetButtonDown("Fire2"))
            {
                tutorialNum+=1;
            }

             if( Input.GetButtonDown("M2"))
             {
                if(time> 4f)
                {
                   tutorialNum = 20; 
                }
                
             }

        }

        
        /*if(tutorialNum == 1)
        {
            tutscreen1.SetActive(true);
        }*/
        if(tutorialNum == 1)
        {
            tutscreen1.SetActive(false);
            tutscreen2.SetActive(true);
        }
           if(tutorialNum == 2)
        {
            tutscreen2.SetActive(false);
            tutscreen3.SetActive(true);
        }

          if(tutorialNum == 3)
        {
            tutscreen3.SetActive(false);
            tutscreen4.SetActive(true);
        }

        if(tutorialNum ==20)
        {
            animator.Play("DuckOw");
            tutscreen1.SetActive(false);
            tutscreen2.SetActive(false);
            tutscreen3.SetActive(false);
            tutscreen4.SetActive(false);
        }

    }
}
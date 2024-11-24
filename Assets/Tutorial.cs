using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public Vector3 userDirection = Vector3.one;
    private bool move = true;

    [Header("Tutorial Flow")]
    public bool allowInput = false;
    public GameObject inputTUT;
    public Animator animator;
    public int tutorialNum = 0;
    public GameObject[] tutorialScreens; // Array to hold tutorial screens
    public float time = 0f;

    void Start()
    {
        // Initialize settings
        move = true;
        allowInput = false;
        tutorialNum = 0;

        // Deactivate all tutorial screens
        foreach (GameObject screen in tutorialScreens)
        {
            screen.SetActive(false);
        }

        // Deactivate input tutorial UI
        if (inputTUT != null)
        {
            inputTUT.SetActive(false);
        }
    }

    void Update()
    {
        // Increment time
        time += Time.deltaTime;

        // Handle movement
        if (move)
        {
            transform.Translate(userDirection * moveSpeed * Time.deltaTime);

            if (transform.position.x >= 0)
            {
                move = false;
                userDirection = Vector3.zero; // Stop movement
            }
        }

        // Trigger tutorial screen and input tutorial after 4 seconds
        if (time >= 4f && time < 5f)
        {
            ShowTutorialScreen(0);
            if (inputTUT != null)
            {
                inputTUT.SetActive(true);
            }
            if (animator != null)
            {
                animator.Play("DuckTalk");
            }
            allowInput = true; // Allow input for progression
        }

        // Handle input progression
        if (allowInput)
        {
            // Advance tutorial on "Fire2" input
            if (Input.GetButtonDown("Fire2"))
            {
                AdvanceTutorial();
            }

            // Skip tutorial on "M2" input
            if (Input.GetButtonDown("M2") && time > 4f)
            {
                SkipTutorial();
            }
        }
    }

    private void ShowTutorialScreen(int screenIndex)
    {
        // Deactivate all screens first
        foreach (GameObject screen in tutorialScreens)
        {
            screen.SetActive(false);
        }

        // Activate the desired screen
        if (screenIndex >= 0 && screenIndex < tutorialScreens.Length)
        {
            tutorialScreens[screenIndex].SetActive(true);
        }
    }

    private void AdvanceTutorial()
    {
        tutorialNum++;

        if (tutorialNum < tutorialScreens.Length)
        {
            ShowTutorialScreen(tutorialNum);
        }
        else
        {
            EndTutorial();
        }
    }

    private void SkipTutorial()
    {
        tutorialNum = 20; // Special skip case
        if (animator != null)
        {
            animator.Play("DuckOw");
        }

        // Deactivate all screens
        foreach (GameObject screen in tutorialScreens)
        {
            screen.SetActive(false);
        }
    }

    private void EndTutorial()
    {
        // Deactivate all tutorial elements
        foreach (GameObject screen in tutorialScreens)
        {
            screen.SetActive(false);
        }

        if (inputTUT != null)
        {
            inputTUT.SetActive(false);
        }

        Debug.Log("Tutorial Ended");
    }
}
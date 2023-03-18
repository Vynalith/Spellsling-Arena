using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public int health;

    public GameObject health1of5;
    public GameObject health2of5;
    public GameObject health3of5;
    public GameObject health4of5;
    public GameObject health5of5;

    public Image Lightning;
    public Image Fire;
    public Image Ice;
    public Image Earth;
    public Image lightningSelect;
    public Image fireSelect;
    public Image iceSelect;
    public Image earthSelect;

    // Start is called before the first frame update
    void Start()
    {
        lightningSelect.gameObject.SetActive(true);
        fireSelect.gameObject.SetActive(false);
        iceSelect.gameObject.SetActive(false);
        earthSelect.gameObject.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 5)
        {
            health1of5.gameObject.SetActive(true);
            health2of5.gameObject.SetActive(true);
            health3of5.gameObject.SetActive(true);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 4)
        {
            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(true);
            health3of5.gameObject.SetActive(true);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 3)
        {
            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(true);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 2)
        {
            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(false);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 1)
        {
            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(false);
            health4of5.gameObject.SetActive(false);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 0)
        {
            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(false);
            health4of5.gameObject.SetActive(false);
            health5of5.gameObject.SetActive(false);
        }
    }

    public void ActiveElement(int index)
    {
        if(index == 1)
        {
            lightningSelect.gameObject.SetActive(true);
            fireSelect.gameObject.SetActive(false);
            iceSelect.gameObject.SetActive(false);
            earthSelect.gameObject.SetActive(false);
        }
        else if(index == 2)
        {
            fireSelect.gameObject.SetActive(true);
            lightningSelect.gameObject.SetActive(false);
            iceSelect.gameObject.SetActive(false);
            earthSelect.gameObject.SetActive(false);
        }
        else if(index == 3)
        {
            iceSelect.gameObject.SetActive(true);
            lightningSelect.gameObject.SetActive(false);
            fireSelect.gameObject.SetActive(false);
            earthSelect.gameObject.SetActive(false);
        }
        else if(index == 4)
        {
            earthSelect.gameObject.SetActive(true);
            lightningSelect.gameObject.SetActive(false);
            fireSelect.gameObject.SetActive(false);
            iceSelect.gameObject.SetActive(false);
        }
    }

    void Hurt()
    {
        health = health-1;
    }
    void Heal()
    {
        health = health + 1;
    }
    
}
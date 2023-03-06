using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

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
}

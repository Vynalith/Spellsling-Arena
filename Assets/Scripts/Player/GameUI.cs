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

    private float iceCooldown;
    private float fireCooldown;
    private float earthCooldown;
    private float lightningCooldown;

    private float iceTime;
    private float fireTime;
    private float earthTime;
    private float lightningTime;

    private bool iceSent;
    private bool fireSent;
    private bool earthSent;
    private bool lightningSent;

    private GameObject Shooter;
    private bool stupidStart = true;

    // Start is called before the first frame update
    void Start()
    {
        lightningSelect.gameObject.SetActive(true);
        fireSelect.gameObject.SetActive(false);
        iceSelect.gameObject.SetActive(false);
        earthSelect.gameObject.SetActive(false);
        Shooter = GameObject.Find("Shooter");
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 5)
        {
            //print("Health 5, = " + health);
            health1of5.gameObject.SetActive(true);
            health2of5.gameObject.SetActive(true);
            health3of5.gameObject.SetActive(true);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 4)
        {
            //print("Health 4, = " + health);
            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(true);
            health3of5.gameObject.SetActive(true);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 3)
        {
            //print("Health 3, = " + health);
            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(true);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 2)
        {
            //print("Health 2, = " + health);

            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(false);
            health4of5.gameObject.SetActive(true);
            health5of5.gameObject.SetActive(true);
        }
        if(health == 1)
        {
           // print("Health 1, = " + health);

            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(false);
            health4of5.gameObject.SetActive(false);
            health5of5.gameObject.SetActive(true);
        }
        if(health <= 0)
        {
            //print("Health 0, = " + health);

            health1of5.gameObject.SetActive(false);
            health2of5.gameObject.SetActive(false);
            health3of5.gameObject.SetActive(false);
            health4of5.gameObject.SetActive(false);
            health5of5.gameObject.SetActive(false);
        }

        if(iceTime < iceCooldown)
        {
            iceSent = false;
            //print(iceTime / iceCooldown);
            Ice.fillAmount = (iceTime / iceCooldown);
            iceTime += Time.deltaTime;
        }
        else if(iceTime > iceCooldown && iceSent == false)
        {
            Ice.fillAmount = 1;
            iceSent = true;
            Shooter.SendMessage("IceEnable", SendMessageOptions.DontRequireReceiver);
        }
        if(fireTime < fireCooldown)
        {
            fireSent = false;
            //print(fireTime / fireCooldown);
            Fire.fillAmount = (fireTime / fireCooldown);
            fireTime += Time.deltaTime;
        }
        else if(fireTime > fireCooldown && fireSent == false)
        {
            Fire.fillAmount = 1;
            fireSent = true;
            Shooter.SendMessage("FireEnable", SendMessageOptions.DontRequireReceiver);
        }
        if(earthTime < earthCooldown +.01f)
        {
            earthSent = false;
            //print(earthTime / earthCooldown);
            Earth.fillAmount = (earthTime / earthCooldown);
            earthTime += Time.deltaTime;
        }
        else if(earthTime > earthCooldown +.01f && earthSent == false)
        {
            Earth.fillAmount = 1;
            earthSent = true;
            Shooter.SendMessage("EarthEnable", SendMessageOptions.DontRequireReceiver);
        }
        if (lightningTime < lightningCooldown)
        {
            lightningSent = false;
            //print(iceTime / iceCooldown);
            Lightning.fillAmount = (lightningTime / lightningCooldown);
            lightningTime += Time.deltaTime;
        }
        else if (lightningTime > lightningCooldown && lightningSent == false)
        {
            Lightning.fillAmount = 1;
            lightningSent = true;
            Shooter.SendMessage("LightningEnable", SendMessageOptions.DontRequireReceiver);
        }



        if (stupidStart)
        {
            Shooter.SendMessage("LightningReady", SendMessageOptions.DontRequireReceiver);
            Shooter.SendMessage("EarthReady", SendMessageOptions.DontRequireReceiver);
            Shooter.SendMessage("FireReady", SendMessageOptions.DontRequireReceiver);
            Shooter.SendMessage("IceReady", SendMessageOptions.DontRequireReceiver);
            stupidStart = false;
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

    void Hurt(int damage)
    {
        print("taking damage");
        health -= damage;
    }
    void Heal()
    {
        health = health + 1;
    }
    
    void IceCooldown(float cooldown)
    {
        iceCooldown = Time.deltaTime + cooldown;
        iceTime = Time.deltaTime;
    }
    
    void FireCooldown(float cooldown)
    {
        fireCooldown = Time.deltaTime + cooldown;
        fireTime = Time.deltaTime;
    }
    
    void EarthCooldown(float cooldown)
    {
        earthCooldown = Time.deltaTime + cooldown;
        earthTime = Time.deltaTime;
    }
    
    void LightningCooldown(float cooldown)
    {
        lightningCooldown = Time.deltaTime + cooldown;
        lightningTime = Time.deltaTime;
    }


}
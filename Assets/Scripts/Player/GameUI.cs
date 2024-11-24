using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Health Display")]
    public GameObject[] healthIcons; // Array to hold health UI icons

    [Header("Element Selection")]
    public Image Lightning, Fire, Ice, Earth; // Cooldown progress images
    public GameObject lightningSelect, fireSelect, iceSelect, earthSelect; // Element selectors

    [Header("Cooldown Timers")]
    private float iceCooldown, fireCooldown, earthCooldown, lightningCooldown;
    private float iceTime, fireTime, earthTime, lightningTime;
    private bool iceSent, fireSent, earthSent, lightningSent;

    [Header("Shooter Reference")]
    public GameObject Shooter;

    private int health = 5; // Initial health
    private bool bossStart = true; // Tracks boss initialization

    void Start()
    {
        // Initialize element selection
        lightningSelect.SetActive(true);
        fireSelect.SetActive(false);
        iceSelect.SetActive(false);
        earthSelect.SetActive(false);

        // Find Shooter GameObject if not assigned
        if (Shooter == null)
            Shooter = GameObject.Find("Shooter");
    }

    void Update()
    {
        UpdateHealthDisplay();
        HandleCooldowns();
        InitializeBossState();
    }

    /// <summary>
    /// Updates the health display based on the current health value.
    /// </summary>
    private void UpdateHealthDisplay()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].SetActive(i < health);
        }
    }

    /// <summary>
    /// Handles the cooldown logic for all elements.
    /// </summary>
    private void HandleCooldowns()
    {
        HandleCooldown(ref iceTime, iceCooldown, Ice, "IceEnable", ref iceSent);
        HandleCooldown(ref fireTime, fireCooldown, Fire, "FireEnable", ref fireSent);
        HandleCooldown(ref earthTime, earthCooldown, Earth, "EarthEnable", ref earthSent);
        HandleCooldown(ref lightningTime, lightningCooldown, Lightning, "LightningEnable", ref lightningSent);
    }

    /// <summary>
    /// Handles cooldown for a specific element.
    /// </summary>
    private void HandleCooldown(ref float currentTime, float cooldown, Image elementImage, string enableMethod, ref bool sent)
    {
        if (currentTime < cooldown)
        {
            sent = false;
            elementImage.fillAmount = currentTime / cooldown;
            currentTime += Time.deltaTime;
        }
        else if (!sent)
        {
            elementImage.fillAmount = 1;
            sent = true;
            Shooter.SendMessage(enableMethod, SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// Initializes the boss state and prepares all elements.
    /// </summary>
    private void InitializeBossState()
    {
        if (!bossStart) return;

        Shooter.SendMessage("LightningReady", SendMessageOptions.DontRequireReceiver);
        Shooter.SendMessage("EarthReady", SendMessageOptions.DontRequireReceiver);
        Shooter.SendMessage("FireReady", SendMessageOptions.DontRequireReceiver);
        Shooter.SendMessage("IceReady", SendMessageOptions.DontRequireReceiver);

        bossStart = false;
    }

    /// <summary>
    /// Activates the currently selected element UI.
    /// </summary>
    /// <param name="index">Index of the active element (1 = Lightning, 2 = Fire, 3 = Ice, 4 = Earth).</param>
    void ActiveElement(int index)
    {
        lightningSelect.SetActive(index == 1);
        fireSelect.SetActive(index == 2);
        iceSelect.SetActive(index == 3);
        earthSelect.SetActive(index == 4);
    }

    /// <summary>
    /// Reduces health by the specified damage amount.
    /// </summary>
    void Hurt(int damage)
    {
        health = Mathf.Max(health - damage, 0); // Ensure health doesn't drop below 0
    }

    /// <summary>
    /// Increases health by 1.
    /// </summary>
    void Heal()
    {
        health = Mathf.Min(health + 1, healthIcons.Length); // Ensure health doesn't exceed max health
    }

    /// <summary>
    /// Sets the cooldown for Ice and starts tracking.
    /// </summary>
    void IceCooldown(float cooldown)
    {
        iceCooldown = cooldown;
        iceTime = 0f;
    }

    /// <summary>
    /// Sets the cooldown for Fire and starts tracking.
    /// </summary>
    void FireCooldown(float cooldown)
    {
        fireCooldown = cooldown;
        fireTime = 0f;
    }

    /// <summary>
    /// Sets the cooldown for Earth and starts tracking.
    /// </summary>
    void EarthCooldown(float cooldown)
    {
        earthCooldown = cooldown;
        earthTime = 0f;
    }

    /// <summary>
    /// Sets the cooldown for Lightning and starts tracking.
    /// </summary>
    void LightningCooldown(float cooldown)
    {
        lightningCooldown = cooldown;
        lightningTime = 0f;
    }
}
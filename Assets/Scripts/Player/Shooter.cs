using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Movement and Components
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform spellShooter;

    // Projectile Prefabs
    [SerializeField] private GameObject firemagic;
    [SerializeField] private GameObject lightningmagic;
    [SerializeField] private GameObject icemagic;
    [SerializeField] private GameObject earthmagic;

    [SerializeField] private GameObject firemagic2;
    [SerializeField] private GameObject lightningmagic2;
    [SerializeField] private GameObject icemagic2;
    [SerializeField] private GameObject earthmagic2;

    private GameObject currentProjectile;
    private GameObject currentProjectile2;

    // Aiming
    [SerializeField] private GameObject aim;

    // Audio
    [SerializeField] private AudioSource iceM1;
    [SerializeField] private AudioSource fireM1;
    [SerializeField] private AudioSource lightningM1;
    [SerializeField] private AudioSource earthM1;

    // Cooldowns
    private bool fireReady = true;
    private bool iceReady = true;
    private bool lightningReady = true;
    private bool earthReady = true;

    [SerializeField] private GameObject gameUI;

    // Current Element
    private enum Element { Lightning, Fire, Ice, Earth }
    private Element currentElement;

    // Shooting Force
    private float shotForce = 20f;

    private bool isPlaying = true;

    void Start()
    {
        // Initialize to lightning by default
        currentProjectile = lightningmagic;
        currentProjectile2 = lightningmagic2;
        currentElement = Element.Lightning;
    }

    void Update()
    {
        if (!isPlaying) return;

        // Handle movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Handle element switching
        if (Input.GetButtonDown("lightning"))
            SwitchElement(Element.Lightning);
        if (Input.GetButtonDown("fire"))
            SwitchElement(Element.Fire);
        if (Input.GetButtonDown("ice"))
            SwitchElement(Element.Ice);
        if (Input.GetButtonDown("earth"))
            SwitchElement(Element.Earth);

        // Handle shooting input
        if (Input.GetButtonDown("M1"))
            Shoot();
        if (Input.GetButtonDown("M2"))
            Shoot2();
    }

    void SwitchElement(Element element)
    {
        currentElement = element;

        switch (element)
        {
            case Element.Lightning:
                currentProjectile = lightningmagic;
                currentProjectile2 = lightningmagic2;
                shotForce = 20f;
                break;

            case Element.Fire:
                currentProjectile = firemagic;
                currentProjectile2 = firemagic2;
                shotForce = 15f;
                break;

            case Element.Ice:
                currentProjectile = icemagic;
                currentProjectile2 = icemagic2;
                shotForce = 10f;
                break;

            case Element.Earth:
                currentProjectile = earthmagic;
                currentProjectile2 = earthmagic2;
                shotForce = 10f;
                break;
        }
    }

    void Shoot()
    {
        if (!CanShoot(currentElement)) return;

        GameObject bullet = Instantiate(currentProjectile, aim.transform.position, spellShooter.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(spellShooter.up * shotForce, ForceMode2D.Impulse);

        PlaySound(currentElement, true);
        StartCooldown(currentElement);
    }

    void Shoot2()
    {
        if (!CanShoot(currentElement)) return;

        GameObject bullet2 = Instantiate(currentProjectile2, aim.transform.position, spellShooter.rotation);
        Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
        rb.AddForce(spellShooter.up * shotForce, ForceMode2D.Impulse);

        PlaySound(currentElement, false);
        StartCooldown(currentElement);
    }

    bool CanShoot(Element element)
    {
        switch (element)
        {
            case Element.Lightning: return lightningReady;
            case Element.Fire: return fireReady;
            case Element.Ice: return iceReady;
            case Element.Earth: return earthReady;
            default: return false;
        }
    }

    void StartCooldown(Element element)
    {
        switch (element)
        {
            case Element.Lightning:
                lightningReady = false;
                StartCoroutine(CooldownTimer(1f, () => lightningReady = true));
                gameUI.SendMessage("LightningCooldown", 1f);
                break;

            case Element.Fire:
                fireReady = false;
                StartCoroutine(CooldownTimer(1.5f, () => fireReady = true));
                gameUI.SendMessage("FireCooldown", 1.5f);
                break;

            case Element.Ice:
                iceReady = false;
                StartCoroutine(CooldownTimer(0.5f, () => iceReady = true));
                gameUI.SendMessage("IceCooldown", 0.5f);
                break;

            case Element.Earth:
                earthReady = false;
                StartCoroutine(CooldownTimer(1.5f, () => earthReady = true));
                gameUI.SendMessage("EarthCooldown", 1.5f);
                break;
        }
    }

    private IEnumerator CooldownTimer(float cooldownTime, System.Action resetAction)
    {
        yield return new WaitForSeconds(cooldownTime);
        resetAction();
    }

    void PlaySound(Element element, bool isPrimary)
    {
        switch (element)
        {
            case Element.Lightning:
                if (isPrimary) lightningM1.Play();
                break;
            case Element.Fire:
                if (isPrimary) fireM1.Play();
                break;
            case Element.Ice:
                if (isPrimary) iceM1.Play();
                break;
            case Element.Earth:
                if (isPrimary) earthM1.Play();
                break;
        }
    }

    public void Death()
    {
        isPlaying = false;
    }

    public void Win()
    {
        isPlaying = false;
        aim.SetActive(false);
    }
}
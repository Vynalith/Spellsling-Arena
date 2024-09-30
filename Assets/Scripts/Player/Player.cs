using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public int health;
    public int maxHealth;
    public Rigidbody2D rb;
    public Rigidbody2D rb2;
    public GameObject damage;
    public Camera camera;
    public Animator animator;
    public GameObject Shooter;
    public GameObject gameUI;
    public AudioSource song1Intro;
    public AudioSource song1Loop;
    public AudioSource bossSongIntro;
    public AudioSource bossSongLoop;
    public AudioSource winSong;
    public GameObject Reset;
    public float countdown;

    private Vector2 movement;
    private Vector2 mousePos;
    private Vector3 flashback;
    private int element;
    private bool Playing;
    private float timer;
    private float bossTimer;
    private float songCount = 13.35f;
    private float bossSongCount = 8.15f;
    private int song1Change = 0;
    private int bossSongChange = 0;
    private bool bossSongStarted;
    private float Deadtimer;
    private bool isDead;

    void Start()
    {
        Playing = true;
        element = 1;
        animator.SetInteger("element", element);
        health = maxHealth - 2;
        isDead = false;
    }

    void Update()
    {
        if (Playing)
        {
            HandleInput();
            HandleAnimations();
            HandleElementSwitch();
            flashback = transform.position;
        }

        HandleMusic();

        if (isDead)
        {
            Deadtimer += Time.deltaTime;
            if (Deadtimer >= countdown)
            {
                Reset.SendMessage("LoadScene", "Menu");
            }
        }
    }

    void FixedUpdate()
    {
        if (Playing)
        {
            MovePlayer();
        }
    }

    private void HandleInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void HandleAnimations()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void HandleElementSwitch()
    {
        if (Input.GetButtonDown("lightning"))
        {
            SetElement(1);
        }
        if (Input.GetButtonDown("fire"))
        {
            SetElement(2);
        }
        if (Input.GetButtonDown("ice"))
        {
            SetElement(3);
        }
        if (Input.GetButtonDown("earth"))
        {
            SetElement(4);
        }
    }

    private void SetElement(int newElement)
    {
        gameUI.SendMessage("ActiveElement", newElement);
        element = newElement;
        animator.SetInteger("element", element);
    }

    private void HandleMusic()
    {
        if (timer >= songCount && song1Change == 0)
        {
            song1Change = 1;
        }
        else if (song1Change == 0)
        {
            timer += Time.deltaTime;
        }

        if (song1Change == 1)
        {
            song1Intro.Stop();
            song1Loop.Play();
            song1Change = 2;
        }

        if (bossSongStarted)
        {
            if (bossTimer >= bossSongCount && bossSongChange == 0)
            {
                bossSongChange = 1;
            }
            else if (bossSongChange == 0)
            {
                bossTimer += Time.deltaTime;
            }

            if (bossSongChange == 1)
            {
                bossSongIntro.Stop();
                bossSongLoop.Play();
                bossSongChange = 2;
            }
        }
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb2.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2.rotation = angle;
    }

    void StartBossMusic()
    {
        song1Loop.Stop();
        bossSongIntro.Play();
        bossSongStarted = true;
    }

    void PlayWinSong()
    {
        StopAllMusic();
        winSong.Play();
    }

    private void StopAllMusic()
    {
        song1Intro.Stop();
        song1Loop.Stop();
        bossSongIntro.Stop();
        bossSongLoop.Stop();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyProjectile"))
        {
            HurtMe(1);
            Destroy(other.gameObject);
        }
    }

    private void HurtMe(int damage)
    {
        health -= damage;
        gameUI.SendMessage("Hurt", damage);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            PlayDamageAnimation();
        }
    }

    private void Die()
    {
        Playing = false;
        isDead = true;
        Shooter.gameObject.SendMessage("Death");
        animator.Play("DEATH");
    }

    private void PlayDamageAnimation()
    {
        switch (element)
        {
            case 1:
                animator.Play("LightningDamage");
                break;
            case 2:
                animator.Play("FireDamage");
                break;
            case 3:
                animator.Play("IceDamage");
                break;
            case 4:
                animator.Play("EarthDamage");
                break;
        }
    }

    void Heal()
    {
        if (health < maxHealth)
        {
            health += 1;
            gameUI.SendMessage("Heal", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void LightningAttacks()
    {
        animator.Play("Lightning m1");
    }

    public void FireAttacks()
    {
        animator.Play("Fire m1");
    }

    public void IceAttacks()
    {
        animator.Play("Ice m1");
    }

    public void EarthAttacks()
    {
        animator.Play("Earth m1");
    }

    public void Win()
    {
        Playing = false;
        Shooter.gameObject.SendMessage("Win");
        PlayWinAnimation();
    }

    private void PlayWinAnimation()
    {
        switch (element)
        {
            case 1:
                animator.Play("WIN! Lightning");
                break;
            case 2:
                animator.Play("FireWIN");
                break;
            case 3:
                animator.Play("IceWIN");
                break;
            case 4:
                animator.Play("EarthWIN");
                break;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

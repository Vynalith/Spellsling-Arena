using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public int maxHealth = 10; // Starting health
    private int currentHealth;
    private bool isDead = false; // Tracks if the player is dead
    private bool isPlaying = true;

    [Header("Animation")]
    public Animator animator;

    [Header("UI & Elements")]
    public GameObject gameUI;
    public GameObject shooter;
    public GameObject gameOverUI;

    private Element currentElement = Element.Lightning;

    [Header("Music & Sound")]
    public AudioSource songIntro;
    public AudioSource songLoop;
    public AudioSource bossIntro;
    public AudioSource bossLoop;
    public AudioSource winMusic;

    private bool isBossMusicPlaying = false;

    [Header("Timers")]
    public float deathCountdown = 3f;
    private float deathTimer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateElement(currentElement);

        // Start music
        if (songIntro != null) songIntro.Play();
        if (songLoop != null) songLoop.Stop();
        if (bossIntro != null) bossIntro.Stop();
        if (bossLoop != null) bossLoop.Stop();

        isBossMusicPlaying = false;
        isDead = false;
        isPlaying = true;
    }

    private void Update()
    {
        if (isDead)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathCountdown)
            {
                LoadScene("Menu");
            }
        }
    }

    private void UpdateElement(Element element)
    {
        currentElement = element;
        animator.SetInteger("element", (int)element);

        // Update UI for active element
        gameUI?.SendMessage("ActiveElement", (int)element, SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        else if (collision.CompareTag("EnemyProjectile"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Remaining health: {currentHealth}");

        // Notify UI of damage
        gameUI?.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.Play($"{currentElement}Damage");
        }
    }

    public void HurtMe(int damage)
    {
        TakeDamage(damage); // Simplify external damage calls
    }

    private void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        gameUI?.SendMessage("Heal", amount, SendMessageOptions.DontRequireReceiver);
    }

    private void Die()
    {
        isDead = true;
        isPlaying = false;
        Debug.Log("Player is dead!");

        // Trigger shooter and death animations
        shooter.SendMessage("Death");
        animator.Play("Death");

        // Show game over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void StartBossMusic()
    {
        if (!isBossMusicPlaying)
        {
            songLoop.Stop();
            bossIntro.Play();
            StartCoroutine(PlayBossLoop());
            isBossMusicPlaying = true;
        }
    }

    private IEnumerator PlayBossLoop()
    {
        yield return new WaitForSeconds(bossIntro.clip.length);
        bossIntro.Stop();
        bossLoop.Play();
    }

    public void PlayWinMusic()
    {
        songIntro.Stop();
        songLoop.Stop();
        bossIntro.Stop();
        bossLoop.Stop();
        winMusic.Play();
    }

    public void Win()
    {
        isPlaying = false;
        shooter.SendMessage("Win");
        animator.Play($"{currentElement}WIN");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

public enum Element
{
    Lightning = 1,
    Fire = 2,
    Ice = 3,
    Earth = 4
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Movement speed

    private Vector2 movement; // Stores input movement
    private Rigidbody2D rb; // Reference to Rigidbody2D
    private Animator animator; // Reference to Animator

    [Header("Mouse Rotation (Optional)")]
    public bool rotateTowardsMouse = true; // Should the player rotate towards the mouse
    private Camera mainCamera; // Main camera reference

    void Start()
    {
        // Get required components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main; // Cache the main camera
    }

    void Update()
    {
        // Capture input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Update animator with movement data
        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        if (rb != null)
        {
            Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }

        // Rotate towards the mouse if enabled
        if (rotateTowardsMouse && mainCamera != null)
        {
            RotateTowardsMouse();
        }
    }

    /// <summary>
    /// Rotates the player to face the mouse position.
    /// </summary>
    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
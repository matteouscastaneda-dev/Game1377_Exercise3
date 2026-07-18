using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private int currentLives = 3;
    [SerializeField] private float invincibleTime = 3f;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private Animator animator;

    private AsteroidsPlayerController controller;
    private bool isDead;
    private bool isInvincible;
    private float invincibleTimer;

    void Start()
    {
        controller = GetComponent<AsteroidsPlayerController>();
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    /// <summary>
    /// Handles life loss death effects
    /// then respawns or ends the game
    /// </summary>
    public void ResolveHit()
    {
        if (isInvincible || isDead)
        {
            return;
        }

        controller.ResetMovement();

        currentLives--;

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        if (currentLives > 0)
        {
            Respawn();
        }
        else
        {
            currentLives = 0;
            isDead = true;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Moves the player back to center
    /// resets its movement and starts the invincibility window
    /// </summary>
    private void Respawn()
    {
        transform.position = Vector3.zero;
        controller.ResetMovement();

        isInvincible = true;
        invincibleTimer = invincibleTime;
    }

    /// <summary>
    /// Adds one life
    /// </summary>
    public void AddLife()
    {
        currentLives++;
    }
}
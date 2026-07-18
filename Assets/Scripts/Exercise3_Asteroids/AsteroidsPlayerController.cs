using System.Collections;
using UnityEngine;

public class AsteroidsPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float thrustForce = 500f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private float rotationInput;
    private float thrustInput;

    [Header("Hyperspace")]
    [SerializeField] private float safeTeleportRadius = 2f;
    [SerializeField] private AudioClip hyperspaceSound;
    [SerializeField] private Animator animator;

    [Header("Shooting")]
    [SerializeField] private float fireCooldown = 0.25f;
    [SerializeField] private AudioClip fireSound;
    private float nextFireTime;

    [Header("Thrust")]
    [SerializeField] private AudioSource thrustAudioSource;

    private float baseRotationSpeed;
    private float baseThrustForce;
    private float bulletScale = 1f;

    private PlayerLives playerLives;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLives = GetComponent<PlayerLives>();

        baseRotationSpeed = rotationSpeed;
        baseThrustForce = thrustForce;
    }

    void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");
        thrustInput = Input.GetAxis("Vertical");
        HandleRotation();
        HandleFire();
        HandleHyperspace();
    }

    void FixedUpdate()
    {
        HandleThrust();
    }

    /// <summary>
    /// Rotates the ship left or right based on input
    /// </summary>
    private void HandleRotation()
    {
        transform.Rotate(Vector3.back, rotationInput * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Applies forward thrust and handles thruster animation and sound while thrusting
    /// </summary>
    private void HandleThrust()
    {
        bool isThrusting = thrustInput > 0;

        if (isThrusting)
        {
            rb.AddForce(transform.up * thrustForce, ForceMode2D.Force);
        }

        if (animator != null)
        {
            animator.SetBool("IsThrusting", isThrusting);
        }

        if (thrustAudioSource != null)
        {
            if (isThrusting && !thrustAudioSource.isPlaying)
            {
                thrustAudioSource.Play();
            }
            else if (!isThrusting && thrustAudioSource.isPlaying)
            {
                thrustAudioSource.Stop();
            }
        }
    }

    /// <summary>
    /// Fires a bullet when the fire button is pressed and is off cooldown
    /// </summary>
    private void HandleFire()
    {
        if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    /// <summary>
    /// Spawns a bullet
    /// scales it for the bigger-bullets powerup and plays the fire sound
    /// </summary>
    private void FireBullet()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.localScale *= bulletScale;

        if (fireSound != null)
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }

    /// <summary>
    /// Triggers teleport when the hyperspace button is pressed.
    /// </summary>
    private void HandleHyperspace()
    {
        if (Input.GetButtonDown("HyperSpace"))
        {
            TeleportToRandomLocation();
        }
    }

    /// <summary>
    /// Teleports the ship to a random spot that is clear of asteroids with sound and animation
    /// </summary>
    private void TeleportToRandomLocation()
    {
        Vector2 teleportPosition;
        int attempts = 0;

        do
        {
            float randomLocationX = Random.Range(ScreenBounds.ScreenLeft, ScreenBounds.ScreenRight);
            float randomLocationY = Random.Range(ScreenBounds.ScreenBottom, ScreenBounds.ScreenTop);
            teleportPosition = new Vector2(randomLocationX, randomLocationY);
            attempts++;
        }
        while (IsAsteroidNearby(teleportPosition) && attempts < 100);

        if (hyperspaceSound != null)
        {
            AudioSource.PlayClipAtPoint(hyperspaceSound, transform.position);
        }
        if (animator != null)
        {
            animator.SetTrigger("Hyperspace");
        }
        transform.position = teleportPosition;
    }

    /// <summary>
    /// Starts the timed speed and rotation boost
    /// restarting it if one is already running
    /// </summary>
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        StopCoroutine(nameof(SpeedBoostRoutine));
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    /// <summary>
    /// Boosts rotation and thrust for a set time
    /// then restores the base values
    /// </summary>
    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        rotationSpeed = baseRotationSpeed * multiplier;
        thrustForce = baseThrustForce * multiplier;
        yield return new WaitForSeconds(duration);
        rotationSpeed = baseRotationSpeed;
        thrustForce = baseThrustForce;
    }

    /// <summary>
    /// Starts the timed bigger bullets boost
    /// restarting it if one is already running
    /// </summary>
    public void ApplyBiggerBullets(float multiplier, float duration)
    {
        StopCoroutine(nameof(BiggerBulletsRoutine));
        StartCoroutine(BiggerBulletsRoutine(multiplier, duration));
    }

    /// <summary>
    /// Increases the bullet scale for a set time
    /// then resets it to normal
    /// </summary>
    private IEnumerator BiggerBulletsRoutine(float multiplier, float duration)
    {
        bulletScale = multiplier;
        yield return new WaitForSeconds(duration);
        bulletScale = 1f;
    }

    /// <summary>
    /// Zeroes out the ships velocity and spin
    /// </summary>
    public void ResetMovement()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            playerLives.ResolveHit();
        }
    }

    /// <summary>
    /// Returns true if an asteroid is within the safe radius of the position
    /// </summary>
    private bool IsAsteroidNearby(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, safeTeleportRadius);
        if (hit != null && hit.CompareTag("Asteroid"))
        {
            return true;
        }
        return false;
    }
}
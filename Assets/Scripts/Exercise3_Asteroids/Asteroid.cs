using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidSize { Small, Medium, Large }

    [SerializeField] private AsteroidSize size;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float minRotationSpeed = -180f;
    [SerializeField] private float maxRotationSpeed = 180f;

    [SerializeField] private int childCount = 2;

    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private float explosionDuration = 0.5f;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private AsteroidSpawner spawner;

    private bool isBreaking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        rb.linearVelocity = direction * speed;
        rb.angularVelocity = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    /// <summary>
    /// Spawns smaller size on break
    /// plays the explosion sound and animation then destroys it
    /// </summary>
    private void BreakAsteroid()
    {
        if (isBreaking)
        {
            return;
        }
        isBreaking = true;

        if (size != AsteroidSize.Small)
        {
            SpawnChildren(GetSmallerSize());
        }

        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        if (animator != null)
        {
            animator.SetTrigger("Explode");
        }

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        Destroy(gameObject, explosionDuration);
    }

    /// <summary>
    /// Spawns the set of smaller child asteroids
    /// </summary>
    private void SpawnChildren(AsteroidSize childSize)
    {
        for (int spawnIndex = 0; spawnIndex < childCount; spawnIndex++)
        {
            spawner.SpawnAsteroid(transform.position, childSize);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BreakAsteroid();
        }
    }

    /// <summary>
    /// Returns the next size down from this asteroids current size
    /// </summary>
    private AsteroidSize GetSmallerSize()
    {
        size--;
        return size;
    }

    /// <summary>
    /// Spawner reference so this asteroid can request child spawns
    /// </summary>
    public void SetSpawner(AsteroidSpawner newSpawner)
    {
        spawner = newSpawner;
    }
}
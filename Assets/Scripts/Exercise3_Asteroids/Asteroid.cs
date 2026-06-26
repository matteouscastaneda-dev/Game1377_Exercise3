/*
 * Assignment: Asteroids Game - Asteroid Script - PART 2
 * 
 * Objective: Create a functional asteroid script. This script will be responsible for the functionality of the asteroids.
 * this should include initial velocity, angular velocity, and breaking into smaller asteroids when destroyed.
 * Remember, asteroids should only spawn through the AsteroidSpawner script. 
 
* Requirements:
* 1. (Done) The asteroid should start with a constant speed but a random angular velocity. Both of these are set in the Rigidbody2D
*       The movement direction of the asteroid should not change. 
*       Hint: All movement for the asteroid should be done via a Rigidbody2D and should be able to be set at Start.
* 2.(Done) When the asteroid is destroyed, it should spawn two smaller asteroids if it is not already the smallest size. 
*       Hint: How can you use a function to set the AsteroidSpawner variable from a different script?
* 3. (Done) When the astroid hits the player, it should destroy the player. 
*/

using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidSize { Small, Medium, Large }

    [SerializeField] private AsteroidSize size;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float minRotationSpeed = -180f;
    [SerializeField] private float maxRotationSpeed = 180f;

    [SerializeField] private int ChildCount = 2;

    private Rigidbody2D rb;
    private AsteroidSpawner spawner;

    void Start()
    {
        //Give the asteroid a random linear and angular velocity.
        rb = GetComponent<Rigidbody2D>();

        Vector2 velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        rb.linearVelocity = velocity * speed;
        rb.angularVelocity = Random.Range(minRotationSpeed, maxRotationSpeed);

        spawner = Object.FindAnyObjectByType<AsteroidSpawner>();
    }

    void Update()
    {
  
    }

    private void BreakAsteroid()
    {
        if (size != AsteroidSize.Small)
        {
            SpawnChildren(GetSmallerSize());
        }

        Destroy(gameObject);
    }

    // Spawns 2 smaller asteroids
    private void SpawnChildren(AsteroidSize childSize)
    {
       for (int spawnIndex = 0; spawnIndex < ChildCount; spawnIndex ++)
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
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    private AsteroidSize GetSmallerSize()
    {
        switch (size)
        {
            case AsteroidSize.Large:
                return AsteroidSize.Medium;
            case AsteroidSize.Medium:
                return AsteroidSize.Small;
            default:
                return AsteroidSize.Small;

        }
    }
}

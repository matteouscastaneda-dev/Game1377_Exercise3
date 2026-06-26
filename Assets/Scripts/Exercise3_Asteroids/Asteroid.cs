/*
 * Assignment: Asteroids Game - Asteroid Script - PART 2
 * 
 * Objective: Create a functional asteroid script. This script will be responsible for the functionality of the asteroids.
 * this should include initial velocity, angular velocity, and breaking into smaller asteroids when destroyed.
 * Remember, asteroids should only spawn through the AsteroidSpawner script. 
 
* Requirements:
* 1. The asteroid should start with a constant speed but a random angular velocity. Both of these are set in the Rigidbody2D
*       The movement direction of the asteroid should not change. 
*       Hint: All movement for the asteroid should be done via a Rigidbody2D and should be able to be set at Start.
* 2. When the asteroid is destroyed, it should spawn two smaller asteroids if it is not already the smallest size. 
*       Hint: How can you use a function to set the AsteroidSpawner variable from a different script?
* 3. When the astroid hits the player, it should destroy the player. 
*/

using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidSize { Small, Medium, Large }

    [SerializeField] private AsteroidSize size;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float minRotationSpeed = -180f;
    [SerializeField] private float maxRotationSpeed = 180f;

    private Rigidbody2D rb;
    private AsteroidSpawner spawner;
    private Vector2 velocity;

    void Start()
    {
        //Give the asteroid a random direction and speed
        rb = GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        rb.linearVelocity = direction * speed;
        rb.angularVelocity = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {

    }

    private void BreakAsteroid()
    {

    }

    private void SpawnChildren(AsteroidSize childSize)
    {
        
    }
}

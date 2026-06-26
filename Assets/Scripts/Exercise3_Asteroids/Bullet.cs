/*
 * Assignment: AsteroidsGame - Bullet Script - 2
 * 
 * Objective:
 * Implement a player controller for a spaceship in an Asteroids prototype. The player should be able to rotate the ship,
 * move forward, wrap around the screen, and shoot bullets. 
 * 
 * Requirements:   
 * PART 2: Shooting
 * 1. The bullets should start off moving in the direciton they are spawned at a speed set by bulletSpeed. 
 *      This should be set in the Start method of this script.
 *      The movement of the bullet should be done with Physics applied to a Rigidbody2D.
 * 2. The bullets should be destroyed after bulletLifetime seconds or when they collide with an asteroid.
 *
 */
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletLifetime = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed;
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

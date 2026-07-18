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
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
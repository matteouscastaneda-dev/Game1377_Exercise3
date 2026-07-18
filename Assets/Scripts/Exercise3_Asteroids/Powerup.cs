using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        ExtraLife,
        SpeedBoost,
        BiggerBullets
    }

    [SerializeField] private PowerupType type;
    [SerializeField] private float effectDuration = 5f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float bulletSizeMultiplier = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        switch (type)
        {
            case PowerupType.ExtraLife:
                other.GetComponent<PlayerLives>().AddLife();
                break;

            case PowerupType.SpeedBoost:
                other.GetComponent<AsteroidsPlayerController>().ApplySpeedBoost(speedMultiplier, effectDuration);
                break;

            case PowerupType.BiggerBullets:
                other.GetComponent<AsteroidsPlayerController>().ApplyBiggerBullets(bulletSizeMultiplier, effectDuration);
                break;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Assigns this powerup a random type
    /// </summary>
    public void RandomizeType()
    {
        int count = System.Enum.GetValues(typeof(PowerupType)).Length;
        type = (PowerupType)Random.Range(0, count);
    }
}
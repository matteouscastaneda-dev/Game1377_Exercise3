using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupPrefabs;
    [SerializeField] private float spawnInterval = 8f;
    [SerializeField] private float edgePadding = 1f;

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPowerup();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    /// <summary>
    /// Spawns one random powerup prefab at a random spot within the screen bounds
    /// </summary>
    private void SpawnPowerup()
    {
        if (powerupPrefabs == null || powerupPrefabs.Length == 0)
        {
            return;
        }

        float x = Random.Range(ScreenBounds.ScreenLeft + edgePadding, ScreenBounds.ScreenRight - edgePadding);
        float y = Random.Range(ScreenBounds.ScreenBottom + edgePadding, ScreenBounds.ScreenTop - edgePadding);
        Vector2 spawnPosition = new Vector2(x, y);

        // pick a random prefab from the array
        int randomIndex = Random.Range(0, powerupPrefabs.Length);
        GameObject chosenPrefab = powerupPrefabs[randomIndex];

        Instantiate(chosenPrefab, spawnPosition, Quaternion.identity);
    }
}
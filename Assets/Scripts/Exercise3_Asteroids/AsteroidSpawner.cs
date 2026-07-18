using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;

    [SerializeField] private int initialAsteroidCount = 5;

    private float spawnXMax = 0f;
    private float spawnXMin = 0f;
    private float spawnYMax = 0f;
    private float spawnYMin = 0f;
    private float playerSafeDistance = 3;

    void Start()
    {
        float screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = Camera.main.aspect * screenHalfHeight;
        spawnXMax = screenHalfWidth + playerSafeDistance;
        spawnXMin = -screenHalfWidth - playerSafeDistance;
        spawnYMax = screenHalfHeight + playerSafeDistance;
        spawnYMin = -screenHalfHeight - playerSafeDistance;
        SpawnInitialAsteroids();
    }

    /// <summary>
    /// Spawns the starting wave at safe random positions
    /// </summary>
    private void SpawnInitialAsteroids()
    {
        for (int asteroidCount = 0; asteroidCount < initialAsteroidCount; asteroidCount++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();

            SpawnAsteroid(spawnPosition, Asteroid.AsteroidSize.Large);
        }
    }

    /// <summary>
    /// Spawns a single asteroid of the given size at the given position 
    /// and passes it a spawner reference
    /// </summary>
    public void SpawnAsteroid(Vector3 position, Asteroid.AsteroidSize size)
    {
        GameObject asteroidToSpawn = asteroidPrefabs[(int)size];

        GameObject newSpawn = Instantiate(asteroidToSpawn, position, Quaternion.identity);

        Asteroid asteroidScript = newSpawn.GetComponent<Asteroid>();
        if (asteroidScript != null)
        {
            asteroidScript.SetSpawner(this);
        }
    }

/// <summary>
/// Returns a random spawn position that is far enough from the center to be safe for the player
/// </summary>
private Vector3 GetRandomSpawnPosition()
{
    Vector3 spawnPosition;

    do
    {
        float spawnLocationX = Random.Range(spawnXMin, spawnXMax);
        float spawnLocationY = Random.Range(spawnYMin, spawnYMax);
        spawnPosition = new Vector2(spawnLocationX, spawnLocationY);
    } while (Vector3.Distance(spawnPosition, Vector3.zero) < playerSafeDistance);
    return spawnPosition;
}
}
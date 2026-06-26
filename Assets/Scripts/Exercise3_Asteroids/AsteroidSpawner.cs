/*
 * Assignment: Asteroids Game - AstroidSpawner Script - PART 2
 * 
 * Objective: Create a functional asteroid spawning script. This script will be responsible for spawning
 * asteroids at the start of the game, as well as spawning smaller asteroids when larger asteroids are destroyed. 
 * ALL ASTEROID SPAWNING SHOULD OCCUR THROUGH THIS SCRIPT. 
 
* Requirements:
* 1. Fill in the SpawnAsteroids method to spawn an asteroid at a location specified by the position and size parameters.
*       Hint: You may need to create a variable for the prefabs you need. 
*       Hint: Use the spawnXMax, spawnXMin, spawnYMax, and spawnYMin variables to determine where the asteroids can spawn.
* 2. Spawn a variable number of asteroids at the start of the game using the SpawnInitialAsteroids() method.
*       This should be determined by a private variable that can be set in the editor (set it to 5 in the Inspector). 
*       The asteroids should spawn at random positions within the camera view, but not too close to the center (0,0)
*       where the player will be (at least 3 units away from the center in any direction).
*       Hint: Vector3.Distance can tell you how far one point is away from another. 
*/
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidSmallPrefab;
    [SerializeField] private GameObject asteroidMediumPrefab;
    [SerializeField] private GameObject asteroidLargePrefab;

    // These variables determine the spawn area for the asteroids.
    // They are calculated at Start based off of the camera size. 
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

    void Update()
    {
       
    }

    private void SpawnInitialAsteroids()
    {
        // Spawn initial asteroids at random positions. Ensure that they do not spawn where the player is located. 
    }

    public void SpawnAsteroid(Vector3 position, Asteroid.AsteroidSize size)
    {
        GameObject asteroidToSpawn;

        switch (size)
        {
            case Asteroid.AsteroidSize.Small:
                asteroidToSpawn = asteroidSmallPrefab;
                break;
            case Asteroid.AsteroidSize.Medium:
                asteroidToSpawn = asteroidMediumPrefab;
                break;
            case Asteroid.AsteroidSize.Large:
                asteroidToSpawn = asteroidLargePrefab;
                break;
            default:
                asteroidToSpawn = asteroidSmallPrefab;
                break;
        }

        Instantiate(asteroidToSpawn, position, Quaternion.identity);
    }
}
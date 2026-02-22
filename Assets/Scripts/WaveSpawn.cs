using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawn : MonoBehaviour
{
    [System.Serializable]
    public class WaveContent
    {
        // Array of enemy prefabs that can be spawned in this wave
        [SerializeField][NonReorderable] GameObject[] WaveSpawn;

        // Returns the list of enemy prefabs for this wave
        public GameObject[] GetMonsterSpawnList()
        {
            return WaveSpawn;
        }
    }

    [SerializeField][NonReorderable] WaveContent[] waves; // Array of all waves
    int currentWave = 0; // Tracks which wave the player is currently on
    float spawnRange = 100; // The radius around the spawner that enemies can spawn
    public List<GameObject> currentMonster; // Keeps track of all living enemies in the current wave

    void Start()
    {
        SpawnWave(); // Spawns the first wave when the game starts
    }

    void Update()
    {
        // If all enemies in the current wave are dead, move to the next wave
        if (currentMonster.Count == 0)
        {
            currentWave++;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        // Loops through each enemy in the current wave and spawns them
        for (int i = 0; i < waves[currentWave].GetMonsterSpawnList().Length; i++)
        {
            // Instantiates the enemy prefab at a random location
            GameObject newspawn = Instantiate(waves[currentWave].GetMonsterSpawnList()[i], FindSpawnLocation(), Quaternion.identity);

            currentMonster.Add(newspawn); // Adds the spawned enemy to the living enemies list

            // Gets the EnemyAI script on the spawned enemy and tells it which spawner it belongs to
            EnemyAI monster = newspawn.GetComponent<EnemyAI>();
            monster.SetSpawner(this);

            // Gets the EnemyHit script on the spawned enemy and tells it which spawner it belongs to
            // This allows EnemyHit to remove itself from the list when it dies
            EnemyHit enemyHit = newspawn.GetComponent<EnemyHit>();
            enemyHit.SetSpawner(this);
        }
    }

    Vector3 FindSpawnLocation()
    {
        Vector3 SpawnPos;

        // Calculates a random x and z position within the spawn range
        float xlocation = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zlocation = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float ylocation = transform.position.y;

        SpawnPos = new Vector3(xlocation, ylocation, zlocation);

        // Checks if there is ground below the spawn position using a raycast
        if (Physics.Raycast(SpawnPos, Vector3.down, 5))
        {
            return SpawnPos; // Returns the valid spawn position
        }
        else
        {
            return FindSpawnLocation(); // If no ground found, tries again recursively
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> enemyWaves;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping = true;
    WaveConfig currentWave;


    private void Start() => StartCoroutine(PreWarm());

    public WaveConfig GetCurrentWave() => currentWave;

    IEnumerator PreWarm()
    {
        
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnWaves());
    }

    // Coroutine for spawning waves
    IEnumerator SpawnWaves()
    {
        do
        {
            // Loop through every wave
            foreach (WaveConfig enemyWave in enemyWaves)
            {
                currentWave = enemyWave;

                // Instantiate all enemy in the wave config 
                for (int i = 0; i < enemyWave.GetEnemyPrefabCount(); i++)
                {
                    // Instantiat enemy and set it as a child game object
                    Instantiate(enemyWave.GetEnemyPrefab(i),
                                enemyWave.GetStartingWaypoint().position,
                                Quaternion.Euler(0, 0, 180),
                                transform);

                    // Random wait time between spawning of enemy
                    yield return new WaitForSeconds(enemyWave.GetRandomSpawnTime());
                }
                // Wait time between switching of waves
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping);
    }
}

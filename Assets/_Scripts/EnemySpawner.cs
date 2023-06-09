using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> enemyWaves;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfig currentWave;

    private void Start() => StartCoroutine(SpawnWaves());

    public WaveConfig GetCurrentWave() => currentWave;

    IEnumerator SpawnWaves()
    {
        // Loop through every wave
        foreach (WaveConfig enemyWave in enemyWaves)
        {
            currentWave = enemyWave;

            // Instantiate all enemy in the wave config 
            for (int i = 0; i < enemyWave.GetEnemyPrefabCount(); i++)
            {
                // Instantiate and set as a child
                Instantiate(enemyWave.GetEnemyPrefab(i), 
                            enemyWave.GetStartingWaypoint().position, 
                            Quaternion.identity, 
                            transform);

                // Random wait time between spawning of enemy
                yield return new WaitForSeconds(enemyWave.GetRandomSpawnTime());
            }
            // Wait time between switching of waves
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}

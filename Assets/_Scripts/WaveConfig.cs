using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
    // Prefab Fields
    [SerializeField] List<GameObject> enemyPrefab;
    [SerializeField] GameObject projectilePrefab;

    [SerializeField] Transform waypoints;

    // Move speed of the enemy
    [SerializeField] float moveSpeed = 5f;
    
    // Enemy Spawn Fields
    [SerializeField] float timeBetweenEnemySpawn = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    public int GetEnemyPrefabCount() =>  enemyPrefab.Count;

    public GameObject GetEnemyPrefab(int index) => enemyPrefab[index];

    public float GetMoveSpeed() => moveSpeed;

    public Transform GetStartingWaypoint() => waypoints.GetChild(0);

    public GameObject GetProjectilePrefab() => projectilePrefab;

    public List<Transform> GetWaypoints()
    {
        List<Transform> enemyWaypoint = new List<Transform>();

        foreach(Transform child in this.waypoints)
        {
            enemyWaypoint.Add(child);
        }

        return enemyWaypoint;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawn - spawnTimeVariance, timeBetweenEnemySpawn + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}

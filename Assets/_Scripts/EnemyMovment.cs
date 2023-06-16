using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    private WaveConfig waveConfig;
    private EnemySpawner spawner;
    private List<Transform> waypoints;
    private int index = 0;
    private Vector2 finalPosition;

    private void Awake() => spawner = FindAnyObjectByType<EnemySpawner>();

    void Start()
    {
        // Init fields
        waveConfig = spawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        
        // Helps in destroying game object after reaching final position
        finalPosition = waypoints[waypoints.Count - 1].position;
        
        // Set first waypoint
        transform.position = waypoints[index].position;
    }

    void Update() => MoveEnemyToWaypoint();

    private void MoveEnemyToWaypoint()
    {
        Vector3 targetPos = waypoints[index].position;
        float enemyMoveSpeed = waveConfig.GetMoveSpeed() * Time.deltaTime;

        // Move the enemy towards the target
        transform.position = Vector2.MoveTowards(transform.position, targetPos, enemyMoveSpeed);

        float distance = Vector2.Distance(transform.position, targetPos);

        // Check if enemy reached the target 
        if (distance <= 0.1f)
        {
            // Increment the index to switch to new target position
            if (index < (waypoints.Count - 1))
            {
                index++;
            }
        }
        // Method will destroy the enemy game object after they reached the final point
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        // Destroy the game object if enemy reaches final position
        if (Vector2.Distance(transform.position, finalPosition) <= 0.1f)
            Destroy(gameObject);
    }

    private void NewDevlopmentBuid()
    {
        // Destroy this game object is the enemy persist the final way point 
        if(isActiveAndEnabled)
        {
            Destroy(gameObject);
        }
    }
}

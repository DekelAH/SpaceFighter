using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Creating a file to be easliy reached in Unity and to create new instance 
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f; // Do determine the time between enemy object spawn
    [SerializeField] float spawnRandomFactor = 0.3f; // To make the spawns more random;
    [SerializeField] float moveSpeed = 2f; // Speed of the enemy movement
    [SerializeField] int numberOfEnemies = 5; // Number of enemy game objects in wave



    // Giving access to enemy prefab by other scripts
    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    // Giving access to list of way points
    public List<Transform> GetWayPoints() 

    {
        var waveWayPoints = new List<Transform>();

        foreach (Transform wayPoint in pathPrefab.transform)
        {
            // Adding the way point transform(location) to waveWayPoints(list)
            waveWayPoints.Add(wayPoint);
        }

        // Returning wave way points in pathPrefab
        return waveWayPoints; 
    }    
    
    // Giving access to time between spawns
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    // Giving access to spawn random factor
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    // Giving access to number of enemies in wave
    public int GetNumberOfEnemies() { return numberOfEnemies; }

    // Giving access to enemy move speed
    public float GetMoveSpeed() { return moveSpeed; }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs; // List of all waves configs to serialized easily
    [SerializeField] int startingWave = 0; // Setting the starting wave from list
    [SerializeField] bool looping = false; // 

    // Start is called before the first frame update
    // Creating a loop for spawning all waves
    IEnumerator Start()
    {
        do
        {

            yield return StartCoroutine(SpawnAllWaves());

        } while (looping);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex]; // Setting the wave in list as the current wave

            // As wave starts - calling SpawnAllEnemiesInWave() and giving it the current wave
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        // Spawning enemies untill it reach to the set number of enemies
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(), // Getting the enemy prefab
                waveConfig.GetWayPoints()[0].transform.position, // Getting the starting way points position
                Quaternion.identity); // Setting the default rotation

            // Getting the component type enemy pathing and passing into SetWaveConfing
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            // Waiting X time between enemy spawns in wave
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }


}


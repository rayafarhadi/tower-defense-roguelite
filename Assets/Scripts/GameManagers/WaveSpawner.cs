using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    private static int enemiesToKill = 0;
    private static bool waveInProgress = false;
    private static bool waveSpawned = false;
    public bool waveEnded = false;
    public bool waveStarted = false;

    public Wave[] waves;
    public Transform spawnPoint;

    public Button startWaveButton;
    public Text waveButtonText;

    private static int waveToSpawn = 0;
    private static int currentWave = 0;


    private void Update()
    {
        if (waveSpawned && !waveInProgress)
        {
            startWaveButton.interactable = true;
            waveButtonText.text = "Start Wave";
            waveSpawned = false;
            waveEnded = true;
        }
    }

    public void StartWave()
    {

        StartCoroutine(SpawnWave());
        startWaveButton.interactable = false;
        waveButtonText.text = "Wave In Progress";

    }

    IEnumerator SpawnWave()
    {
        waveInProgress = true;
        waveStarted = true;
        enemiesToKill = waves[currentWave].enemyCount;

        Wave wave = waves[waveToSpawn];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }

        waveToSpawn++;
        waveSpawned = true;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public static void UpdateWaveStatus()
    {
        enemiesToKill--;
        waveInProgress = enemiesToKill > 0;
        if (!waveInProgress)
        {
            currentWave++;
        }
    }

    public bool IsWaveInProgress(){
        return waveInProgress;
    }
}

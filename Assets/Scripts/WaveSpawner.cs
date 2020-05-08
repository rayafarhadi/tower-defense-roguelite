using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    private static int enemiesToKill = 0;
    private static bool waveInProgress = false;
    private static bool stopSpawning = false;

    public Wave[] waves;
    private static int lastWave;
    public Transform spawnPoint;

    public float waveInterval = 20f;
    private float countdown;
    public Text waveCountdownTimer;
    private static int waveToSpawn = 0;
    private static int currentWave = 0;

    private void Start()
    {
        countdown = waveInterval;
        lastWave = waves.Length - 1;
    }

    private void Update()
    {

        if (stopSpawning){
            this.enabled = false;
        }

        if (waveInProgress)
        {
            return;
        }

        if (countdown <= 0f)
        {
            PlayerStats.wavesSurvived++;
            StartCoroutine(SpawnWave());
            countdown = waveInterval;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveInProgress = true;
        enemiesToKill = waves[currentWave].enemyCount;

        Wave wave = waves[waveToSpawn];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }

        waveToSpawn++;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public static void UpdateWaveStatus(){
        enemiesToKill--;
        waveInProgress = enemiesToKill > 0;
        Debug.Log("Enemies left to kill: " + enemiesToKill);
        if(!waveInProgress){
            currentWave++;
            if (currentWave > lastWave)
            {
                stopSpawning = true;
            }
        }
    }
}

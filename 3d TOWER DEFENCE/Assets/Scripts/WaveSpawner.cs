using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform fastEnemyPrefab;
    public Transform slowEnemyPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] TextMeshProUGUI waveCountDownText;
    [SerializeField] TextMeshProUGUI waveNumberText;
    [SerializeField] GameObject skipCountdownPanel;

    private int[][] waves = new int[10][];
    public float timeBetweenWaves = 5f;
    float countdown = 5f;
    int displayWave = 0;
    int waveIndex = 0;
    public bool doCountdown = true;

    private void Start() {
        waves[0] = new int[]{2, 0, 0};
        waves[1] = new int[]{4, 0, 0};
        waves[2] = new int[]{5, 1, 0};
        waves[3] = new int[]{8, 0, 2};
        waves[4] = new int[]{5, 5, 1};
        waves[5] = new int[]{15, 0 , 0};
        waves[6] = new int[]{10, 5, 5};
        waves[7] = new int[]{10, 10, 5};
        waves[8] = new int[]{0 , 20, 0};
        waves[9] = new int[]{0, 0, 20};
        waves[10] = new int[]{15, 15, 15};
        waves[11] = new int[]{0, 30, 1};
        //Debug.Log(waves[2][1].ToString());
        skipCountdownPanel.SetActive(false);
    }
    private void Update()
    {
        if (doCountdown) {
            skipCountdownPanel.SetActive(true);
        }else{
            skipCountdownPanel.SetActive(false);
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        if (doCountdown) {
            countdown -= Time.deltaTime;
            waveCountDownText.text = Mathf.Floor(countdown + 1).ToString();                            
        }
        
        waveNumberText.text = "Wave: " + displayWave.ToString();
    }

    IEnumerator SpawnWave()
    {
        displayWave++;

        // for (int i = 0; i < waves[waveIndex][0] + waves[waveIndex][1] + waves[waveIndex][2]; i++)
        // {
        //     SpawnEnemy(enemyPrefab);
        //     yield return new WaitForSeconds(0.4f);
        // }
        if (waves[waveIndex] != null){
            for (int i = 0; i < waves[waveIndex][0]; i++) {
                SpawnEnemy(enemyPrefab);
                doCountdown = false;
                yield return new WaitForSeconds(0.4f);               
            }
            for (int i = 0; i < waves[waveIndex][1]; i++) {
                SpawnEnemy(fastEnemyPrefab);
                doCountdown = false;
                yield return new WaitForSeconds(0.3f);               
            }
            for (int i = 0; i < waves[waveIndex][2]; i++) {
                SpawnEnemy(slowEnemyPrefab);
                doCountdown = false;
                yield return new WaitForSeconds(2.5f);
            }
        } else{
            Debug.Log("You Win!");
        }
        

        doCountdown = true;
        waveIndex++;
    }

    private void SpawnEnemy(Transform enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
    public void skipCountdown() {
        countdown = 1f;
    }
}

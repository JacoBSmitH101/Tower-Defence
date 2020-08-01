using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class StrongEnemy : MonoBehaviour
{
       [Header("Balancing Options:")]
    [SerializeField] float speed = 100.5f;
    [SerializeField] float health = 20f;
    [SerializeField] float rewardForKill = 10f;

    private GameObject waveSpawnerGO;
    [SerializeField] WaveSpawner waveSpawner;
    float maxHealth;
    private Transform target;
    private int wavepointIndex = 0;
    [SerializeField] Slider healthBar;

    private void Start()
    {
        waveSpawnerGO = GameObject.Find("GameMaster");
        waveSpawner = waveSpawnerGO.GetComponent<WaveSpawner>();
        maxHealth = health;
        target = Waypoints.points[0];
    }

    private void Update()
    {
        //waveSpawner.doCountdown = false;
        Debug.Log(health);
        healthBar.value = health / maxHealth;
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.075f)
        {
            GetNextWaypoint();
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            GameMaster.baseHealth--;
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    private void OnDestroy()
    {
        //waveSpawner.doCountdown = true;
        ShopController.balance += rewardForKill;
    }
    public void takeDamage(float damage)
    {
        
        health -= damage;
        //print(healthStatic);
    }
}

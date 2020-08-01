﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class MissileLauncher : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform cam;
    public GameObject turretOptionsPanel;
    public GameObject turretOptionsPanelForInspector;

    
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Shooting Options")]

    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float range = 0.5f;

    [SerializeField] Transform partToRotate;
    [SerializeField] float missileLauncherPriceForInpector;
    [SerializeField] GameObject muzzleFlash;
    public static float missileLauncherPrice = 100f;

    public float upgradeMultiplier = 1;
    public float bulletDamage = 1;
    private float upgradeAmount = 1f;

    [Header("MISC")]

    [SerializeField] public TextMeshProUGUI upgradeText;
    [SerializeField] public bool isMenuOpen = false;
    [SerializeField] public float startDelay = 2f;
    [SerializeField] public TextMeshProUGUI upgradeAmountDisplay;

    private GameObject onTile;
    public MissileLauncher(GameObject tile)
    {
        onTile = tile;
    }

    // Start is called before the first frame update
    void Start()
    {
        upgradeAmount = 1;

        turretOptionsPanel = turretOptionsPanelForInspector;
        turretOptionsPanel.SetActive(false);

        cam = Camera.main.transform;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        missileLauncherPrice = missileLauncherPriceForInpector;

        this.GetComponentInChildren<Billboard>().cam = cam;

        turretOptionsPanel.SetActive(false);
    }

    void UpdateTarget()
    {
        //Debug.Log(target.transform);
        isMenuOpen = turretOptionsPanel.activeInHierarchy;

        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        Debug.Log(enemies);

        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy);
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
        //Debug.Log(target.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(upgradeAmount.ToString());
        upgradeAmountDisplay.text = upgradeAmount.ToString();

        String upgradePrice = Mathf.CeilToInt(100 * upgradeMultiplier).ToString();
        upgradeText.text = "Upgrade " + upgradePrice;

            //fireRate = 1 / upgradeMultiplier;
            if (target == null)
            {
                return;
            }
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        
    }

    private void Shoot()
    {
        
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(target, bulletDamage);
            }
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void sell()
    {
        Destroy(gameObject);
    }
    public void showMenu()
    {
        turretOptionsPanel.SetActive(true);
    }

    public void backOffMenu()
    {
        turretOptionsPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        isMenuOpen = false;
        ShopController.balance += (float)0.5 * missileLauncherPrice * upgradeMultiplier;
        
    }
    public bool menuIsOpen()
    {
        return turretOptionsPanel.activeInHierarchy;
    }
    public void upgrade()
    {

        Debug.Log(upgradeMultiplier);
        Debug.Log(upgradeMultiplier * 100);
        if (ShopController.balance < upgradeMultiplier * 100)
        {
            
            Debug.Log("CANT UPGRADE NOT ENOUG HMONEY");
            return;
        }
        else
        {
             upgradeAmount++;
            if (upgradeAmount % 5 == 0) {
                bulletDamage++;
            }else{
                fireRate++;    
            }
            
            ShopController.balance -= upgradeMultiplier * 100;
            upgradeMultiplier += upgradeMultiplier / 4;
        }
       
        
    }
}
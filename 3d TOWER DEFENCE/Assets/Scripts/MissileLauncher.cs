using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class MissileLauncher : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform cam;
    public GameObject turretOptionsPanelForInspector;
    [Header("Main Option:")]
    public static  float missileLauncherPrice;
    [SerializeField] public string enemyTag;
    [SerializeField] Transform[] firepoints;
    [SerializeField] TextMeshProUGUI upgradeAmountDisplay;
    [SerializeField] int upgradeAmount;

    [SerializeField] float upgradeMultiplier;
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] Transform partToRotate;
    [SerializeField] float turnSpeed = 2f;
    [SerializeField] float fireRate = 4;
    [SerializeField] GameObject missilePrefab;
    private float fireCountdown;
    public float range = 1f;
    private int currentFirePoint = 0;
    private float missileDamage = 4;
    private void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    private void Update() {
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
    public void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
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
    }
    public void Shoot(){
        GameObject missileGO = (GameObject)Instantiate(missilePrefab, new Vector3(firepoints[currentFirePoint].position.x, firepoints[currentFirePoint].position.y, firepoints[currentFirePoint].position.z), firepoints[currentFirePoint].rotation);
            //Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            Missile missile = missileGO.GetComponent<Missile>();

            if (missile != null)
            {
                missile.Seek(target, missileDamage);
            }
            getNextFirePoint();
    }
    public void getNextFirePoint(){
        if (currentFirePoint == firepoints.Length - 1) {
            currentFirePoint = 0;
        }else{
            currentFirePoint++;
        }
    }
}

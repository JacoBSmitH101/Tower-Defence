using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Transform cam;
    public GameObject turretOptionsPanel;
    public GameObject turretOptionsPanelForInspector;

    
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform[] firePoint;

    [Header("Shooting Options")]

    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float range = 0.5f;

    [SerializeField] Transform partToRotate;
    [SerializeField] float standardTurretPriceForInpector;
    [SerializeField] GameObject muzzleFlash;
    public static float standarTurretPrice = 100f;

    public float upgradeMultiplier = 1;
    public float bulletDamage = 1;
    private float upgradeAmount = 1f;

    [Header("MISC")]

    [SerializeField] public TextMeshProUGUI upgradeText;
    [SerializeField] public bool isMenuOpen = false;
    [SerializeField] public float startDelay = 2f;
    [SerializeField] public TextMeshProUGUI upgradeAmountDisplay;
    [SerializeField] Animator animator;
    private int currentFirePoint = 0;
    bool played = false;

    private GameObject onTile;

    public Turret(GameObject tile)
    {
        onTile = tile;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        upgradeAmount = 1;

        turretOptionsPanel = turretOptionsPanelForInspector;
        turretOptionsPanel.SetActive(false);

        cam = Camera.main.transform;

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        standarTurretPrice = standardTurretPriceForInpector;

        this.GetComponentInChildren<Billboard>().cam = cam;

        turretOptionsPanel.SetActive(false);
    }

    void UpdateTarget()
    {
        isMenuOpen = turretOptionsPanel.activeInHierarchy;

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

    // Update is called once per frame
    void Update()
    {
        Debug.Log(upgradeAmount.ToString());
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
            Transform nextFirepoint = getNextFirepoint(firePoint);
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, nextFirepoint.position, nextFirepoint.rotation);
            Instantiate(muzzleFlash, nextFirepoint.position, nextFirepoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(target, bulletDamage);
            }
        
        
    }
    public Transform getNextFirepoint(Transform[] firePoints) {
        if (currentFirePoint == firePoints.Length) {
            currentFirePoint = 0;
        }
        increaseFirePoint();
        return firePoints[currentFirePoint];
        
    }
    public void increaseFirePoint() {
        currentFirePoint++;
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
        animator.SetBool("isMenuOpen", true);
    }

    public void backOffMenu()
    {
        StartCoroutine("closeMenu");
    }

    private void OnDestroy()
    {
        isMenuOpen = false;
        ShopController.balance += (float)0.5 * standarTurretPrice * upgradeMultiplier;
        
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
    IEnumerator closeMenu() {
        
        if (!played){
            animator.SetBool("isMenuOpen", false);
            played = true;
            yield return new WaitForSeconds(0.3f);
        }
        turretOptionsPanel.SetActive(false);
        played = false;
        StopAllCoroutines();
    }
    
}

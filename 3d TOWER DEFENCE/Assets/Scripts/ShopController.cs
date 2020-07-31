using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    static int selectedTurret = 0;

    [Header("UI")]
    public TextMeshProUGUI balanceText;

    [Header("BALANCING")]
    public float Inputbalance = 50f;
    public static float balance = 50f;

    [Header("STANDARD TURRET OPTIONS")]
    public GameObject inputStandardTurretPrefab;
    public static GameObject standardTurretPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        standardTurretPrefab = inputStandardTurretPrefab;
        balance = Inputbalance;
    }

    // Update is called once per frame
    void Update()
    {
        balanceText.text = "£" + Mathf.Floor(balance).ToString();
        if (Input.GetKeyDown(KeyCode.M))
        {
            balance += 9999;
        }
    } 
    public void setStandardTurret()
    {
        selectedTurret = 1;
    }
    public void setMissileLauncher(){
        selectedTurret = 2;
    }
    public static void buildTurret(GameObject pos, float price, GameObject tileName)
    {
        
       
    }
    public static int getSelectedTurret()
    {
        return selectedTurret;
    }
}

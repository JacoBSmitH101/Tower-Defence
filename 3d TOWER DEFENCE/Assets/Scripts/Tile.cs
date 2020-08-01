using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    Color defaultColor;
    Renderer _renderer;


    [Header("Standard Turret Options")]
    [SerializeField] GameObject standardTurretPrefab;
    [SerializeField] GameObject missileLauncherPrefab;


    GameObject turret;
    GameObject missileLauncher;

    shopManager shop;

    public bool menuIsOpen = false;
    float selectedTurret;
    // Start is called before the first frame update
    void Start()
    {

        defaultColor = GetComponent<Renderer>().material.color;
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = Color.gray;
    }
    private void OnMouseExit()
    {
        _renderer.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        Debug.Log(turret);
        if (turret == null && !IsPointerOverUIObject())
        {
            
            Vector3 actuallPos = new Vector3(transform.position.x, transform.position.y + (GetComponent<Renderer>().bounds.size.y) / 2, transform.position.z);
            if (ShopController.getSelectedTurret() == 1 && ShopController.balance >= Turret.standarTurretPrice)
            {
                turret = Instantiate(standardTurretPrefab, actuallPos, transform.rotation);
                ShopController.balance -= Turret.standarTurretPrice;
                FindObjectOfType<SoundManager>().Play("Build");
                return;
            }
            if (ShopController.getSelectedTurret() == 2 && ShopController.balance >= MissileLauncher.missileLauncherPrice)
            {
                turret = Instantiate(missileLauncherPrefab, actuallPos, transform.rotation);
                ShopController.balance -= MissileLauncher.missileLauncherPrice;
                FindObjectOfType<SoundManager>().Play("Build");
                return;
            }
        }
        else
        {
           if (turret == null)
           {
                return;
            }
            else
            {
                if (turret.GetComponent<Turret>()){
                    if (!turret.GetComponent<Turret>().turretOptionsPanelForInspector.activeInHierarchy)
                    {
                        turret.GetComponent<Turret>().Invoke("showMenu", 0f);
                        menuIsOpen = true;
                    }
                    else
                    {
                        menuIsOpen = false;
                        turret.GetComponent<Turret>().Invoke("backOffMenu", 0f);
                    }
                }else if(turret.GetComponent<MissileLauncher>()){

                    if (!turret.GetComponent<MissileLauncher>().turretOptionsPanelForInspector.activeInHierarchy)
                    {
                        turret.GetComponent<MissileLauncher>().Invoke("showMenu", 0f);
                        menuIsOpen = true;
                    }
                    else
                    {
                        menuIsOpen = false;
                        turret.GetComponent<MissileLauncher>().Invoke("backOffMenu", 0f);
                    }
                }
                
            }
                    
           
                    
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }





}

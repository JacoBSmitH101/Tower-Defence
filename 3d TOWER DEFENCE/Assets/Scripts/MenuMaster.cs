using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    private int currentMenu = 0;

    [Header("Menu Tabs")]

    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        levelPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setLevelPanel()
    {
        levelPanel.SetActive(true);
        optionsPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void setOptionsPanel()
    {
        optionsPanel.SetActive(true);
        levelPanel.SetActive(false);
    }
    public void quit() {
        Application.Quit();
    }
}

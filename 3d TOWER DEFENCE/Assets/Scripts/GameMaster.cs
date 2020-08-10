using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{


    [Header("Base Health")]

    [SerializeField] float baseHealthForInspector;
    [SerializeField] float maxHealthForInspector;

    public static float baseHealth;  
    public static float maxHealth;

    private static bool _isPaused = false;

    public Slider slider;

    [Header("UI")]

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;
    void Start()
    {
        baseHealth = baseHealthForInspector;
        maxHealth = maxHealthForInspector;

        

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (baseHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            slider.value = baseHealth / maxHealth;
            baseHealthForInspector = baseHealth;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPaused = !_isPaused;
                if (_isPaused)
                {
                    Time.timeScale = 0f;
                }else if (!_isPaused)
                {
                    Time.timeScale = 1f;
                }

                
            }
            
        }
        if (Input.GetKey(KeyCode.Space)){
            Time.timeScale = 5;
        }else{
            Time.timeScale = 1;
        }
        pausePanel.SetActive(_isPaused);
        
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
        
    }
    public void unPause()
    {
        Time.timeScale = 1f;
        _isPaused = false;
    }
    public void toMenu()
    {
        SceneManager.LoadScene(0);
    }
    

}

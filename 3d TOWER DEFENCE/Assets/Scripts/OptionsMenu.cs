using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown  graphicsDropdown;
    // Start is called before the first frame update
    void Start()
    {
        graphicsDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("quality"));
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }
    public void setFullScreen(bool isfullScreen){
        Screen.fullScreen = isfullScreen;
    }
}

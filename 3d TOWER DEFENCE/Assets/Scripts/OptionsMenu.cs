using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown  graphicsDropdown;
    [SerializeField] Toggle playMusicToggle;
    [SerializeField] MenuMaster menuMaster;
    // Start is called before the first frame update
    void Start()
    {
        graphicsDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("quality"));
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
    }

    // Update is called once per frame
    void Update()
    {
        playMusicToggle.onValueChanged.AddListener(delegate {
                ToggleValueChanged(playMusicToggle);
            });
    }
    public void setGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }
    public void setFullScreen(bool isfullScreen){
        Screen.fullScreen = isfullScreen;
    }
    void ToggleValueChanged(Toggle change)
    {
        if(change.isOn){
            menuMaster.startMenuMusic();
        }
        if (!change.isOn) {
            menuMaster.stopMenuMusic();
        }
    }
}

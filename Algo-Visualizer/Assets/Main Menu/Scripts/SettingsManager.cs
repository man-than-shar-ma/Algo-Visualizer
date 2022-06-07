using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;

    public TMP_Dropdown resolutionDropdown;
    

    Resolution[] screenResolutions;


    void Start(){
        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(2000,0);
        
        screenResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currResolutionIndex = 0;
        for(int i=0; i<screenResolutions.Length; i++){
            string option = screenResolutions[i].width + " X " + screenResolutions[i].height;
            options.Add(option);

            if(screenResolutions[i].width == Screen.currentResolution.width && screenResolutions[i].height == Screen.currentResolution.height){
                currResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        int qualIndex = PlayerPrefs.GetInt("qualityIndex", 2);
        qualityDropdown.value = qualIndex;
        setQuality(qualIndex);

        float volSlider = PlayerPrefs.GetFloat("volumeSliderValue", 1);
        volumeSlider.value = volSlider;
        setVolume(volSlider);

        bool isFullScr = PlayerPrefs.GetInt("isFullScreen", 1) == 1 ? true : false;
        fullScreenToggle.isOn = isFullScr;
        setFullScreen(isFullScr);

        int resIndex = PlayerPrefs.GetInt("resolutionIndex", currResolutionIndex);
        resolutionDropdown.value = resIndex;
        setResolution(resIndex);

        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0,0);
        // Debug.Log("Hi");
        gameObject.SetActive(false);
    }
    
    public void setVolume(float sliderValue){
        audioMixer.SetFloat("volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("volumeSliderValue", sliderValue);
    }

    public void setQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }

    public void setFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("isFullScreen", isFullScreen ? 1 : 0);
    }

    public void setResolution(int resolutionIndex){
        Resolution resolution = screenResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start(){
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currResolutionIndex = 0;
        for(int i=0; i<resolutions.Length; i++){
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void setVolume(float sliderValue){
        audioMixer.SetFloat("volume", Mathf.Log10(sliderValue) * 20);
    }

    public void setQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    public void setResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

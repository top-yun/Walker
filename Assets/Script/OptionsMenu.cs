using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private AudioSource music;

    public Slider volume;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;
    /// <summary>
    /// Event fired when sliders, dropdown change
    /// </summary>
    void Start()
    {
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();


        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " (" + resolutions[i].refreshRate + "fps)";
            options.Add(option);

            
            if (resolutions[i].width == 1280 &&
                resolutions[i].height == 720)
            {
                currentResolutionIndex = i;
            }

            
        }

        resolutionDropdown.AddOptions(options);
        // resolutionDropdown.value = currentResolutionIndex;
        // resolutionDropdown.RefreshShownValue();


        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

        volume.value = music.volume;
    }

    void Update()
    {
        music.volume = volume.value;
    }

    public void VolumePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", music.volume);
    }

    void PlayMusic()
    {
        StartCoroutine("FadeSound");
    }

    IEnumerator FadeSound()
    {
        while (music.volume > 0.01f)
        {
            music.volume -= Time.deltaTime / 1.0f;
            yield return null;
        }
    }
    public void SetResolution(Dropdown dropdown)
    {
        Resolution resolution = resolutions[dropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(Toggle isFullscreen)
    {
        Screen.fullScreen = isFullscreen.isOn;
    }
}
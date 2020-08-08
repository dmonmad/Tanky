using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuManager : MonoBehaviour
{
    public Slider MusicVolumeSlider;
    public Slider FXVolumeSlider;
    public AudioMixer mixer;

    private void Start()
    {
        MusicVolumeSlider.value = GetMusicVolume();
        FXVolumeSlider.value = GetFXVolume();
    }

    public void UpdateMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicVolumeSlider.value);
        mixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolumeSlider.value) * 20);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 0.75f);        
    }

    public void UpdateFXVolume()
    {
        PlayerPrefs.SetFloat("FXVolume", FXVolumeSlider.value);
        mixer.SetFloat("FXVolume", Mathf.Log10(FXVolumeSlider.value) * 20);
    }

    public static float GetFXVolume()
    {
        return PlayerPrefs.GetFloat("FXVolume", 0.75f);
    }

}

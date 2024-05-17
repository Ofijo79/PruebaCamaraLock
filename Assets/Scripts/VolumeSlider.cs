using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    [SerializeField] string _volumeParameter;
    Slider _volumeSlider;

    Toggle _muteToggle;

    bool muted;

    void Awake() 
    {
        _volumeSlider = GetComponent<Slider>();
        _muteToggle = GetComponentInChildren<Toggle>();
        _muteToggle.onValueChanged.AddListener(Mute);
        _volumeSlider.onValueChanged.AddListener(Volume);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat(_volumeParameter, _volumeSlider.maxValue);
        string muteValue = PlayerPrefs.GetString(_volumeParameter + "Mute", "False");

        if(muteValue == "False")
        {
            muted = false;
        }

        else if(muteValue == "True")
        {
            muted = true;
        }

        _muteToggle.isOn = !muted;
    }

    void OnDisable() 
    {
        PlayerPrefs.SetFloat(_volumeParameter, _volumeSlider.value);
        PlayerPrefs.SetString(_volumeParameter + "Mute", muted.ToString());  
    }
    void Volume(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * 20);
    }

    void Mute(bool soundEnabled)
    {
        if(soundEnabled)
        {
            float lastVolume = PlayerPrefs.GetFloat(_volumeParameter, _volumeSlider.maxValue);
            _mixer.SetFloat(_volumeParameter, Mathf.Log10(lastVolume) * 20);
            muted = false;
        }
        else
        {
            PlayerPrefs.SetFloat(_volumeParameter, _volumeSlider.value);
            _mixer.SetFloat(_volumeParameter, Mathf.Log10(_volumeSlider.minValue) * 20);
            muted = true;
        }
    }
    
}
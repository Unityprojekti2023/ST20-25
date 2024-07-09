using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class OptionsMenu : MonoBehaviour
{
    [Header("References to UI Elements")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    
    public TextMeshProUGUI sensitivityValueText;
    public TextMeshProUGUI volumeValueText;

    public AudioSource audioSource;

    [Header("Mouse sensitivity default value")]
    public float mouseSensitivity = 2.0f;
    private float volume = 10.0f;


    // Define a key for PlayerPrefs
    private readonly string sensitivityKey = "MouseSensitivity";
    private readonly string volumeKey = "Volume";
    void Start()
    {
        // Load sensitivity value from PlayerPrefs, use default if not found
        mouseSensitivity = PlayerPrefs.GetFloat(sensitivityKey, 8.0f);
        volume = PlayerPrefs.GetFloat(volumeKey, 10.0f);


        sensitivitySlider.value = mouseSensitivity;
        volumeSlider.value = volume;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeValueChanged);
        sensitivityValueText.text = mouseSensitivity.ToString("F1");
        volumeValueText.text = volume.ToString();
    }

    private void OnVolumeValueChanged(float value)
    {
        audioSource.volume = value / 100;
        volume = value;
        volumeValueText.text = volume.ToString();
        PlayerPrefs.SetFloat(volumeKey, volume);
        PlayerPrefs.Save();
    }

    void OnSensitivityValueChanged(float value)
    {
        mouseSensitivity = value;
        sensitivityValueText.text = mouseSensitivity.ToString("F1");

        // Save sensitivity value to PlayerPrefs
        PlayerPrefs.SetFloat(sensitivityKey, mouseSensitivity);
        PlayerPrefs.Save();
    }
    

    void UpdateSensitivityText(TextMeshProUGUI textComponent)
    {
        textComponent.text = mouseSensitivity.ToString("F1"); // Display one decimal place
    }
}
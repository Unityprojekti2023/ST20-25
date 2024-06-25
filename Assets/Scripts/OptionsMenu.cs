using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("References to UI Elements")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;

    [Header("Mouse sensitivity default value")]
    public float mouseSensitivity = 2.0f;


    // Define a key for PlayerPrefs
    private string sensitivityKey = "MouseSensitivity";
    void Start()
    {
        // Load sensitivity value from PlayerPrefs, use default if not found
        mouseSensitivity = PlayerPrefs.GetFloat(sensitivityKey, 8.0f);

        sensitivitySlider.value = mouseSensitivity;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChanged);
        UpdateSensitivityText();
    }

    void OnSensitivityValueChanged(float value)
    {
        mouseSensitivity = value;
        UpdateSensitivityText();

        // Save sensitivity value to PlayerPrefs
        PlayerPrefs.SetFloat(sensitivityKey, mouseSensitivity);
        PlayerPrefs.Save();
    }

    void UpdateSensitivityText()
    {
        sensitivityValueText.text = mouseSensitivity.ToString("F1"); // Display one decimal place
    }
}
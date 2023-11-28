using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("References to UI Elements")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;

    [Header("Mouse sensitivity default value")]
    private float sensitivityValue = 2.0f;


    // Define a key for PlayerPrefs
    private string sensitivityKey = "MouseSensitivity";
    void Start()
    {
        // Load sensitivity value from PlayerPrefs, use default if not found
        sensitivityValue = PlayerPrefs.GetFloat(sensitivityKey, 2.0f);

        sensitivitySlider.value = sensitivityValue;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChanged);
        UpdateSensitivityText();
    }

    void OnSensitivityValueChanged(float value)
    {
        sensitivityValue = value;
        UpdateSensitivityText();

        // Save sensitivity value to PlayerPrefs
        PlayerPrefs.SetFloat(sensitivityKey, sensitivityValue);
        PlayerPrefs.Save();
    }

    public float GetSensitivity()
    {
        return sensitivityValue;
    }

    void UpdateSensitivityText()
    {
        sensitivityValueText.text = sensitivityValue.ToString("F1"); // Display one decimal place
    }
}
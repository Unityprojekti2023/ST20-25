using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;

    private float sensitivityValue = 2.0f;

    void Start()
    {
        sensitivitySlider.value = sensitivityValue;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityValueChanged);
        UpdateSensitivityText();
    }

    void OnSensitivityValueChanged(float value)
    {
        sensitivityValue = value;
        UpdateSensitivityText();
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
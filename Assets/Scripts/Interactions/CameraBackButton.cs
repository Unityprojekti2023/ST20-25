using UnityEngine;
using UnityEngine.UI;

public class CameraBackButton : MonoBehaviour
{
    private void Start()
    {
        Button backButton = GetComponent<Button>();
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnBackButtonClick()
    {
        if (CameraController.Instance != null)
        {
            CameraController.Instance.MainCameraActive();
        }
    }
}
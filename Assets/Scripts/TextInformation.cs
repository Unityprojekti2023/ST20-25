using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextInformation : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    private Coroutine hideCoroutine;
    public float logDuration = 2.5f;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string message)
    {
        if (textComponent != null)
        {
            //Stop previous coroutine if it's running.
            if(hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }

            textComponent.text = message;
            hideCoroutine = StartCoroutine(HideAfterDelay()); //Start coroutine
        }
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(logDuration);
        textComponent.text = ""; // Empty the text to hide it
    }
}

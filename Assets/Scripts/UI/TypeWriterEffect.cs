using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    private TMP_Text textComponent;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        fullText = textComponent.text;
        textComponent.text = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

}


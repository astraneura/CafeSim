using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public static DialogueManager instance;

    private Story currentStory;
    private ICustomer activeCustomer;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple instances of DialogueManager detected. Destroying duplicate.");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public IEnumerator DialogueBoxTimeout(float delay, ICustomer customer)
    {
        yield return new WaitForSeconds(delay);

        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        if (customer != null)
        {
            customer.StopSpeaking();
        }
    }

    public void StartDialogue(TextAsset inkJSON, ICustomer customer, System.Action<Story> setupVars)
    {
        currentStory = new Story(inkJSON.text);
        activeCustomer = customer;

        setupVars?.Invoke(currentStory);

        dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ContinueStory()
    {
        if(currentStory == null) return;

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue().Trim();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);

        activeCustomer?.StopSpeaking();
        activeCustomer = null;
        currentStory = null;
    }

}

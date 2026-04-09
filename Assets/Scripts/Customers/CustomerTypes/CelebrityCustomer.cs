using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class CelebrityCustomer : MonoBehaviour, ICustomer
{
//manager references
    private GameManager gameManager;
    private DrinkManager drinkManager;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> enterClips;
    [SerializeField] private List<AudioClip> completeClips;
    [SerializeField] private AudioClip speakingClip;

    //database references
    public CustomerNameDatabase nameDatabase; // Reference to the name database
    public DrinkRecipeDatabase drinkRecipeDatabase; // Reference to the drink recipe database

    public string CustomerName => customerName;
    public string customerName;

    public float orderTimeLimit = 20f;
    protected float orderTimer;
    protected bool orderInProgress = false;

    [SerializeField] private List<TextAsset> inkDialogues;
    //[SerializeField] private TextAsset dialogueInk;

    public DrinkRecipe currentRecipe;
    public List<OrderStep> currentOrder = new List<OrderStep>();

    // UI Elements
    public Slider patienceSlider;

    void Awake()
    {
        customerName = GetCustomerName();
        patienceSlider.gameObject.SetActive(false);
    }

    void Start()
    {
        PlayEnterSound();
    }

    void Update()
    {
        UpdateOrderTimer();
    }
    public string GetCustomerName()
    {
        if (nameDatabase != null && nameDatabase.names.Count > 0)
        {
            int randomIndex = Random.Range(0, nameDatabase.names.Count);
            return nameDatabase.names[randomIndex];
        }
        return "Customer"; // Fallback name
    }

    void PlayEnterSound()
    {
        if (audioSource != null && enterClips.Count > 0)
        {
            int randomIndex = Random.Range(0, enterClips.Count);
            audioSource.clip = enterClips[randomIndex];
            audioSource.Play();
        }
    }

    void PlayCompleteSound()
    {
        if (audioSource != null && completeClips.Count > 0)
        {
            int randomIndex = Random.Range(0, completeClips.Count);
            audioSource.clip = completeClips[randomIndex];
            audioSource.Play();
        }
    }

    public bool GenerateOrder()
    {
        if (orderInProgress)
            return false; // Prevent generating a new order if one is already in progress
        GameManager.Instance.EnableRegularMachines();
        if (drinkRecipeDatabase == null || drinkRecipeDatabase.allRecipes.Count == 0)
            return false;

        currentRecipe = drinkRecipeDatabase.allRecipes[Random.Range(0, drinkRecipeDatabase.allRecipes.Count)];
        currentOrder.Clear();
        foreach (string step in currentRecipe.steps)
        {
            currentOrder.Add(new OrderStep { stepName = step });
        }
        orderTimer = orderTimeLimit; // Reset the timer
        orderInProgress = true;

        OrderManager.Instance.SetCurrentOrder(this, currentRecipe);

        if (patienceSlider != null)
        {
            patienceSlider.gameObject.SetActive(true);
            patienceSlider.value = 1f;
        }

        Debug.Log("Current order: " + string.Join(", ", currentRecipe.steps));
        return true;
    }

    public void UpdateOrderTimer()
    {
        if (!orderInProgress)
            return;

        orderTimer -= Time.deltaTime;

        if (patienceSlider != null)
        {
            patienceSlider.value = orderTimer / orderTimeLimit;
        }

        if (orderTimer <= 0f)
        {
            orderInProgress = false;
            Debug.Log(customerName + "'s order has timed out!");
            OnOrderTimeout();
        }
    }

    public void OnOrderTimeout()
    {
        orderInProgress = false;
        Debug.Log($"{customerName} ran out of patience and left!");
        OrderManager.Instance.ClearCurrentOrder();
        OrderManager.Instance.totalOrdersFailed++;
        OrderManager.Instance.dataController.GetComponent<UserProfileData>().ordersFailed
        = OrderManager.Instance.totalOrdersFailed;
        Destroy(gameObject);
    }

    public void ResetOrderProgress()
    {
        foreach (OrderStep step in currentOrder)
        {
            step.isCompleted = false;
        }
        Debug.Log($"{customerName}'s order progress has been reset.");
    }

    public void CompleteOrder()
    {
        if (currentRecipe != null && orderInProgress)
        {
            orderInProgress = false;
            OrderManager.Instance.ClearCurrentOrder();
            Debug.Log("Adding money: " + currentRecipe.cost);
            FindAnyObjectByType<PlayerInteraction>().AddMoney(currentRecipe.cost);
            PlayCompleteSound();
            Destroy(gameObject, 0.3f);
        }
    }

    public void Speak()
    {
        audioSource.clip = speakingClip;
        audioSource.volume = 0.25f;
        audioSource.loop = true;
        audioSource.Play();

        TextAsset dialogueInk = inkDialogues[Random.Range(0, inkDialogues.Count)];

        DialogueManager.instance.StartDialogue(
            dialogueInk, this, story =>
            {
                story.variablesState["customerName"] = customerName;
                story.variablesState["drinkName"]  = currentRecipe.drinkName;
            }
        );
    }

    public void StopSpeaking()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.volume = 1f;
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    public void CloseDialogue()
    {
        DialogueManager.GetInstance().dialogueText.text = "";
        DialogueManager.GetInstance().dialoguePanel.SetActive(false);
    }
}

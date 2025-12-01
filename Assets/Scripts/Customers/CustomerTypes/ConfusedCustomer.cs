using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class ConfusedCustomer : MonoBehaviour, ICustomer
{
    //database reference
    [SerializeField] private CustomerNameDatabase nameDatabase;
    public string CustomerName => customerName;
    public string customerName;

    // variables for random order qualities generation
    private Qualities chosenQualities;
    private string chosenEmotionalQuality;
    private string chosenPhysicalQuality;
    private int desiredEmotionalQualityValue;
    private int desiredPhysicalQualityValue;
    private int emotionalBalance;
    private int physicalBalance;

    private PlayerInteraction pInteract;

    private TextMeshProUGUI orderText;


    void Awake()
    {
        customerName = GetCustomerName();
        pInteract = FindAnyObjectByType<PlayerInteraction>();
        orderText = GameObject.Find("ValueOrder").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!pInteract.canGenerateOrder)
        {
            CheckOrderCompletion();
        }
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

    public bool GenerateOrder()
    {
        GameManager.Instance.EnableQualityMachines();
        OrderManager.Instance.SetConfusedCustomer(this);
        chosenQualities = new Qualities();

        desiredEmotionalQualityValue = Random.Range(-5, 5);
        desiredPhysicalQualityValue = Random.Range(-5, 5);
        if (desiredEmotionalQualityValue == 0)
            desiredEmotionalQualityValue = 1;
        if (desiredPhysicalQualityValue == 0)
            desiredPhysicalQualityValue = 1;

        if (desiredEmotionalQualityValue < 0)
        {
            chosenEmotionalQuality = chosenQualities.negativeEmotionalQualities[
                Random.Range(0, chosenQualities.negativeEmotionalQualities.Count)];
        }
        else
        {
            chosenEmotionalQuality = chosenQualities.positiveEmotionalQualities[
                Random.Range(0, chosenQualities.positiveEmotionalQualities.Count)];
        }

        if (desiredPhysicalQualityValue < 0)
        {
            chosenPhysicalQuality = chosenQualities.negativePhysicalQualities[
                Random.Range(0, chosenQualities.negativePhysicalQualities.Count)];
        }
        else
        {
            chosenPhysicalQuality = chosenQualities.positivePhysicalQualities[
                Random.Range(0, chosenQualities.positivePhysicalQualities.Count)];
        }

        Debug.Log($"{customerName} wants a drink with {chosenEmotionalQuality} value of {desiredEmotionalQualityValue} and {chosenPhysicalQuality} value of {desiredPhysicalQualityValue}.");
        orderText.text = $"{chosenEmotionalQuality}: {desiredEmotionalQualityValue}\n{chosenPhysicalQuality}: {desiredPhysicalQualityValue}";
        // add here to pause the day timer in the GameManager

        switch (chosenEmotionalQuality)
        {
            case "Energizing":
            case "Calming":
                emotionalBalance = DrinkManager.Instance.energizedCalmingBalance;
                break;
            case "Light":
            case "Heavy":
                emotionalBalance = DrinkManager.Instance.lightHeavyBalance;
                break;
            case "Fresh":
            case "Nostalgic":
                emotionalBalance = DrinkManager.Instance.freshNostalgicBalance;
                break;
            case "Uplifting":
            case "Depressing":
                emotionalBalance = DrinkManager.Instance.upliftingDepressingBalance;
                break;
            case "Warm":
            case "Cold":
                emotionalBalance = DrinkManager.Instance.warmColdBalance;
                break;
            default:
                emotionalBalance = 0;
                break;
        }

        switch (chosenPhysicalQuality)
        {
            case "Creamy":
            case "Thin":
                physicalBalance = DrinkManager.Instance.creamyThinBalance;
                break;
            case "Sweet":
            case "Bitter":
                physicalBalance = DrinkManager.Instance.sweetBitterBalance;
                break;
            case "Spicy":
            case "Bland":
                physicalBalance = DrinkManager.Instance.spicyBlandBalance;
                break;
            case "Blessed":
            case "Cursed":
                physicalBalance = DrinkManager.Instance.blessedCursedBalance;
                break;
            default:
                physicalBalance = 0;
                break;
        }

        return true;
    }

    public void CheckOrderCompletion()
    {
        if (emotionalBalance == desiredEmotionalQualityValue && physicalBalance == desiredPhysicalQualityValue)
        {
            OrderManager.Instance.orderCompleted = true;
            Debug.Log($"{customerName}'s order is complete!");
        }

        int currentEmotional = DrinkManager.Instance.GetEmotionalBalanceForQuality(chosenEmotionalQuality);
        int currentPhysical = DrinkManager.Instance.GetPhysicalBalanceForQuality(chosenPhysicalQuality);

        if (currentEmotional == desiredEmotionalQualityValue && currentPhysical == desiredPhysicalQualityValue)
        {
            if (OrderManager.Instance.currentCustomer == (ICustomer)this)
            {
                OrderManager.Instance.orderCompleted = true;
                Debug.Log($"{customerName}'s order is complete!");
            }
            else
            {
                Debug.Log($"{customerName}'s order is ready, but they are not the current customer.");
            }
        }
    }

    public void CompleteOrder()
    {
        OrderManager.Instance.ClearCurrentOrder();
        orderText.text = "";
        Debug.Log("Adding money: $20");
        FindAnyObjectByType<PlayerInteraction>().AddMoney(20f);
        Destroy(gameObject, 2f);
    }

    public void Speak()
    {
        DialogueManager.GetInstance().dialoguePanel.SetActive(true);
        DialogueManager.GetInstance().dialogueText.text = $"Hello, I am {customerName}. I would like a drink with {chosenEmotionalQuality} value of {desiredEmotionalQualityValue} and {chosenPhysicalQuality} value of {desiredPhysicalQualityValue}.";
        DialogueManager.GetInstance().StartCoroutine(DialogueManager.GetInstance().DialogueBoxTimeout(5f));
    }

    public void CloseDialogue()
    {
        DialogueManager.GetInstance().dialogueText.text = "";
        DialogueManager.GetInstance().dialoguePanel.SetActive(false);
    }


    //Reset Order Progress, Update Order Timer, and On Order Timeout will be unused for this customer type
    public void ResetOrderProgress()
    {

    }

    public void UpdateOrderTimer()
    {
        return;
    }
    public void OnOrderTimeout()
    {
        return;
    }


}

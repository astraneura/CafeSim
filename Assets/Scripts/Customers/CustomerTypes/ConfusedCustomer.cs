using UnityEngine;
using UnityEngine.InputSystem.Interactions;

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
    void Start()
    {
        customerName = GetCustomerName();
        pInteract = FindAnyObjectByType<PlayerInteraction>();
    }

    void Update()
    {
        if (!pInteract.canGenerateOrder)
        {
            switch (chosenEmotionalQuality)
            {
                case "Energizing":
                    emotionalBalance = DrinkManager.Instance.energizedCalmingBalance;
                    break;
                case "Calming":
                    emotionalBalance = DrinkManager.Instance.energizedCalmingBalance;
                    break;
                case "Light":
                    emotionalBalance = DrinkManager.Instance.lightHeavyBalance;
                    break;
                case "Heavy":
                    emotionalBalance = DrinkManager.Instance.lightHeavyBalance;
                    break;
                case "Fresh":
                    emotionalBalance = DrinkManager.Instance.freshNostalgicBalance;
                    break;
                case "Nostalgic":
                    emotionalBalance = DrinkManager.Instance.freshNostalgicBalance;
                    break;
                case "Uplifting":
                    emotionalBalance = DrinkManager.Instance.upliftingDepressingBalance;
                    break;
                case "Depressing":
                    emotionalBalance = DrinkManager.Instance.upliftingDepressingBalance;
                    break;
                case "Warm":
                    emotionalBalance = DrinkManager.Instance.warmColdBalance;
                    break;
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
                    physicalBalance = DrinkManager.Instance.creamyThinBalance;
                    break;
                case "Thin":
                    physicalBalance = DrinkManager.Instance.creamyThinBalance;
                    break;
                case "Sweet":
                    physicalBalance = DrinkManager.Instance.sweetBitterBalance;
                    break;
                case "Bitter":
                    physicalBalance = DrinkManager.Instance.sweetBitterBalance;
                    break;
                case "Spicy":
                    physicalBalance = DrinkManager.Instance.spicyBlandBalance;
                    break;
                case "Bland":
                    physicalBalance = DrinkManager.Instance.spicyBlandBalance;
                    break;
                case "Blessed":
                    physicalBalance = DrinkManager.Instance.blessedCursedBalance;
                    break;
                case "Cursed":
                    physicalBalance = DrinkManager.Instance.blessedCursedBalance;
                    break;
                default:
                    physicalBalance = 0;
                    break;
            }
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
        chosenQualities = new Qualities();

        // Select random emotional quality
        chosenEmotionalQuality = chosenQualities.emotionalQualities[Random.Range(0, chosenQualities.emotionalQualities.Count)];
        // Select random physical quality
        chosenPhysicalQuality = chosenQualities.physicalQualities[Random.Range(0, chosenQualities.physicalQualities.Count)];

        // Generate desired quality values
        desiredEmotionalQualityValue = Random.Range(1, 11);
        desiredPhysicalQualityValue = Random.Range(1, 11);
        Debug.Log($"{customerName} wants a drink with {chosenEmotionalQuality} value of {desiredEmotionalQualityValue} and {chosenPhysicalQuality} value of {desiredPhysicalQualityValue}.");
        // add here to pause the day timer in the GameManager

        return true;
    }

    public void CheckOrderCompletion()
    {
        if (emotionalBalance == desiredEmotionalQualityValue && physicalBalance == desiredPhysicalQualityValue)
        {
            OrderManager.Instance.orderCompleted = true;
            Debug.Log($"{customerName}'s order is complete!");
        }
    }

    public void CompleteOrder()
    {
        OrderManager.Instance.ClearCurrentOrder();
        Debug.Log("Adding money: $20");
        FindAnyObjectByType<PlayerInteraction>().AddMoney(20f);
        Destroy(gameObject, 2f);
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

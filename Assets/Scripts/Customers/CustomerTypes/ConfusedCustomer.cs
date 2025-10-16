using UnityEngine;

public class ConfusedCustomer : MonoBehaviour, ICustomer
{
    //database reference
    private CustomerNameDatabase nameDatabase;
    public string CustomerName => customerName;
    public string customerName;

    // variables for random order qualities generation
    private Qualities chosenQualities;
    private string chosenEmotionalQuality;
    private string chosenPhysicalQuality;
    private int desiredEmotionalQualityValue;
    private int desiredPhysicalQualityValue;
    void Start()
    {
        customerName = GetCustomerName();
    }

    void Update()
    {

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

    }
}

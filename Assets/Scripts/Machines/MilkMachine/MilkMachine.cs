using UnityEngine;
using UnityEngine.UI;

public class MilkMachine : MonoBehaviour, IOrderStepSourceInterface
{
    private DrinkManager drinkManager;
    public float workDuration = 5f;
    private float workTimer;
    private bool isWorking = false;

    private Customer currentCustomer;
    private Slider progressBar;

    [SerializeField] private Ingredient milk;

    void Start()
    {
        progressBar = GetComponentInChildren<Slider>();
        progressBar.gameObject.SetActive(false);
        drinkManager = FindAnyObjectByType<DrinkManager>();
    }


    void Update()
    {
        if (isWorking)
        {
            workTimer -= Time.deltaTime;
            if (progressBar != null)
            {
                progressBar.value = 1f - (workTimer / workDuration);
            }
            if (workTimer <= 0f)
            {
                CompleteWork();
            }
        }
    }

    public void Interact(Customer customer)
    {
        if (!isWorking && customer != null)
        {
            currentCustomer = customer;
            StartWork();
            Debug.Log("MilkMachine: Started working for customer " + customer.name);
        }
    }

    private void StartWork()
    {
        isWorking = true;
        workTimer = workDuration;
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value = 0f; // Reset progress bar
        }
    }

    private void CompleteWork()
    {
        isWorking = false;
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(false);
        }
        if (currentCustomer != null)
        {
            OrderManager.Instance.AttemptStep("Add Milk");
            drinkManager.CalculateEmotionalValue(milk);
            Debug.Log($"MilkMachine: Completed work for customer {currentCustomer.name}");
            currentCustomer = null; //reset the current customer
        }
    }

    public string GetOrderStepName()
    {
        return "Add Milk";
    }
    
    public Ingredient GetIngredient()
    {
        return milk;
    }
}

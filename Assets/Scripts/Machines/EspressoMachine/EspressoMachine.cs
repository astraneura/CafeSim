using UnityEngine;
using UnityEngine.UI;

public class EspressoMachine : MonoBehaviour, IOrderStepSourceInterface
{
    private DrinkManager drinkManager;
    public float workDuration = 5f;
    private float workTimer;
    private bool isWorking = false;

    private Customer currentCustomer;
    private Slider progressBar;

    public Ingredient espresso;

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
            Debug.Log("espressoMachine: Started working for customer " + customer.name);
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
            OrderManager.Instance.AttemptStep("Add Espresso");
            Debug.Log($"espressoMachine: Completed work for customer {currentCustomer.name}");
            currentCustomer = null; //reset the current customer
        }
    }

    public string GetOrderStepName()
    {
        return "Add Espresso";
    }
    
    public Ingredient GetIngredient()
    {
        return espresso;
    }
}

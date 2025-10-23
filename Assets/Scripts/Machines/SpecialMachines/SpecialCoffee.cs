using UnityEngine;
using UnityEngine.UI;

public class SpecialCoffee : MonoBehaviour, IOrderStepSourceInterface
{
    [SerializeField] private Ingredient coffee;
    public float workDuration = 5f;
    private float workTimer;
    private bool isWorking = false;
    private ICustomer currentCustomer;
    private Slider progressBar;

    void Start()
    {
        progressBar = GetComponentInChildren<Slider>();
        progressBar.gameObject.SetActive(false);
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

    public void Interact(ICustomer customer)
    {
        if (!isWorking && customer != null)
        {
            currentCustomer = customer;
            StartWork();
            Debug.Log("CoffeeMachine: Started working");
        }
    }
    
    private void StartWork()
    {
        workTimer = workDuration;
        isWorking = true;
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value = 0f; // Reset progress bar
        }
    }

    public Ingredient GetIngredient()
    {
        return coffee;
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
            DrinkManager.Instance.CalculateEmotionalValue(coffee);
            DrinkManager.Instance.CalculatePhysicalValue(coffee);
        }
    }
    
    //not used on special machines
    public string GetOrderStepName()
    {
        return null;
    }
}

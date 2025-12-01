using UnityEngine;
using UnityEngine.UI;

public class WaterMachine : MonoBehaviour, IOrderStepSourceInterface
{
    public float workDuration = 5f;
    private float workTimer;
    private bool isWorking = false;

    private ICustomer currentCustomer;
    private Slider progressBar;

    public Ingredient water;

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
            Debug.Log("WaterMachine: Started working");
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
            string stepName = GetOrderStepName();
            OrderManager.Instance.AttemptStep(stepName);
            Debug.Log($"WaterMachine: Completed work");
            currentCustomer = null; //reset the current customer
        }
    }

    public string GetOrderStepName()
    {
        return "Add Water";
    }
    
    public Ingredient GetIngredient()
    {
        return water;
    }
}

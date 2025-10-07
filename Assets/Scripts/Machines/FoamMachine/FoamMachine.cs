using UnityEngine;
using UnityEngine.UI;

public class FoamMachine : MonoBehaviour, IOrderStepSourceInterface
{
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
            Debug.Log("FoamMachine: Started working");
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
            Debug.Log($"FoamMachine: Completed work");
            currentCustomer = null; //reset the current customer
        }
    }

    public string GetOrderStepName()
    {
        return "Add Foam";
    }
    
    public Ingredient GetIngredient()
    {
        return null;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CoffeeMachine : MonoBehaviour, IOrderStepSourceInterface
{
    public float workDuration = 5f;
    private float workTimer;
     private bool isWorking = false;

     private Customer currentCustomer;
    private Slider progressBar;

    void Start()
    {
        progressBar = GetComponentInChildren<Slider>();
        progressBar.gameObject.SetActive(false);
    }


    void Update()
    {
        if (isWorking){
            workTimer -= Time.deltaTime;
            if(progressBar != null){
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
            Debug.Log("CoffeeMachine: Started working for customer " + customer.name);
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

    private void CompleteWork()
    {
        isWorking = false;
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(false);
        }
        if (currentCustomer != null)
        {
            currentCustomer.TryCompleteStep("Add Coffee");
            Debug.Log($"CoffeeMachine: Completed work for customer {currentCustomer.name}");
            currentCustomer = null; //reset the current customer
        }
    }
    
    public string GetOrderStepName()
    {
        return "Add Coffee";
    }
}

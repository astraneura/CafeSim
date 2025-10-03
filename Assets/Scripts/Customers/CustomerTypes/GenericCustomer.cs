using UnityEngine;
using UnityEngine.UI;
public class GenericCustomer : MonoBehaviour
{
    //reference to the base customer
    private BaseCustomer baseCustomer;

    void Awake()
    {
        baseCustomer = new BaseCustomer();
        baseCustomer.customerName = baseCustomer.GetCustomerName();
        baseCustomer.patienceSlider = GameObject.Find("PatienceSlider").GetComponent<Slider>();
        baseCustomer.patienceSlider.gameObject.SetActive(false);
        baseCustomer.gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        baseCustomer.UpdateOrderTimer();
    }

    public void CreateOrder()
    {
        baseCustomer.GenerateOrder();
    }

}

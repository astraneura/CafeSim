using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    public string stepName; //unique for each machine
    public float workDuration = 5f; // how long the step takes to complete


    private float workTimer;
    private bool isWorking;
    private Customer currentCustomer;
    private Slider progressBar;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

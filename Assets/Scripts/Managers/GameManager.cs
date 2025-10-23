using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    //variables for spawning customers
    public GameObject customerPrefab;
    public GameObject confusedCustomerPrefab;
    public Transform[] spawnPoints;
    private int nextSpawnIndex = 0;
    private bool isSpawning = false;
    private bool spawnAllowed = true;
    public int currentCustomers = 0;
    [SerializeField] private float spawnDelay;

    // variables for tracking the day timer
    private TextMeshProUGUI dayTimerText;
    public float dayDuration = 300f; // Duration of the day in seconds
    private float dayTimer = 0f;

    //variables for managing the machines
    [SerializeField] private List<GameObject> regularMachines;
    [SerializeField] private List<GameObject> specialMachines;

    private void Awake()
    {
        dayTimerText = GameObject.Find("DayTimer").GetComponent<TextMeshProUGUI>();
        dayTimer = dayDuration;

        foreach (var machine in specialMachines)
        {
            machine.SetActive(false);
        }
        foreach (var machine in regularMachines)
        {
            machine.SetActive(true);
        }

        StartCoroutine(SpawnNewCustomerAfterDelay(1f));
    }

    void Update()
    {
        UpdateDayTimer();
        if (currentCustomers == spawnPoints.Length)
        {
            spawnAllowed = false;
            StartCoroutine(AllowCatchUp());
        }
    }

    private IEnumerator SpawnNewCustomerAfterDelay(float delay)
    {
        isSpawning = true;
        yield return new WaitForSeconds(delay);

        SpawnNewCustomer();
        isSpawning = false;
    }
    private void SpawnNewCustomer()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }
        if (!spawnAllowed)
        {
            Debug.Log("Spawning is currently not allowed.");
            return;
        }
        int randomNumber = Random.Range(0, 100);
        Debug.Log(randomNumber);
        if (randomNumber < 101) // 20% chance to spawn a confused customer
        {
            SpawnConfusedCustomer();
            foreach (var machine in regularMachines)
            {
                machine.SetActive(false);
            }
            foreach (var machine in specialMachines)
            {
                machine.SetActive(true);
            }
        }
        else
        {
            SpawnRegularCustomer();
            foreach (var machine in regularMachines)
            {
                machine.SetActive(true);
            }
            foreach (var machine in specialMachines)
            {
                machine.SetActive(false);
            }
        }
    }

    public void OnCustomerOrderStarted()
    {
        if (!isSpawning)
            StartCoroutine(SpawnNewCustomerAfterDelay(spawnDelay));
    }

    public void OnCustomerOrderCompleted()
    {
        currentCustomers = Mathf.Max(0, currentCustomers - 1);
        if (currentCustomers < spawnPoints.Length)
        {
            spawnAllowed = true;
            SpawnNewCustomer();
        }
    }

    private IEnumerator AllowCatchUp()
    {
        yield return new WaitForSeconds(15f);
        if (currentCustomers < spawnPoints.Length)
        {
            spawnAllowed = true;
            SpawnNewCustomer();
        }
    }

    private void SpawnConfusedCustomer()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }
        Transform spawnPoint = spawnPoints[nextSpawnIndex];
        Instantiate(confusedCustomerPrefab, spawnPoint.position, spawnPoint.rotation);
        currentCustomers++;
        // Cycle to the next spawn point
        nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;
        SpawnNewCustomerAfterDelay(spawnDelay); // remove later - allow more customers for testing 
        //spawnAllowed = false; // set spawning to false to allow player time to create custom drink
    }

    private void SpawnRegularCustomer()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }
        Transform spawnPoint = spawnPoints[nextSpawnIndex];
        Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        currentCustomers++;
        // Cycle to the next spawn point
        nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;
        SpawnNewCustomerAfterDelay(spawnDelay); // continue spawning regular customers
    }

    private void UpdateDayTimer()
    {
        if (dayTimer > 0)
        {
            dayTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(dayTimer / 60f);
            int seconds = Mathf.FloorToInt(dayTimer % 60f);
            dayTimerText.text = $"{minutes:00}:{seconds:00}";
        }
        else
        {
            dayTimer = 0;
            dayTimerText.text = "00:00";
            EndDay();
        }
    }

    private void EndDay()
    {
        SceneManager.LoadScene("End");
    }
}

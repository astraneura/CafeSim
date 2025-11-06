using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

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
    public List<GameObject> regularMachines;
    public List<GameObject> specialMachines;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dayTimerText = GameObject.Find("DayTimer").GetComponent<TextMeshProUGUI>();
        dayTimer = dayDuration;

        StartCoroutine(SpawnNewCustomerAfterDelay(1f));
    }

    void Start()
    {
        for (int i = 0; i < specialMachines.Count; i++)
        {
            specialMachines[i].SetActive(false);
        }
        for (int i = 0; i < regularMachines.Count; i++)
        {
            regularMachines[i].SetActive(true);
        }
        // foreach (var machine in specialMachines)
        // {
        //     machine.SetActive(false);
        // }
        // foreach (var machine in regularMachines)
        // {
        //     machine.SetActive(true);
        // }
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
        if (currentCustomers >= spawnPoints.Length)
        {
            Debug.Log("Max customers reached. Pausing spawn cycle.");
            return;
        }
        int randomNumber = Random.Range(0, 100);
        Debug.Log(randomNumber);
        if (randomNumber < 20) // 20% chance to spawn a confused customer
        {
            SpawnConfusedCustomer();
        }
        else
        {
            SpawnRegularCustomer();
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
        for (int i = 0; i < specialMachines.Count; i++)
        {
            specialMachines[i].SetActive(true);
        }
        for (int i = 0; i < regularMachines.Count; i++)
        {
            regularMachines[i].SetActive(false);
        }
        // foreach (var machine in regularMachines)
        // {
        //     machine.SetActive(false);
        // }
        // foreach (var machine in specialMachines)
        // {
        //     machine.SetActive(true);
        // }
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
        StartCoroutine(SpawnNewCustomerAfterDelay(spawnDelay)); // remove later - allow more customers for testing
        //spawnAllowed = false; // set spawning to false to allow player time to create custom drink
    }

    private void SpawnRegularCustomer()
    {
        for (int i = 0; i < specialMachines.Count; i++)
        {
            specialMachines[i].SetActive(false);
        }
        for (int i = 0; i < regularMachines.Count; i++)
        {
            regularMachines[i].SetActive(true);
        }
        // foreach (var machine in specialMachines)
        // {
        //     machine.SetActive(false);
        // }
        // foreach (var machine in regularMachines)
        // {
        //     machine.SetActive(true);
        // }
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
        StartCoroutine(SpawnNewCustomerAfterDelay(spawnDelay)); // continue spawning regular customers
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

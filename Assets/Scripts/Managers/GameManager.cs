using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    [Header("Customer Spawning")]
    public GameObject customerPrefab;
    public GameObject confusedCustomerPrefab;
    public Transform[] spawnPoints;
    public float spawnDelay = 12f;
    public int currentCustomers = 0;

    private int nextSpawnIndex = 0;
    private bool spawnAllowed = true;


    [Header("Day Timer")]
    public float dayDuration = 300f; // Duration of the day in seconds
    private TextMeshProUGUI dayTimerText;
    private float dayTimer;

    [Header("Machines")]
    public List<GameObject> regularMachines;
    public List<GameObject> specialMachines;

    [Header("UI Elements")]
    public TextMeshProUGUI qualityOrderText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < specialMachines.Count; i++)
        {
            specialMachines[i].SetActive(false);
        }
        for (int i = 0; i < regularMachines.Count; i++)
        {
            regularMachines[i].SetActive(true);
        }

        qualityOrderText.text = "";

        dayTimerText = GameObject.Find("DayTimer").GetComponent<TextMeshProUGUI>();
        dayTimer = dayDuration;

        Cursor.lockState = CursorLockMode.Locked;

        SpawnNewCustomer();
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            if (spawnAllowed)
            {
                SpawnNewCustomer();
            }
        }
    }

    void Update()
    {
        UpdateDayTimer();
    }

    private void SpawnNewCustomer()
    {
        if (spawnPoints.Length == 0)
            return;

        int attempts = 0;
        bool foundSpot = false;

        while(attempts < spawnPoints.Length)
        {
            Transform point = spawnPoints[nextSpawnIndex];
            if(!Physics.CheckSphere(point.position, 0.1f))
            {
                foundSpot = true;
                break;
            }
            nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;
            attempts++;
        }

        if(!foundSpot)
        {
            Debug.Log("No available spawn points found.");
            return;
        }

        int roll = Random.Range(0, 100);
        Transform spawnPoint = spawnPoints[nextSpawnIndex];
        if (roll < 20) // 20% chance to spawn a confused customer
        {
            Instantiate(confusedCustomerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        currentCustomers++;
        nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;
    }

    public void OnCustomerOrderCompleted()
    {
        currentCustomers = Mathf.Max(0, currentCustomers - 1);
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

    public void EnableQualityMachines()
    {
        foreach (var m in specialMachines)
        {
            m.SetActive(true);
        }
        foreach (var m in regularMachines)
        {
            m.SetActive(false);
        }
    }
    public void EnableRegularMachines()
    {
        foreach (var m in specialMachines)
        {
            m.SetActive(false);
        }
        foreach (var m in regularMachines)
        {
            m.SetActive(true);
        }
    }
}

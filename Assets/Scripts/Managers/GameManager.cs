using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    //variables for spawning customers
    public GameObject customerPrefab;
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

    private void Awake()
    {
        dayTimerText = GameObject.Find("DayTimer").GetComponent<TextMeshProUGUI>();
        dayTimer = dayDuration;

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
        Transform spawnPoint = spawnPoints[nextSpawnIndex];
        Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log($"Spawned customer at {spawnPoint.name}");
        currentCustomers++;

        // Cycle to the next spawn point
        nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;
        // Start the coroutine to spawn the next customer after a delay
        StartCoroutine(SpawnNewCustomerAfterDelay(spawnDelay));
    }

    public void OnCustomerOrderStarted()
    {
        if (!isSpawning)
            StartCoroutine(SpawnNewCustomerAfterDelay(spawnDelay));
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

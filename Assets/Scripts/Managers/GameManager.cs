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


    private void Awake()
    {
        StartCoroutine(SpawnNewCustomerAfterDelay(1f));
    }

    void Update()
    {
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
}

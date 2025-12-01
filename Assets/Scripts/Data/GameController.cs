using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject dc;
    private float startTime;

    public static GameController Instance { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        dc = GameObject.Find("DataController");
        startTime = Time.time;
    }
    public void CalculatePlayTime()
    {
        dc.GetComponent<UserProfileData>().timePlayed = Time.time - startTime;
    }
}

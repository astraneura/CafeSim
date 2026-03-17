using UnityEngine;

public class GameStateAtmosphere : MonoBehaviour
{
    public float value = 1f;
    public float maxValue = 100f;
    public float minPitch = 0.9f;
    public float maxPitch = 1.0f;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        float t = Mathf.Clamp01(value / maxValue);
        source.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }
}
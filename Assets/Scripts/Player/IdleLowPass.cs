using UnityEngine;
using UnityEngine.Audio;

public class IdleLowPass : MonoBehaviour
{
    public AudioMixer mixer;
    public float maxIdleTime = 4f;

    Vector3 checkPosition;
    float checkTimer;
    float idleTime;

    void Start()
    {
        checkPosition = transform.position;
    }

    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= 1f)
        {
            float moved = Vector3.Distance(transform.position, checkPosition);
            if (moved > 0.5f)
                idleTime = 0f;
            else
                idleTime += checkTimer;
            checkPosition = transform.position;
            checkTimer = 0f;
        }

        float t = Mathf.Clamp01(1f - idleTime / maxIdleTime);
        float cutoff = Mathf.Lerp(200f, 22000f, t);
        mixer.SetFloat("MusicLowPass", cutoff);
    }
}
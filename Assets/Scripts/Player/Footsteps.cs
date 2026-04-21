using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public float stepDistance = 1f;

    Vector3 lastStepPosition;

    void Start()
    {
        lastStepPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, lastStepPosition) >= stepDistance)
        {
            footstepSource.Play();
            lastStepPosition = transform.position;
        }
    }
}
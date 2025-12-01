using UnityEngine;

public class PersistentGO : MonoBehaviour
{
    public static PersistentGO instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

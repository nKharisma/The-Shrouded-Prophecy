using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps music playing across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    void OnDisable()
    {
        Destroy(gameObject); // Stops music when leaving the scene
    }
}


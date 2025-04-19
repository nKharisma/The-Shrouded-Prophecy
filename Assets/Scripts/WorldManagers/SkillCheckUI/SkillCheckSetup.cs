using UnityEngine;
using UnityEngine.SceneManagement;
public class SkillCheckSetup : MonoBehaviour
{
    [SerializeField] private GameObject[] skillCheckUIInstances; // Array of skill check UI instances in the scene
    private int currentSkillCheckIndex = 0; // Variable to track the current skill check index

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the SkillCheckInstanceHolder in the scene
        SkillCheckInstanceHolder holder = FindObjectOfType<SkillCheckInstanceHolder>();
        if (holder != null)
        {
            skillCheckUIInstances = holder.GetSkillCheckUIInstances();
            Debug.Log($"Loaded {skillCheckUIInstances.Length} skill check UI instances from the scene.");
        }
        else
        {
            Debug.LogWarning("No SkillCheckInstanceHolder found in the scene.");
        }

        currentSkillCheckIndex = 0; // Reset the index
    }

    public void SetupSkillCheck()
    {
        if (skillCheckUIInstances.Length == 0)
        {
            Debug.LogError("No skill check UI instances assigned.");
            return;
        }

        if (currentSkillCheckIndex < 0 || currentSkillCheckIndex >= skillCheckUIInstances.Length)
        {
            Debug.LogError("Invalid skill check index.");
            return;
        }

        GameObject skillCheckUIInstance = skillCheckUIInstances[currentSkillCheckIndex];
        if (skillCheckUIInstance == null)
        {
            Debug.LogError("SkillCheckUIInstance is not assigned for index: " + currentSkillCheckIndex);
            return;
        }

        SkillCheckManager.instance.SetSkillCheck(skillCheckUIInstance);
        Debug.Log("SetSkillCheck called with: " + skillCheckUIInstance.name);

        // Increment the skill check index for the next use
        currentSkillCheckIndex = (currentSkillCheckIndex + 1) % skillCheckUIInstances.Length;
    }
}
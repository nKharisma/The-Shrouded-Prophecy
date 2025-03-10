using UnityEngine;

public class SkillCheckSetup : MonoBehaviour
{
    [SerializeField] private GameObject[] skillCheckUIInstances; // Array of skill check UI instances in the scene
    private int currentSkillCheckIndex = 0; // Variable to track the current skill check index

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    public static CompanionManager instance;

    [System.Serializable]
    public class CompanionData
    {
        public string companionName;
        public GameObject companionObject;
        public PowerType powerType;
        public bool isRecruited;
    }
    
    public CompanionData currentlyEquippedCompanion;

    public enum PowerType { HealBoost, ImmunityBoost, EnemyDisableBoost }

    public List<CompanionData> allCompanions = new List<CompanionData>();
    
    public Transform playerTransform;
    public PowerUpManager powerUpManager;

    void Awake()
    {
        if (instance == null){
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }else {
            Destroy(gameObject);
        }
    }

	void Start()
	{
		EnableAllRecruitedPowerUps();
	}

	public void RecruitCompanion(string name)
{
    CompanionData comp = allCompanions.Find(c => c.companionName == name);
    if (comp != null && !comp.isRecruited)
    {
        comp.isRecruited = true;

        // Enable companion's follow AI if applicable
        CompanionFollowAI followAI = comp.companionObject.GetComponent<CompanionFollowAI>();
        if (followAI != null)
        {
            followAI.enabled = true;
        }

        Debug.Log($"{comp.companionName} has joined!");

        // Check if the PowerUpManager exists and enable the power-up
        if (powerUpManager != null)
        {
            Debug.Log($"Enabling power-up for {comp.powerType}.");
            powerUpManager.EnablePowerUp(comp.powerType);
        }
    }
    else if (comp != null && comp.isRecruited)
    {
        Debug.LogWarning($"Companion {name} is already recruited.");
    }
    else
    {
        Debug.LogWarning($"Companion {name} not found.");
    }
}


    public void EnableAllRecruitedPowerUps()
{
    if (powerUpManager == null)
    {
        Debug.Log("PowerUpManager is not assigned.");
        return;
    }

    foreach (var comp in allCompanions)
    {
        if (comp.isRecruited)
        {
            Debug.Log($"Enabling power-up for {comp.powerType}.");
            powerUpManager.EnablePowerUp(comp.powerType);
        }
    }
    }
}


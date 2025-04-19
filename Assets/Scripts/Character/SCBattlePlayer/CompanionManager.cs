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

    public enum PowerType { HealBoost, ImmunityBoost, EnemyDisableBoost }

    public List<CompanionData> allCompanions = new List<CompanionData>();
    
    public Transform playerTransform;
    public PowerUpManager powerUpManager;

    void Awake()
    {
        if (instance == null){
            DontDestroyOnLoad(gameObject);
            instance = this;
        }else {
            Destroy(gameObject);
        }
    }

    public void RecruitCompanion(string name)
    {
        CompanionData comp = allCompanions.Find(c => c.companionName == name);
        if (comp != null && !comp.isRecruited)
        {
            comp.isRecruited = true;
            //comp.companionObject.SetActive(true);
            
        CompanionFollowAI followAI = comp.companionObject.GetComponent<CompanionFollowAI>();
            if (followAI != null)
            {
                //followAI.player = playerTransform;
                followAI.enabled = true;
            }else if(followAI == null){
                //followAI.player = playerTransform;
                followAI.enabled = true;
            }
            Debug.Log($"{comp.companionName} has joined!");
        }
    }

    public List<PowerType> GetRecruitedPowers()
    {
        List<PowerType> powers = new List<PowerType>();
        foreach (var comp in allCompanions)
        {
            if (comp.isRecruited)
                powers.Add(comp.powerType);
        }
        return powers;
    }
}


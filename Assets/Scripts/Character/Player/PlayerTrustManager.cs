using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTrustManager : MonoBehaviour
{
    public static PlayerTrustManager instance;
    [Header("Trust")]
    [SerializeField] private int startingTrustAmount = 0;
    [SerializeField] private int maxTrustAmount = 150;
    [SerializeField] private Image trustMeterFill;
    private int currentTrustAmount;
    
    private void Awake() {
        
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }else {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onTrustGained += TrustGained;
        GameEventsManager.instance.playerEvents.onTrustLost += TrustLost;
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    
    private void OnDestroy() {
        GameEventsManager.instance.playerEvents.onTrustGained -= TrustGained;
        GameEventsManager.instance.playerEvents.onTrustLost -= TrustLost;
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    
    private void Start()
    {
        if(currentTrustAmount > 0)
        {
            GameEventsManager.instance.playerEvents.PlayerTrustMeterChange(currentTrustAmount);
        }
        UpdateInterface();
    }
    
    private void OnSceneChanged(Scene current, Scene next)
    {
        Debug.Log($"Scene changed from {current.name} to {next.name}");
    
        if (WorldSaveGameManager.instance != null && WorldSaveGameManager.instance.currentSaveData != null)
        {
            Debug.Log("Loading trust data on scene change...");
            LoadTrustData(ref WorldSaveGameManager.instance.currentSaveData);
        }
        else
        {
            Debug.LogWarning("WorldSaveGameManager or currentSaveData is null. Cannot load trust data.");
        }
    }
    
    public void SaveTrustData(ref CharacterSaveData saveData)
    {
        saveData.trustAmount = currentTrustAmount;
    }
    
    public void LoadTrustData(ref CharacterSaveData saveData)
    {
        currentTrustAmount = saveData.trustAmount;
        GameEventsManager.instance.playerEvents.PlayerTrustMeterChange(currentTrustAmount);
        UpdateInterface();
    }
    
    private void TrustGained(int trustAmount)
    {
        currentTrustAmount += trustAmount;
        
        if(currentTrustAmount > maxTrustAmount)
        {
            currentTrustAmount = maxTrustAmount;
        }
        
        GameEventsManager.instance.playerEvents.PlayerTrustMeterChange(currentTrustAmount);
        UpdateInterface();
    }
    
    private void TrustLost(int trustAmount)
    {
        currentTrustAmount -= trustAmount;
        
        if(currentTrustAmount < 0)
        {
            currentTrustAmount = 0;
        }
        
        GameEventsManager.instance.playerEvents.PlayerTrustMeterChange(currentTrustAmount);
        UpdateInterface();
    }
    
    void UpdateInterface()
    {
        trustMeterFill.fillAmount = (float)currentTrustAmount / (float)maxTrustAmount;
    }
}



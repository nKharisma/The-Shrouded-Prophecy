using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrustManager : MonoBehaviour
{
    [Header("Trust")]
    [SerializeField] private int startingTrustAmount = 0;
    [SerializeField] private int maxTrustAmount = 100;
    [SerializeField] private Image trustMeterFill;
    private int currentTrustAmount;
    
    private void Awake() {
        currentTrustAmount = startingTrustAmount;
    }
    
    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onTrustGained += TrustGained;
    }
    
    private void OnDestroy() {
        GameEventsManager.instance.playerEvents.onTrustGained -= TrustGained;
    }
    
    private void Start()
    {
        GameEventsManager.instance.playerEvents.PlayerTrustMeterChange(currentTrustAmount);
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
    
    void UpdateInterface()
    {
        trustMeterFill.fillAmount = (float)currentTrustAmount / (float)maxTrustAmount;
    }
}



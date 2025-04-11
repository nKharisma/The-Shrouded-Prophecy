using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCTimer : MonoBehaviour
{
    [SerializeField] public float startWaitSeconds;
    [SerializeField] TMP_Text text;
    private HealthManager healthManager;

    private float waitSeconds;
    private int waitSecondsInt;
    
    private SkillCheckManager skillCheck;
    
    void Start()
    {
        skillCheck = SkillCheckManager.instance;
        
        if(skillCheck == null)
        {
            Debug.LogError("SkillCheckManager is not set.");
            return;
        }
        
        healthManager = FindObjectOfType<HealthManager>();
        resetTimer();
    }

    private void FixedUpdate()
    {
        if (waitSeconds > 0)
        {
            waitSeconds -= Time.fixedDeltaTime;
            waitSecondsInt = (int)waitSeconds;
            text.text = waitSecondsInt.ToString();
        }
        else
        {
            skillCheck.endConditions(1);
            resetTimer();

            healthManager.Heal(100f);
        }
    }

    public void resetTimer()
    {
        waitSeconds = startWaitSeconds;
        text.text = ((int)waitSeconds).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public GameObject scPlayer;
    public Image[] powerUpImages;
    public Sprite[] greyPowerUpSprites;
    public Outline[] powerUpOutlines;
    public GameObject[] enemies;
    public HealthManager healthManager;
    private PlayerControls inputActions;

    private int currentPowerUp = 0;
    [SerializeField] private int disabled = 1;

    // Start is called before the first frame update
    void Start()
    {   
        UpdatePUSelection();
    }


    // Update is called once per frame
    void Update()
    {   
        if (disabled == 0)
        {
            // change to inputActions once integrated into main prefab
            // if(inputActions.PlayerMovement.LeftPowerUp.WasPressedThisFrame())
            if(Input.GetKeyDown(KeyCode.Q))
            {
                ChangePU(-1);
            }

            // else if(inputActions.PlayerMovement.RightPowerUp.WasPressedThisFrame())
            if(Input.GetKeyDown(KeyCode.E))
            {
                ChangePU(1);
            }

            // else if(inputActions.PlayerMovement.SelectPowerUp.WasPressedThisFrame())
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ActivatePU(currentPowerUp);
            }
        }

    }

    void ChangePU(int direction)
    {
        powerUpOutlines[currentPowerUp].enabled = false;

        currentPowerUp = (currentPowerUp + direction + powerUpImages.Length) % powerUpImages.Length;
        powerUpOutlines[currentPowerUp].enabled = true;
    }

    void UpdatePUSelection()
    {
        for (int i = 0; i < powerUpOutlines.Length; i++)
        {
            powerUpOutlines[i].enabled = (i == currentPowerUp);
        }
    }
    
    /*
    IEnumerator DiableEnemey(float time)
    {
        foreach(GameObject enemy in enemies)
        {
            EnemyPatrol script = enemy.GetComponent<EnemyPatrol>();
            if (script != null)
            {
                script.enabled = false;
            }
        }

        yield return new WaitForSeconds(time);

        foreach(GameObject enemy in enemies)
        {
            EnemyPatrol script = enemy.GetComponent<EnemyPatrol>();
            if (script != null)
            {
                script.enabled = true;
            }
        }
    }*/
    
        public void EnablePowerUp(CompanionManager.PowerType powerType)
    {
        switch (powerType)
        {
            case CompanionManager.PowerType.HealBoost:
                powerUpImages[0].sprite = greyPowerUpSprites[0]; // Enable the HealBoost power-up
                break;
            case CompanionManager.PowerType.ImmunityBoost:
                powerUpImages[1].sprite = greyPowerUpSprites[1]; // Enable the ImmunityBoost power-up
                break;
            default:
                Debug.LogWarning("Unknown power type.");
                break;
        }
    
        Debug.Log($"Power-up for {powerType} enabled.");
    }

    void ActivatePU(int index)
    {
        switch (index)
        {
            case 0:
                healthManager.Heal(25);
                break;
            case 1:
                StartCoroutine(healthManager.IsImmuned(7f));
                break;
            case 2:

                break;
        }
    }
}

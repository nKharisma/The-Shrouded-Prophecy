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
    private int disabled = 1;

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

        else if (disabled == 1)
        {
            for (int i = 0; i < powerUpImages.Length; i++)
            {
                powerUpImages[i].sprite = greyPowerUpSprites[i];
            }

            powerUpOutlines[currentPowerUp].enabled = false;
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
                StartCoroutine(DiableEnemey(7f));
                break;
            case 3:

                break;
        }
    }
}

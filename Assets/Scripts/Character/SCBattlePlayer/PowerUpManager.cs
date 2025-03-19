using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public GameObject scPlayer;
    public Image[] powerUpImages;
    public Outline[] powerUpOutlines;
    public HealthManager healthManager;
    private PlayerControls inputActions;

    private int currentPowerUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePUSelection();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputActions.PlayerMovement.LeftPowerUp.WasPressedThisFrame())
        {
            ChangePU(-1);
        }
        else if(inputActions.PlayerMovement.RightPowerUp.WasPressedThisFrame())
        {
            ChangePU(1);
        }
        else if(inputActions.PlayerMovement.SelectPowerUp.WasPressedThisFrame())
        {
            ActivatePU(currentPowerUp);
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
            case 3:

                break;
        }
    }
}

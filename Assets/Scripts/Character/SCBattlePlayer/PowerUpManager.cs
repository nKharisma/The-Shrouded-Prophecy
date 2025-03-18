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

    private int currentPowerUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePUSelection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangerPU(int direction)
    {

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

                break;
            case 2:

                break;
            case 3:

                break;
        }
    }
}

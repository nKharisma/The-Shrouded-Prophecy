using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillCheckManager : MonoBehaviour
{
    [Header("Skill Check UI")]
    [SerializeField] private GameObject skillCheckUI;

    public bool skillCheckUIIsPlaying { get; private set; }

    void Start()
    {
        skillCheckUIIsPlaying = false;
        skillCheckUI.SetActive(false);
    }

    public void playSkillCheckUI()
    {
        skillCheckUIIsPlaying = true;
        skillCheckUI.SetActive(true);
    }

    public void endConditions(int condition)
    {
        // survived the clock
        if (condition == 1)
        {
            skillCheckUIIsPlaying = false;
            skillCheckUI.SetActive(false);
        }

        // ran out of health
        if (condition == 2)
        {
            skillCheckUIIsPlaying = false;
            skillCheckUI.SetActive(false);
        }
    }
}

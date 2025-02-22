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

    void Update()
    {
        
    }
}

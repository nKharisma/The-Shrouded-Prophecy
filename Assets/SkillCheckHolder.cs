using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckInstanceHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] skillCheckUIInstances; // Array of skill check UI instances in the scene

    public GameObject[] GetSkillCheckUIInstances()
    {
        return skillCheckUIInstances;
    }
}

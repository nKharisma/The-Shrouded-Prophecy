using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] public float damage;

    private SkillCheckManager skillCheck;
    private SCTimer scTimer;

    void Start()
    {
        skillCheck = FindObjectOfType<SkillCheckManager>();
        scTimer = FindObjectOfType<SCTimer>();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            healthManager.TakeDamage(damage);
        }

        if (healthManager.healthAmount <= 0)
        {
            skillCheck.endConditions(2);
            healthManager.Heal(100f);

            scTimer.resetTimer();
        }
    }
}

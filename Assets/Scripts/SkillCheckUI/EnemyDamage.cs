using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] public float damage;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            healthManager.TakeDamage(damage);
        }
    }
}

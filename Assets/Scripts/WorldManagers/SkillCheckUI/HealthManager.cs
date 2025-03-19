using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private bool immuned = false;
    [SerializeField] public Image healthBar;
    [SerializeField] public float healthAmount = 100f;

    public void TakeDamage(float damage)
    {
        if (immuned == false)
        {
            healthAmount -= damage;
            healthBar.fillAmount = healthAmount / 100f;
        }
    }

    public void Heal(float healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

    public IEnumerator IsImmuned(float time)
    {
        immuned = true;
        yield return new WaitForSeconds(time);

        immuned = false;
    }
}

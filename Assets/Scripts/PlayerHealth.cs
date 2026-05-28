using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    private bool isDead = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        WristUI.Instance?.UpdateUI();

        if (currentHealth <= 0)
        {
            isDead = true;
            DeathScreen.Instance?.Show(ScoreManager.Instance.Score);
        }
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        isDead = false;
        WristUI.Instance?.UpdateUI();
    }
}
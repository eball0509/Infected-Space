using UnityEngine;
using TMPro;

public class WristUI : MonoBehaviour
{
    public static WristUI Instance;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "HP: " + PlayerHealth.Instance?.currentHealth;
        if (scoreText != null)
            scoreText.text = "Score: " + ScoreManager.Instance?.Score;
        if (waveText != null)
            waveText.text = "Wave: " + WaveManager.Instance?.CurrentWave;
    }
}
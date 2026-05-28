using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int Score { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddKill()
    {
        Score += 100;
        WristUI.Instance?.UpdateUI();
    }

    public void Reset()
    {
        Score = 0;
        WristUI.Instance?.UpdateUI();
    }
}
using UnityEngine;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public EnemySpawner spawner;
    public GameObject waveAnnouncementCanvas;
    public TextMeshProUGUI waveText;
    public float announcementDuration = 2f;
    public float waveCooldown = 3f;

    public int CurrentWave { get; private set; } = 0;
    public int EnemiesRemainingInWave { get; private set; } = 0;
    private int enemiesToSpawnThisWave = 0;
    private bool waveActive = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        CurrentWave++;
        enemiesToSpawnThisWave = 5 + (CurrentWave * 2);
        EnemiesRemainingInWave = enemiesToSpawnThisWave;
        waveActive = true;

        WristUI.Instance?.UpdateUI();
        yield return StartCoroutine(ShowWaveAnnouncement());

        spawner.StartWave(enemiesToSpawnThisWave);
    }

    public void OnEnemyKilled()
    {
        EnemiesRemainingInWave--;
        ScoreManager.Instance?.AddKill();
        WristUI.Instance?.UpdateUI();

        if (EnemiesRemainingInWave <= 0 && waveActive)
        {
            waveActive = false;
            StartCoroutine(WaveCooldown());
        }
    }

    IEnumerator WaveCooldown()
    {
        yield return new WaitForSeconds(waveCooldown);
        StartCoroutine(StartNextWave());
    }

    IEnumerator ShowWaveAnnouncement()
    {
        Transform player = PlayerLocator.GetPlayer();
        if (player != null)
        {
            Vector3 forward = new Vector3(player.forward.x, 0, player.forward.z).normalized;
            waveAnnouncementCanvas.transform.position = player.position + forward * 2f;
            waveAnnouncementCanvas.transform.rotation = Quaternion.LookRotation(forward);
        }

        waveText.text = "WAVE " + CurrentWave;
        waveAnnouncementCanvas.SetActive(true);

        yield return new WaitForSeconds(announcementDuration);

        waveAnnouncementCanvas.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance;

    public GameObject panel;
    public TextMeshProUGUI scoreText;
    public Button replayButton;
    public Button mainMenuButton;
    public Button quitButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panel.SetActive(false);
    }

    void Start()
    {
        replayButton.onClick.AddListener(OnReplay);
        mainMenuButton.onClick.AddListener(OnMainMenu);
        quitButton.onClick.AddListener(OnQuit);
    }

    public void Show(int score)
    {
        StartCoroutine(ShowScreen(score));
    }

    IEnumerator ShowScreen(int score)
    {
        yield return new WaitForSeconds(0.5f);

        Transform player = PlayerLocator.GetPlayer();
        if (player != null)
        {
            Vector3 forward = new Vector3(player.forward.x, 0, player.forward.z).normalized;
            panel.transform.position = player.position + forward * 2f;
            panel.transform.rotation = Quaternion.LookRotation(forward);
        }

        scoreText.text = "Score: " + score;
        panel.SetActive(true);
    }

    public void OnReplay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
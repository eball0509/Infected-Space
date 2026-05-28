using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Canvas menuCanvas;

    void Start()
    {
        playButton.onClick.AddListener(LoadGame);
        StartCoroutine(PositionCanvas());
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator PositionCanvas()
    {
        yield return new WaitForSeconds(1f);

        Transform player = PlayerLocator.GetPlayer();
        if (player != null)
        {
            Vector3 forward = new Vector3(player.forward.x, 0, player.forward.z).normalized;
            menuCanvas.transform.position = player.position + forward * 2f;
            menuCanvas.transform.rotation = Quaternion.LookRotation(forward);
        }
    }
}
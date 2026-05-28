using UnityEngine;
using UnityEngine.UI;

public class ControllerUIInteraction : MonoBehaviour
{
    public float rayLength = 5f;
    public Transform rayOrigin;
    public GameObject deathScreenCanvas;
    private LineRenderer lineRenderer;
    private bool isMainMenuScene;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isMainMenuScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu";
    }

    void Update()
    {
        Transform origin = rayOrigin != null ? rayOrigin : transform;

        bool deathScreenActive = deathScreenCanvas != null && deathScreenCanvas.activeSelf;
        bool shouldShowRay = isMainMenuScene || deathScreenActive;

        lineRenderer.enabled = shouldShowRay;

        if (!shouldShowRay) return;

        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, origin.position + origin.forward * rayLength);

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) ||
            OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            Ray ray = new Ray(origin.position, origin.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                Button btn = hit.collider.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke();
                }
            }
        }
    }
}
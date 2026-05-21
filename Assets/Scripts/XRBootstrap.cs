using UnityEngine;

public class XRBootstrap : MonoBehaviour
{
    public static float FloorY { get; private set; }

    void Awake()
    {
        Transform centerEye = FindCenterEye(transform);
        if (centerEye != null)
        {
            PlayerLocator.SetPlayer(centerEye);
        }

        // Use OVR to get floor level
        OVRPlugin.SetTrackingOriginType(OVRPlugin.TrackingOrigin.FloorLevel);
        FloorY = 0f; // floor is always Y 0 when using FloorLevel tracking origin
    }

    Transform FindCenterEye(Transform root)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
        {
            if (t.name.ToLower().Contains("centereye"))
                return t;
        }
        return null;
    }
}
using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public float range = 10f;
    public GameObject muzzleFlash;
    private bool triggerDown = false;

    void Update()
    {
        float right = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

        if (right > 0.5f && !triggerDown)
        {
            triggerDown = true;
            Shoot();
        }
        else if (right <= 0.5f)
        {
            triggerDown = false;
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Destroy(hit.collider.gameObject);
        }

        GameObject flash = Instantiate(muzzleFlash, muzzleFlash.transform.parent);
        flash.SetActive(true);
        Destroy(flash, 0.05f);
    }

}
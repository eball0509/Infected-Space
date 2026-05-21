using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float range = 50f;
    public int damage = 1;

    public GameObject muzzleFlash;
    public Transform muzzlePoint;

    private bool triggerDown = false;

    void Update()
    {
        float right = OVRInput.Get(
            OVRInput.Axis1D.PrimaryIndexTrigger,
            OVRInput.Controller.RTouch
        );

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
        Ray ray = new Ray(
            muzzlePoint.position,
            muzzlePoint.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            EnemyHealth enemy =
                hit.collider.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        if (muzzleFlash != null)
        {
            GameObject flash = Instantiate(
                muzzleFlash,
                muzzlePoint.position,
                muzzlePoint.rotation
            );

            flash.transform.localScale =
                new Vector3(0.05f, 0.05f, 0.15f);

            Destroy(flash, 0.1f);
        }
    }
}
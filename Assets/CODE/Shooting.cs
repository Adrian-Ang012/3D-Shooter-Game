using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera playerCamera;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 20f;
    public float fireRate = 0.2f;
    public float maxDistance = 200f;
    public LayerMask aimLayers = ~0;

    private float nextFireTime = 0f;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        if (bulletPrefab == null || firePoint == null || playerCamera == null)
            return;

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, aimLayers))
            targetPoint = hit.point;
        else
            targetPoint = ray.origin + ray.direction * maxDistance;

        Vector3 toTarget = targetPoint - firePoint.position;

        if (Vector3.Dot(firePoint.forward, toTarget) <= 0f)
        {
            targetPoint = firePoint.position + playerCamera.transform.forward * maxDistance;
            toTarget = targetPoint - firePoint.position;
        }

        Vector3 shootDirection = toTarget.normalized;

        Quaternion baseRotation = Quaternion.LookRotation(shootDirection);
        Quaternion offset = Quaternion.Euler(90f, 0f, 90f);

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            baseRotation * offset
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = shootDirection * bulletSpeed;

        Destroy(bullet, 5f);
    }
}
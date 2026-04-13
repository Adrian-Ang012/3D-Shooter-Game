using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;

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
        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab is not assigned!");
            return;
        }

        Vector3 spawnPos = transform.position + transform.forward * 1f + Vector3.up * 1f;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        bullet.transform.forward = transform.forward;
        bullet.transform.Rotate(90f, 0f, 0f); 
        bullet.transform.Rotate(0f, 0f, 90f); 

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = transform.forward * bulletSpeed;

        Destroy(bullet, 5f);
    }
}
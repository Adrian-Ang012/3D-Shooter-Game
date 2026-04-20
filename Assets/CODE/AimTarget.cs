using UnityEngine;

public class AimTargetFollower : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 200f;
    public LayerMask aimLayers = ~0;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, aimLayers))
            targetPoint = hit.point;
        else
            targetPoint = ray.origin + ray.direction * maxDistance;

        transform.position = targetPoint;
    }
}
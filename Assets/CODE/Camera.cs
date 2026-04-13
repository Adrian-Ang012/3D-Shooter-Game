using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float distance = 1f;
    public float heightOffset = 1f;
    public float mouseSensitivity = 100f;
    public float verticalClamp = 80f;
    public float minDistance = 0.5f;
    public LayerMask collisionLayers;

    float yaw = 0f;
    float pitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -verticalClamp, verticalClamp);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = player.position + rotation * new Vector3(0f, heightOffset, -distance);

        Vector3 direction = desiredPosition - (player.position + Vector3.up * heightOffset);
        float targetDistance = distance;

        if (Physics.Raycast(player.position + Vector3.up * heightOffset, direction.normalized, out RaycastHit hit, distance, collisionLayers))
            targetDistance = Mathf.Clamp(hit.distance - 0.1f, minDistance, distance);

        Vector3 finalPosition = player.position + rotation * new Vector3(0f, heightOffset, -targetDistance);
        transform.position = finalPosition;
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}
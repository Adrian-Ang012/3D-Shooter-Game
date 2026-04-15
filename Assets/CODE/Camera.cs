using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Camera Positioning")]
    public float distance = 3f;
    public float heightOffset = 1.5f;
    public float shoulderOffset = 0.8f; // push camera to right shoulder

    [Header("Sensitivity & Limits")]
    public float mouseSensitivity = 100f;
    public float minPitch = -10f; 
    public float maxPitch = 60f;  

    [Header("Collision Settings")]
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
        // Mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Rotation based on yaw/pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Base position above player
        Vector3 basePosition = player.position + Vector3.up * heightOffset;

        // Backward offset (distance behind player)
        Vector3 backwardOffset = rotation * Vector3.back * distance;

        // Shoulder offset applied relative to player's right (not rotated with pitch/yaw)
        Vector3 shoulder = player.right * shoulderOffset;

        // Desired position
        Vector3 desiredPosition = basePosition + backwardOffset + shoulder;

        // Collision check
        Vector3 direction = desiredPosition - basePosition;
        float targetDistance = distance;

        if (Physics.Raycast(basePosition, direction.normalized, out RaycastHit hit, distance, collisionLayers))
        {
            targetDistance = Mathf.Clamp(hit.distance - 0.1f, minDistance, distance);
            desiredPosition = basePosition + rotation * Vector3.back * targetDistance + shoulder;
        }

        // Final camera position
        transform.position = desiredPosition;

        // Always look at player
        transform.LookAt(basePosition);
    }
}

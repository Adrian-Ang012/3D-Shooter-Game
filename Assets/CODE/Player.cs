using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cameraTransform;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        RotateWithCamera();
        Move();
    }

    void RotateWithCamera()
    {
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        if (cameraForward != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(cameraForward);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * z + right * x;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
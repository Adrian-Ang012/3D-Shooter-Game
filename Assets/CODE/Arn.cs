using UnityEngine;

public class ArmPitchOnly : MonoBehaviour
{
    public Transform cameraTransform;
    public float smooth = 10f;
    public float strength = 0.5f; // small effect

    float current;

    Quaternion baseRotation;

    void Start()
    {
        baseRotation = transform.localRotation;
    }

    void LateUpdate()
    {
        float pitch = cameraTransform.eulerAngles.x;

        if (pitch > 180f)
            pitch -= 360f;

        current = Mathf.Lerp(current, pitch * strength, smooth * Time.deltaTime);

        // small additive rotation ONLY
        transform.localRotation = baseRotation * Quaternion.Euler(current, 0f, 0f);
    }
}
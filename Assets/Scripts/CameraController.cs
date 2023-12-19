using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSens = 1f;

    private float _xRotation = 0f;
    public void CameraRotation(Transform playerTransform)
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -30f, 30f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}

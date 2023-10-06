using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSens = 1f;

    public Transform Player;

    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -30f, 30f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}

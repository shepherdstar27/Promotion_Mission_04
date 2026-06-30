using UnityEngine;

public class CameraLook: MonoBehaviour
{
    [SerializeField] private Transform playerBody;     // 좌우 회전을 적용할 플레이어
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float minPitch = -30f;    // 위아래로 볼 수 있는 최소 각도
    [SerializeField] private float maxPitch = 60f;     // 위아래로 볼 수 있는 최대 각도

    private float pitch = 0f; 

    private void Start()
    {
        // 마우스 락
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        playerBody.Rotate(Vector3.up * mouseX);

        pitch = pitch - mouseY;
        if (pitch < minPitch) pitch = minPitch;
        if (pitch > maxPitch) pitch = maxPitch;

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
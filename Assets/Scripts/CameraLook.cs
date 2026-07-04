using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _mouseSensitivity = 3f;
    [SerializeField] private float _minPitch = -30f;
    [SerializeField] private float _maxPitch = 60f;

    private float _pitch = 0f;

    private void Update()
    {
        // 커서가 풀려 있으면(UI 조작 중) 시점 회전 멈춤
        if (CursorManager.Instance != null && CursorManager.Instance.IsCursorUnlocked)
            return;

        float mouseX = Input.GetAxisRaw("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _mouseSensitivity;

        _playerBody.Rotate(Vector3.up * mouseX);

        _pitch = _pitch - mouseY;
        if (_pitch < _minPitch) _pitch = _minPitch;
        if (_pitch > _maxPitch) _pitch = _maxPitch;

        transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody _rb;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        // 커서가 풀려 있으면(UI 조작 중) 이동 입력 무시
        if (CursorManager.Instance != null && CursorManager.Instance.IsCursorUnlocked)
        {
            _moveDirection = Vector3.zero;
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        _moveDirection = forward * vertical + right * horizontal;
        if (_moveDirection.sqrMagnitude > 1f)
            _moveDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = _rb.position + _moveDirection * _moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(targetPosition);
    }
}
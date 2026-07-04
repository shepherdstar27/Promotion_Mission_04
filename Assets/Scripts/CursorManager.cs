using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    // 커서를 풀어야 하는 UI가 몇 개 열려 있는지 카운트
    private int _unlockRequestCount = 0;

    // 지금 커서가 풀려 있는지 외부에서 읽기
    public bool IsCursorUnlocked { get { return _unlockRequestCount > 0; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        UnlockCursor();
    }

    public void LockCursorForGameplay()
    {
        _unlockRequestCount = 0; // 혹시 남아있는 요청 초기화
        LockCursor();
    }

    // UI가 열릴 때 호출
    public void RequestUnlock()
    {
        _unlockRequestCount = _unlockRequestCount + 1;
        ApplyState();
    }

    // UI가 닫힐 때 호출
    public void ReleaseUnlock()
    {
        _unlockRequestCount = _unlockRequestCount - 1;
        if (_unlockRequestCount < 0)
            _unlockRequestCount = 0;
        ApplyState();
    }

    private void ApplyState()
    {
        if (_unlockRequestCount > 0)
            UnlockCursor();
        else
            LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
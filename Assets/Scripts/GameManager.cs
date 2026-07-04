using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string _defaultPlayerName = "Player";
    [SerializeField] private GameObject _startButton; // 시작 버튼 오브젝트 연결

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // 게임 시작 버튼이 이 함수를 부른다.
    public void StartGame()
    {
        Debug.Log("게임 시작");

        if (CursorManager.Instance != null)
            CursorManager.Instance.LockCursorForGameplay();

        NetworkManager.Instance.RequestCreateLocalPlayer(_defaultPlayerName);
        NetworkManager.Instance.RequestCreateInventory();
        UIManager.Instance.OpenUI(UIType.PlayerProfile);

        if (_startButton != null)
            _startButton.SetActive(false);
    }
}
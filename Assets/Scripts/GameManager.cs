using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string _defaultPlayerName = "Player";

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

        // 1) 먼저 플레이어(ViewModel + View) 생성
        NetworkManager.Instance.RequestCreateLocalPlayer(_defaultPlayerName);

        // 2) 그 다음 UI 열기
        UIManager.Instance.OpenUI(UIType.PlayerProfile);
        UIManager.Instance.OpenUI(UIType.MVVMTest);
    }
}
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    // 어디서든 접근할 수 있게 하는 단순 싱글톤
    public static NetworkManager Instance { get; private set; }

    // 순수 C# 서비스를 품는다.
    private PlayerService _playerService;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerService = new PlayerService();
    }

    // 로컬 플레이어 생성 요청을 받는 창구
    public void RequestCreateLocalPlayer(string playerName)
    {
        // 1) 서비스에게 ViewModel 생성을 맡김
        PlayerViewModel viewModel = _playerService.CreateLocalPlayer(playerName);

        // 2) ViewModel이 정상 생성되었으면, 화면(View) 생성을 게임오브젝트 매니저에 요청
        if (viewModel != null)
        {
            GameObjectManager.Instance.SpawnLocalPlayer(viewModel);
        }
    }

    // 다른 곳(프로필 UI, 테스트 UI)에서 로컬 플레이어 ViewModel을 받아갈 창구
    public PlayerViewModel GetLocalPlayerViewModel()
    {
        return _playerService.GetLocalPlayerViewModel();
    }


    // 로컬 플레이어 Exp 추가 요청
    public void RequestAddPlayerExp(int amount)
    {
        PlayerViewModel viewModel = _playerService.GetLocalPlayerViewModel();
        if (viewModel == null)
        {
            Debug.LogWarning("로컬 플레이어가 아직 없습니다.");
            return;
        }

        viewModel.AddExp(amount);
    }

    // 로컬 플레이어 이름 변경 요청
    public void RequestChangePlayerName(string newName)
    {
        PlayerViewModel viewModel = _playerService.GetLocalPlayerViewModel();
        if (viewModel == null)
        {
            Debug.LogWarning("로컬 플레이어가 아직 없습니다.");
            return;
        }

        viewModel.ChangeName(newName); 
    }








}
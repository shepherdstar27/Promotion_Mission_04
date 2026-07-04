public class PlayerService
{
    // 지금 로컬 플레이어의 ViewModel을 보관하는 곳
    private PlayerViewModel _localPlayerViewModel;

    // 로컬 플레이어 ViewModel을 생성한다. 이미 있으면 다시 안 만든다.
    public PlayerViewModel CreateLocalPlayer(string playerName)
    {
        if (_localPlayerViewModel != null)
        {
            // 이미 생성되어 있으면 기존 것을 그대로 돌려줌 (중복 생성 방지)
            return _localPlayerViewModel;
        }

        _localPlayerViewModel = new PlayerViewModel(playerName);
        return _localPlayerViewModel;
    }

    // 보관 중인 로컬 플레이어 ViewModel을 꺼내준다. 없으면 null.
    public PlayerViewModel GetLocalPlayerViewModel()
    {
        return _localPlayerViewModel;
    }
}
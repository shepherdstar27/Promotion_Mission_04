using UnityEngine;

public class LocalPlayerView : ViewBase
{
    // 편의를 위해 넘겨받은 ViewModel을 플레이어 타입으로 보관
    private PlayerViewModel _playerViewModel;

    // 바인딩 직후 한 번 호출됨 (ViewBase가 부름)
    protected override void OnBind()
    {
        _playerViewModel = ViewModel as PlayerViewModel;

        if (_playerViewModel == null)
        {
            Debug.LogError("LocalPlayerView에 PlayerViewModel이 아닌 것이 바인딩되었습니다.");
            return;
        }

        Debug.Log("플레이어 View 바인딩됨: " + _playerViewModel.PlayerName);
    }

    // 어떤 속성이 바뀌면 호출됨 (ViewBase가 부름)
    protected override void OnPropertyChanged(string propertyName)
    {
        // 플레이어 본체는 지금 특별히 표시할 게 없으므로 로그만.
        // 예를 들어 이름표(네임태그)를 머리 위에 띄운다면 여기서 갱신.
        if (propertyName == "PlayerName")
        {
            Debug.Log("플레이어 이름 변경 감지: " + _playerViewModel.PlayerName);
        }
    }
}
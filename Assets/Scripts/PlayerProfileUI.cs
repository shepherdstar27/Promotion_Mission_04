using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProfileUI : ViewBase
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _expText;

    private PlayerViewModel _playerViewModel;

    private void Start()
    {
        // 이미 생성되어 있는 로컬 플레이어 ViewModel을 창구에서 받아온다.
        PlayerViewModel viewModel = NetworkManager.Instance.GetLocalPlayerViewModel();

        if (viewModel == null)
        {
            Debug.LogWarning("아직 로컬 플레이어 ViewModel이 없습니다. 시작 후 다시 시도하세요.");
            return;
        }

        // 받아온 ViewModel과 바인딩 (ViewBase의 Bind)
        Bind(viewModel);
    }

    protected override void OnBind()
    {
        _playerViewModel = ViewModel as PlayerViewModel;
        if (_playerViewModel == null)
            return;

        // 바인딩 직후 현재 값으로 전체 화면을 한 번 채운다.
        RefreshAll();
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        if (_playerViewModel == null)
            return;

        // 바뀐 속성에 해당하는 부분만 갱신
        if (propertyName == "PlayerName")
            UpdateName();
        else if (propertyName == "Level")
            UpdateLevel();
        else if (propertyName == "Exp")
            UpdateExp();
    }

    private void RefreshAll()
    {
        UpdateName();
        UpdateLevel();
        UpdateExp();
    }

    private void UpdateName()
    {
        if (_nameText != null)
            _nameText.text = _playerViewModel.PlayerName;
    }

    private void UpdateLevel()
    {
        if (_levelText != null)
            _levelText.text = "Lv. " + _playerViewModel.Level;
    }

    private void UpdateExp()
    {
        if (_expText != null)
            _expText.text = "EXP " + _playerViewModel.Exp + " / " + _playerViewModel.ExpToNextLevel;
    }
}
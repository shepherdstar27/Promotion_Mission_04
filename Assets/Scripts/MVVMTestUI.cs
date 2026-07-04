using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MVVMTestUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInputField; // 새 이름 입력
    [SerializeField] private Button _changeNameButton;       // 이름 변경 버튼
    [SerializeField] private Button _addExpButton;           // Exp 추가 버튼
    [SerializeField] private int _expPerClick = 30;          // 버튼 한 번에 더할 Exp

    private void Start()
    {
        // 버튼 클릭에 함수를 연결 
        if (_changeNameButton != null)
            _changeNameButton.onClick.AddListener(OnClickChangeName);

        if (_addExpButton != null)
            _addExpButton.onClick.AddListener(OnClickAddExp);
    }

    private void OnDestroy()
    {
        // 연결 해제 (중복 구독·누수 방지)
        if (_changeNameButton != null)
            _changeNameButton.onClick.RemoveListener(OnClickChangeName);

        if (_addExpButton != null)
            _addExpButton.onClick.RemoveListener(OnClickAddExp);
    }

    private void OnClickChangeName()
    {
        if (_nameInputField == null)
            return;

        string newName = _nameInputField.text;
        // 네트워크 매니저를 통해 서비스에 이름 변경을 요청
        NetworkManager.Instance.RequestChangePlayerName(newName);
    }

    private void OnClickAddExp()
    {
        // 네트워크 매니저를 통해 서비스에 Exp 추가를 요청
        NetworkManager.Instance.RequestAddPlayerExp(_expPerClick);
    }
}
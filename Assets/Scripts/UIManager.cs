using System.Collections.Generic;
using UnityEngine;

// UI 이름표
public enum UIType
{
    PlayerProfile,
    MVVMTest,
    Inventory,
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [System.Serializable]
    public class UIEntry
    {
        public UIType Type;
        public GameObject Panel; // 해당 UI 패널 오브젝트
        public KeyCode ToggleKey = KeyCode.None; // 여닫기 단축키 (None이면 단축키 없음)
        public bool UnlockCursorWhenOpen = true;  // 열릴 때 커서를 풀지 여부
    }

    [SerializeField] private List<UIEntry> _uiEntries = new List<UIEntry>();

    private Dictionary<UIType, UIEntry> _entryTable = new Dictionary<UIType, UIEntry>();
    private Dictionary<UIType, bool> _openState = new Dictionary<UIType, bool>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _entryTable.Clear();
        _openState.Clear();

        for (int i = 0; i < _uiEntries.Count; i++)
        {
            UIEntry entry = _uiEntries[i];
            if (entry.Panel == null)
                continue;

            _entryTable[entry.Type] = entry;
            _openState[entry.Type] = false;
            entry.Panel.SetActive(false); // 시작 시 모두 닫힘
        }
    }


    private void Update()
    {
        // 각 UI의 단축키 입력을 확인해 여닫기
        for (int i = 0; i < _uiEntries.Count; i++)
        {
            UIEntry entry = _uiEntries[i];
            if (entry.ToggleKey == KeyCode.None)
                continue;

            if (Input.GetKeyDown(entry.ToggleKey))
                ToggleUI(entry.Type);
        }
    }

    public void ToggleUI(UIType type)
    {
        if (_openState.ContainsKey(type) == false)
            return;

        if (_openState[type])
            CloseUI(type);
        else
            OpenUI(type);
    }

    public void OpenUI(UIType type)
    {
        if (_entryTable.ContainsKey(type) == false)
            return;
        if (_openState[type]) // 이미 열려 있으면 무시 (중복 방지)
            return;

        UIEntry entry = _entryTable[type];
        entry.Panel.SetActive(true);
        _openState[type] = true;

        if (entry.UnlockCursorWhenOpen && CursorManager.Instance != null)
            CursorManager.Instance.RequestUnlock();
    }

    public void CloseUI(UIType type)
    {
        if (_entryTable.ContainsKey(type) == false)
            return;
        if (_openState[type] == false) // 이미 닫혀 있으면 무시
            return;

        UIEntry entry = _entryTable[type];
        entry.Panel.SetActive(false);
        _openState[type] = false;

        if (entry.UnlockCursorWhenOpen && CursorManager.Instance != null)
            CursorManager.Instance.ReleaseUnlock();
    }
}
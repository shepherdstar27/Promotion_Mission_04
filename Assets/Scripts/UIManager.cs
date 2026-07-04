using System.Collections.Generic;
using UnityEngine;

// UI 이름표
public enum UIType
{
    PlayerProfile,
    MVVMTest,
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [System.Serializable]
    public class UIEntry
    {
        public UIType Type;
        public GameObject Panel; // 해당 UI 패널 오브젝트
    }

    [SerializeField] private List<UIEntry> _uiEntries = new List<UIEntry>();

    private Dictionary<UIType, GameObject> _uiTable = new Dictionary<UIType, GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 목록을 사전으로 정리하고, 시작 시 모두 꺼둠
        _uiTable.Clear();
        for (int i = 0; i < _uiEntries.Count; i++)
        {
            UIEntry entry = _uiEntries[i];
            if (entry.Panel == null)
                continue;

            _uiTable[entry.Type] = entry.Panel;
            entry.Panel.SetActive(false);
        }
    }

    public void OpenUI(UIType type)
    {
        if (_uiTable.ContainsKey(type))
            _uiTable[type].SetActive(true);
    }

    public void CloseUI(UIType type)
    {
        if (_uiTable.ContainsKey(type))
            _uiTable[type].SetActive(false);
    }
}
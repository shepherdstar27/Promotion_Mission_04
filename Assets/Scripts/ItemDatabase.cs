using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    // JsonUtility가 배열을 직접 못 읽으므로 감싸는 래퍼
    [System.Serializable]
    private class ItemDataListWrapper
    {
        public List<ItemData> Items;
    }

    [SerializeField] private string _jsonFileName = "JsonOutput/Item"; // Resources 기준, 확장자 제외

    private Dictionary<string, ItemData> _itemTable = new Dictionary<string, ItemData>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadItems();
    }

    private void LoadItems()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>(_jsonFileName);
        if (jsonAsset == null)
        {
            Debug.LogError("아이템 JSON을 찾지 못했습니다: " + _jsonFileName);
            return;
        }

        // 최상위 배열을 객체로 감쌈: [ ... ] → { "Items": [ ... ] }
        string wrappedJson = "{ \"Items\": " + jsonAsset.text + " }";

        ItemDataListWrapper wrapper = JsonUtility.FromJson<ItemDataListWrapper>(wrappedJson);
        if (wrapper == null || wrapper.Items == null)
        {
            Debug.LogError("아이템 JSON 파싱에 실패했습니다.");
            return;
        }

        _itemTable.Clear();
        for (int i = 0; i < wrapper.Items.Count; i++)
        {
            ItemData data = wrapper.Items[i];

            if (_itemTable.ContainsKey(data.Id))
            {
                Debug.LogWarning("중복된 아이템 ID: " + data.Id);
                continue;
            }

            _itemTable.Add(data.Id, data);
        }

        Debug.Log("아이템 " + _itemTable.Count + "개를 불러왔습니다.");
    }

    public ItemData GetItemById(string id)
    {
        if (_itemTable.ContainsKey(id))
            return _itemTable[id];

        Debug.LogWarning("해당 ID의 아이템이 없습니다: " + id);
        return null;
    }

    // 전체 아이템 ID 목록 (테스트로 아이템 뿌릴 때 등에 사용)
    public List<string> GetAllItemIds()
    {
        List<string> ids = new List<string>();
        foreach (KeyValuePair<string, ItemData> pair in _itemTable)
        {
            ids.Add(pair.Key);
        }
        return ids;
    }
}
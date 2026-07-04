using System.Collections.Generic;

// 인벤토리 한 칸(슬롯): 어떤 아이템을 몇 개 가지고 있는지
public class InventorySlot
{
    public string ItemId;
    public int Count;

    public InventorySlot(string itemId, int count)
    {
        ItemId = itemId;
        Count = count;
    }
}

public class InventoryViewModel : ViewModelBase
{
    // 보유 슬롯 목록
    private List<InventorySlot> _slots = new List<InventorySlot>();

    // 현재 선택된 슬롯의 인덱스 (-1이면 선택 없음)
    private int _selectedIndex = -1;

    // 외부에서 슬롯 목록을 읽을 수 있게 (읽기 전용으로 넘김)
    public IReadOnlyList<InventorySlot> Slots { get { return _slots; } }

    public int SelectedIndex
    {
        get { return _selectedIndex; }
        private set { SetField(ref _selectedIndex, value, "SelectedIndex"); }
    }

    // 현재 선택된 슬롯 (없으면 null)
    public InventorySlot GetSelectedSlot()
    {
        if (_selectedIndex < 0 || _selectedIndex >= _slots.Count)
            return null;
        return _slots[_selectedIndex];
    }

    // --- 아이템 추가 (획득) ---

    public void AddItem(string itemId, int amount)
    {
        if (string.IsNullOrEmpty(itemId) || amount <= 0)
            return;

        // 이미 같은 아이템 슬롯이 있으면 수량만 늘림 (스택)
        InventorySlot existing = FindSlot(itemId);
        if (existing != null)
        {
            existing.Count = existing.Count + amount;
        }
        else
        {
            // 없으면 새 슬롯 추가
            _slots.Add(new InventorySlot(itemId, amount));
        }

        // 목록이 바뀌었음을 방송
        OnPropertyChanged("Slots");
    }

    // --- 아이템 사용/소모 (수량 감소, 0이면 제거) ---

    public void ConsumeItem(string itemId, int amount)
    {
        if (string.IsNullOrEmpty(itemId) || amount <= 0)
            return;

        InventorySlot slot = FindSlot(itemId);
        if (slot == null)
            return;

        slot.Count = slot.Count - amount;

        if (slot.Count <= 0)
        {
            int removedIndex = _slots.IndexOf(slot);
            _slots.Remove(slot);

            // 선택돼 있던 슬롯이 사라졌거나 인덱스가 밀렸으면 선택 해제
            if (_selectedIndex == removedIndex)
                SelectedIndex = -1;
            else if (_selectedIndex > removedIndex)
                SelectedIndex = _selectedIndex - 1;
        }

        OnPropertyChanged("Slots");
    }

    // --- 슬롯 선택 ---

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= _slots.Count)
        {
            SelectedIndex = -1;
            return;
        }
        SelectedIndex = index;
    }

    // --- 내부 도우미 ---

    private InventorySlot FindSlot(string itemId)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].ItemId == itemId)
                return _slots[i];
        }
        return null;
    }
}
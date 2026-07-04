using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryView : ViewBase
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private InventorySlotView _slotPrefab;

    // 상세 표시 영역
    [SerializeField] private Image _detailIcon;
    [SerializeField] private TMP_Text _detailNameText;
    [SerializeField] private TMP_Text _detailDescText;
    [SerializeField] private Button _useButton;

    private InventoryViewModel _inventoryViewModel;
    private List<InventorySlotView> _slotViews = new List<InventorySlotView>();

    private void OnEnable()
    {
        if (_inventoryViewModel == null)
        {
            InventoryViewModel viewModel = NetworkManager.Instance.GetInventoryViewModel();
            if (viewModel == null)
                return;
            Bind(viewModel);
        }

        // 사용 버튼 연결 (중복 방지 후 등록)
        if (_useButton != null)
        {
            _useButton.onClick.RemoveListener(OnClickUse);
            _useButton.onClick.AddListener(OnClickUse);
        }

        RefreshSlots();
    }

    private void OnDisable()
    {
        if (_useButton != null)
            _useButton.onClick.RemoveListener(OnClickUse);
    }

    protected override void OnBind()
    {
        _inventoryViewModel = ViewModel as InventoryViewModel;
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        if (propertyName == "Slots")
            RefreshSlots();
        else if (propertyName == "SelectedIndex")
            RefreshSelection();
    }

    private void RefreshSlots()
    {
        if (_inventoryViewModel == null)
            return;

        IReadOnlyList<InventorySlot> slots = _inventoryViewModel.Slots;
        EnsureSlotViewCount(slots.Count);

        for (int i = 0; i < _slotViews.Count; i++)
        {
            if (i < slots.Count)
            {
                InventorySlot slot = slots[i];
                ItemData data = ItemDatabase.Instance.GetItemById(slot.ItemId);

                Sprite icon = null;
                if (data != null)
                    icon = Resources.Load<Sprite>(data.IconPath);

                _slotViews[i].gameObject.SetActive(true);
                _slotViews[i].Setup(i, this);
                _slotViews[i].SetData(icon, slot.Count);
            }
            else
            {
                _slotViews[i].gameObject.SetActive(false);
            }
        }

        RefreshSelection();
    }

    private void RefreshSelection()
    {
        if (_inventoryViewModel == null)
            return;

        int selected = _inventoryViewModel.SelectedIndex;

        // 하이라이트 갱신
        for (int i = 0; i < _slotViews.Count; i++)
        {
            if (_slotViews[i].gameObject.activeSelf)
                _slotViews[i].SetSelected(i == selected);
        }

        // 상세 정보 갱신
        RefreshDetail();
    }

    private void RefreshDetail()
    {
        InventorySlot slot = _inventoryViewModel.GetSelectedSlot();

        if (slot == null)
        {
            // 선택된 게 없으면 상세 비우고 사용 버튼 끔
            if (_detailIcon != null) _detailIcon.enabled = false;
            if (_detailNameText != null) _detailNameText.text = "";
            if (_detailDescText != null) _detailDescText.text = "";
            if (_useButton != null) _useButton.interactable = false;
            return;
        }

        ItemData data = ItemDatabase.Instance.GetItemById(slot.ItemId);
        if (data == null)
            return;

        if (_detailNameText != null)
            _detailNameText.text = data.Name;
        if (_detailDescText != null)
            _detailDescText.text = data.Description;
        if (_detailIcon != null)
        {
            Sprite icon = Resources.Load<Sprite>(data.IconPath);
            _detailIcon.sprite = icon;
            _detailIcon.enabled = (icon != null);
        }
        if (_useButton != null)
            _useButton.interactable = true;
    }

    private void EnsureSlotViewCount(int needed)
    {
        while (_slotViews.Count < needed)
        {
            InventorySlotView newSlot = Instantiate(_slotPrefab, _slotParent);
            _slotViews.Add(newSlot);
        }
    }

    public void OnSlotClicked(int index)
    {
        Debug.Log("슬롯 클릭됨: " + index);
        if (_inventoryViewModel != null)
            _inventoryViewModel.SelectSlot(index);
    }

    // 사용 버튼 클릭
    private void OnClickUse()
    {
        InventorySlot slot = _inventoryViewModel.GetSelectedSlot();
        if (slot == null)
            return;

        // 네트워크 매니저를 통해 사용 요청
        NetworkManager.Instance.RequestUseItem(slot.ItemId);
    }
}
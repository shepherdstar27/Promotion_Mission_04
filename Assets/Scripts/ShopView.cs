using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopView : ViewBase
{
    [SerializeField] private Transform _itemParent;
    [SerializeField] private ShopItemView _itemPrefab;
    [SerializeField] private TMP_Text _feedbackText; // 구매 성공/실패 메시지

    private ShopViewModel _shopViewModel;
    private List<ShopItemView> _itemViews = new List<ShopItemView>();

    private void OnEnable()
    {
        if (_shopViewModel == null)
        {
            ShopViewModel viewModel = NetworkManager.Instance.GetShopViewModel();
            if (viewModel == null)
                return;
            Bind(viewModel);
        }

        RefreshItems();
        ClearFeedback();
    }

    protected override void OnBind()
    {
        _shopViewModel = ViewModel as ShopViewModel;
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        if (propertyName == "SaleItemIds")
            RefreshItems();
    }

    private void RefreshItems()
    {
        if (_shopViewModel == null)
            return;

        IReadOnlyList<string> ids = _shopViewModel.SaleItemIds;
        EnsureItemViewCount(ids.Count);

        for (int i = 0; i < _itemViews.Count; i++)
        {
            if (i < ids.Count)
            {
                _itemViews[i].gameObject.SetActive(true);
                _itemViews[i].Setup(ids[i], this);
            }
            else
            {
                _itemViews[i].gameObject.SetActive(false);
            }
        }
    }

    private void EnsureItemViewCount(int needed)
    {
        while (_itemViews.Count < needed)
        {
            ShopItemView newItem = Instantiate(_itemPrefab, _itemParent);
            _itemViews.Add(newItem);
        }
    }

    // 상품 슬롯의 구매 버튼이 이걸 부름
    public void OnBuyClicked(string itemId)
    {
        PurchaseResult result = NetworkManager.Instance.RequestBuyItem(itemId);
        ShowFeedback(result, itemId);
    }

    private void ShowFeedback(PurchaseResult result, string itemId)
    {
        if (_feedbackText == null)
            return;

        ItemData data = ItemDatabase.Instance.GetItemById(itemId);
        string itemName = (data != null) ? data.Name : itemId;

        if (result == PurchaseResult.Success)
            _feedbackText.text = itemName + " 구매 완료!";
        else if (result == PurchaseResult.NotEnoughGold)
            _feedbackText.text = "재화가 부족합니다.";
        else
            _feedbackText.text = "구매할 수 없습니다.";
    }

    private void ClearFeedback()
    {
        if (_feedbackText != null)
            _feedbackText.text = "";
    }
}
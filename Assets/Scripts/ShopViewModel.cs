using System.Collections.Generic;

// 구매 결과
public enum PurchaseResult
{
    Success,        // 구매 성공
    NotEnoughGold,  // 재화 부족
    InvalidItem,    // 잘못된 아이템
}

public class ShopViewModel : ViewModelBase
{
    // 판매하는 아이템 ID 목록
    private List<string> _saleItemIds = new List<string>();

    public IReadOnlyList<string> SaleItemIds { get { return _saleItemIds; } }

    private PurchaseResult _lastResult;
    public PurchaseResult LastResult { get { return _lastResult; } }

    // 판매 목록
    public void SetSaleItems(List<string> itemIds)
    {
        _saleItemIds.Clear();
        if (itemIds != null)
        {
            for (int i = 0; i < itemIds.Count; i++)
                _saleItemIds.Add(itemIds[i]);
        }

        OnPropertyChanged("SaleItemIds");
    }
}
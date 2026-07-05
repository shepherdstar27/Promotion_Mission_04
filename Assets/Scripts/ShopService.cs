using System.Collections.Generic;

public class ShopService
{
    private ShopViewModel _shopViewModel;

    public ShopViewModel CreateShop(List<string> saleItemIds)
    {
        if (_shopViewModel != null)
            return _shopViewModel;

        _shopViewModel = new ShopViewModel();
        _shopViewModel.SetSaleItems(saleItemIds);
        return _shopViewModel;
    }

    public ShopViewModel GetShopViewModel()
    {
        return _shopViewModel;
    }
}
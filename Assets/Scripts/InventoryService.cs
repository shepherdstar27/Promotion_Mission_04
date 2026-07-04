public class InventoryService
{
    private InventoryViewModel _inventoryViewModel;

    public InventoryViewModel CreateInventory()
    {
        if (_inventoryViewModel != null)
            return _inventoryViewModel;

        _inventoryViewModel = new InventoryViewModel();
        return _inventoryViewModel;
    }

    public InventoryViewModel GetInventoryViewModel()
    {
        return _inventoryViewModel;
    }
}
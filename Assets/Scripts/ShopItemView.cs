using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;

    private string _itemId;
    private ShopView _owner;

    public void Setup(string itemId, ShopView owner)
    {
        _itemId = itemId;
        _owner = owner;

        if (_buyButton != null)
        {
            _buyButton.onClick.RemoveListener(OnClickBuy);
            _buyButton.onClick.AddListener(OnClickBuy);
        }

        RefreshData();
    }

    private void RefreshData()
    {
        ItemData data = ItemDatabase.Instance.GetItemById(_itemId);
        if (data == null)
            return;

        if (_nameText != null)
            _nameText.text = data.Name;

        if (_priceText != null)
            _priceText.text = data.Price + " G";

        if (_iconImage != null)
        {
            Sprite icon = Resources.Load<Sprite>(data.IconPath);
            _iconImage.sprite = icon;
            _iconImage.enabled = (icon != null);
        }
    }

    private void OnClickBuy()
    {
        if (_owner != null)
            _owner.OnBuyClicked(_itemId);
    }
}
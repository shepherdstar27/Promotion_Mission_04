using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private GameObject _highlight; // 선택 표시용 (테두리 등)
    [SerializeField] private Button _button;

    private int _slotIndex;      // 이 슬롯이 목록에서 몇 번째인지
    private InventoryView _owner; // 클릭을 전달할 부모

    // 부모가 이 슬롯을 세팅할 때 호출
    public void Setup(int slotIndex, InventoryView owner)
    {
        _slotIndex = slotIndex;
        _owner = owner;
        Debug.Log("[Setup] 슬롯 " + slotIndex + " 세팅됨, button=" + (_button != null));

        if (_button != null)
        {
            _button.onClick.RemoveListener(OnClickSlot); // 중복 방지
            _button.onClick.AddListener(OnClickSlot);
        }
    }

    // 아이콘·수량을 채움
    public void SetData(Sprite icon, int count)
    {
        if (_iconImage != null)
        {
            _iconImage.sprite = icon;
            _iconImage.enabled = true;

        }

        if (_countText != null)
        {
            // 수량이 1개면 숫자를 안 보이게, 2개 이상이면 표시 
            if (count > 1)
                _countText.text = count.ToString();
            else
                _countText.text = "";
        }
    }

    // 선택 여부에 따라 하이라이트 켜고 끔
    public void SetSelected(bool selected)
    {
        if (_highlight != null)
            _highlight.SetActive(selected);
    }

    private void OnClickSlot()
    {
        Debug.Log("OnClickSlot 호출됨, index=" + _slotIndex + ", owner=" + (_owner != null));
        if (_owner != null)
            _owner.OnSlotClicked(_slotIndex);
    }
}
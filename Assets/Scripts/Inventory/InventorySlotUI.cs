using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(CanvasGroup))] // 드래그 시 레이캐스트 제어를 위해 필요
public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler
{
    [Header("Slot Display Graphics")]
    [SerializeField] private Image Image_ItemIcon;
    [SerializeField] private TextMeshProUGUI TextMesh_Price;
    [SerializeField] private TextMeshProUGUI TextMesh_StackCount;

    [Header("Live Assigned Slot Asset")]
    [SerializeField] private string _currentSlotItemId = "";

    // --- 드래그 앤 드롭을 위한 추가 변수 ---
    private Vector3 _originalPosition;
    private Transform _originalParent;
    private CanvasGroup CanvasGroup_Slot;

    private void Awake()
    {
        // 유니티 참조 컴포넌트 캐싱
        CanvasGroup_Slot = GetComponent<CanvasGroup>();
        if (CanvasGroup_Slot == null)
        {
            CanvasGroup_Slot = gameObject.AddComponent<CanvasGroup>();
        }
    }


    public void ClearSlotGraphic()
    {
        _currentSlotItemId = "";

        if (Image_ItemIcon != null)
        {
            Image_ItemIcon.sprite = null;
            Image_ItemIcon.enabled = false;
        }
        if (TextMesh_StackCount != null)
        {
            TextMesh_StackCount.enabled = false;
        }
        if (TextMesh_Price != null)
        {
            TextMesh_Price.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (string.IsNullOrEmpty(_currentSlotItemId) == true) return;

        if (InventoryUI.Instance != null && InventoryUI.Instance.GetTooltipUI() != null)
        {
            InventoryUI.Instance.GetTooltipUI().RenderItemTooltip(_currentSlotItemId);
        }
    }




}
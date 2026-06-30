using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : UIBase
{
    private static InventoryUI _instance;
    public static InventoryUI Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    [Header("UI Resource References")]
    [SerializeField] private GameObject GameObject_SlotUiPrefab;       // 장착할 InventorySlotUI 프리팹
    [SerializeField] private Transform Transform_SlotGridContainer;    // LayoutGroup 컴포넌트가 붙은 그리드 부모 트랜스폼

    [Header("Left Tooltip UI Object Link")]
    [SerializeField] private ItemTooltipUI Component_LeftTooltipUI;    // 좌측 아이템 상세 정보창 컴포넌트 주소

    [Header("Special Resource Texts")]
    [SerializeField] private TextMeshProUGUI TextMesh_GoldAmount;
    [SerializeField] private TextMeshProUGUI TextMesh_FuelAmount;
    [SerializeField] private TextMeshProUGUI TextMesh_SuppliesAmount;

    [Header("Close Buttons")]
    [SerializeField] private UIButton Button_Close;

    // 규칙 반영: 일반 멤버 변수는 _소문자로 시작
    private List<InventorySlotUI> _createdSlotScripts = new List<InventorySlotUI>();

    private void Awake()
    {
        Instance = this;
        BindEvents();
    }

    private void OnEnable()
    {

    }

    public int GetCreatedSlotCount()
    {
        return _createdSlotScripts.Count;
    }

    private void BindEvents()
    {
        if (Button_Close != null)
        {
            Button_Close.BindOnClickButtonEvent(OnClick_Close);
        }
    }

    private void OnClick_Close()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }


    }

    // 좌측 툴팁에 손쉽게 교차 접근을 허용하기 위한 중간 외교 게이트웨이 함수
    public ItemTooltipUI GetTooltipUI()
    {
        return Component_LeftTooltipUI;
    }

    // [슬롯 실물 작도]: 배 스펙 칸 수에 맞추어 격자 화면 UI 기물을 실시간 생성합니다.
    public void CreateUIContainerSlots(int totalCount)
    {
        // 기존에 잔존하던 레이아웃 박스 요소 일제 청소 루프
        for (int i = 0; i < _createdSlotScripts.Count; i++)
        {
            if (_createdSlotScripts[i] != null)
            {
                Destroy(_createdSlotScripts[i].gameObject);
            }
        }
        _createdSlotScripts.Clear();

        // 새로운 슬롯 인스턴스 일제 생성 주입
        for (int i = 0; i < totalCount; i++)
        {
            GameObject spawnSlotObj = Instantiate(GameObject_SlotUiPrefab, Transform_SlotGridContainer);
            InventorySlotUI slotScript = spawnSlotObj.GetComponent<InventorySlotUI>();

            if (slotScript != null)
            {
                _createdSlotScripts.Add(slotScript);
                slotScript.ClearSlotGraphic(); // 순정 공백 정돈
            }
        }
    }



    // [특수 재화 실시간 수치 인쇄부]
    public void UpdateSpecialResourceText(int gold, int fuel, int supplies)
    {
        if (TextMesh_GoldAmount != null) TextMesh_GoldAmount.text = gold.ToString();
        if (TextMesh_FuelAmount != null) TextMesh_FuelAmount.text = fuel.ToString();
        if (TextMesh_SuppliesAmount != null) TextMesh_SuppliesAmount.text = supplies.ToString();
    }
}
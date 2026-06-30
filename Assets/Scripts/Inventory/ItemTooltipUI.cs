using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTooltipUI : MonoBehaviour
{
    [Header("Tooltip Panel Frame View")]
    [SerializeField] private GameObject GameObject_TooltipMasterFrame; // 좌측 정보창 총괄 캔버스 덩어리 오브젝트

    [Header("Tooltip Detail Renderer Elements")]
    [SerializeField] private Image Image_BigItemIcon;
    [SerializeField] private TextMeshProUGUI TextMesh_ItemName;
    [SerializeField] private TextMeshProUGUI TextMesh_ItemDescription;
    [SerializeField] private TextMeshProUGUI TextMesh_ItemPrice;



    //  슬롯에서 호출 시 프레임을 켜고 JSON 정보를 추출 매핑합니다.
    public void RenderItemTooltip(string itemId)
    {

    }


}
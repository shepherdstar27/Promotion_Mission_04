using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private string _itemId = "Item_0001"; // 이 오브젝트가 주는 아이템 ID
    [SerializeField] private int _amount = 1;              // 획득 수량
    [SerializeField] private KeyCode _pickupKey = KeyCode.E;

    private bool _playerInRange = false; // 플레이어가 범위 안에 있는지

    private void Update()
    {
        // 범위 안이고 E를 누르면 획득
        if (_playerInRange && Input.GetKeyDown(_pickupKey))
            Pickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;

        _playerInRange = true;
        Debug.Log(_itemId + " 획득 가능 (E)");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == false)
            return;

        _playerInRange = false;
    }

    private void Pickup()
    {
        // 네트워크 매니저를 통해 인벤토리에 추가 요청
        NetworkManager.Instance.RequestAddItem(_itemId, _amount);

        Debug.Log(_itemId + " 획득!");

        // 주운 아이템 오브젝트는 사라짐
        Destroy(gameObject);
    }
}
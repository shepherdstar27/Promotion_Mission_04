using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    // 어디서든 접근할 수 있게 하는 단순 싱글톤
    public static NetworkManager Instance { get; private set; }

    // 순수 C# 서비스를 품는다.
    private PlayerService _playerService;
    private InventoryService _inventoryService;

    [SerializeField] private int _healAmountPerItem = 30; // 아이템 1개 사용 시 회복량

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerService = new PlayerService();
        _inventoryService = new InventoryService();
    }

    // 로컬 플레이어 생성 요청을 받는 창구
    public void RequestCreateLocalPlayer(string playerName)
    {
        // 1) 서비스에게 ViewModel 생성을 맡김
        PlayerViewModel viewModel = _playerService.CreateLocalPlayer(playerName);

        // 2) ViewModel이 정상 생성되었으면, 화면(View) 생성을 게임오브젝트 매니저에 요청
        if (viewModel != null)
        {
            GameObjectManager.Instance.SpawnLocalPlayer(viewModel);
        }
    }

    // 다른 곳(프로필 UI, 테스트 UI)에서 로컬 플레이어 ViewModel을 받아갈 창구
    public PlayerViewModel GetLocalPlayerViewModel()
    {
        return _playerService.GetLocalPlayerViewModel();
    }


    // 로컬 플레이어 Exp 추가 요청
    public void RequestAddPlayerExp(int amount)
    {
        PlayerViewModel viewModel = _playerService.GetLocalPlayerViewModel();
        if (viewModel == null)
        {
            Debug.LogWarning("로컬 플레이어가 아직 없습니다.");
            return;
        }

        viewModel.AddExp(amount);
    }

    // 로컬 플레이어 이름 변경 요청
    public void RequestChangePlayerName(string newName)
    {
        PlayerViewModel viewModel = _playerService.GetLocalPlayerViewModel();
        if (viewModel == null)
        {
            Debug.LogWarning("로컬 플레이어가 아직 없습니다.");
            return;
        }

        viewModel.ChangeName(newName); 
    }


    // 인벤토리 생성 요청
    public void RequestCreateInventory()
    {
        _inventoryService.CreateInventory();
    }

    // 인벤토리 ViewModel 조회
    public InventoryViewModel GetInventoryViewModel()
    {
        return _inventoryService.GetInventoryViewModel();
    }

    // 아이템 획득 요청
    public void RequestAddItem(string itemId, int amount)
    {
        InventoryViewModel viewModel = _inventoryService.GetInventoryViewModel();
        if (viewModel == null)
        {
            Debug.LogWarning("인벤토리가 아직 없습니다.");
            return;
        }
        viewModel.AddItem(itemId, amount);
    }

    public void RequestUseItem(string itemId)
    {
        InventoryViewModel inventory = _inventoryService.GetInventoryViewModel();
        PlayerViewModel player = _playerService.GetLocalPlayerViewModel();

        if (inventory == null || player == null)
        {
            Debug.LogWarning("인벤토리 또는 플레이어가 없습니다.");
            return;
        }

        // 아이템 데이터를 찾아 효과 종류를 확인
        ItemData data = ItemDatabase.Instance.GetItemById(itemId);
        if (data == null)
            return;

        // 사용 불가 아이템은 여기서 중단 (소모도 안 함)
        if (data.UseType == "Unusable")
        {
            Debug.Log(data.Name + " : 사용할 수 없는 아이템입니다.");
            return;
        }

        // 효과 종류에 따라 처리
        if (data.UseType == "Heal")
        {
            player.Heal(data.UseValue); // JSON에 적힌 수치만큼 회복
            Debug.Log(data.Name + " 사용 → HP +" + data.UseValue);
        }
        else if (data.UseType == "None")
        {
            // 효과는 없지만 소모는 됨 (예: 그냥 먹으면 사라지는 아이템)
            Debug.Log(data.Name + " 사용 (효과 없음)");
        }
        else if (data.UseType == "TakeDamage")
        {
            // TakeDamage 포션
            player.TakeDamage(data.UseValue); // JSON에 적힌 수치만큼 데미지
            Debug.Log(data.Name + " 사용 → Damage " + data.UseValue);
        }

        // 효과 적용 후 아이템 1개 소모
        inventory.ConsumeItem(itemId, 1);
    }




}
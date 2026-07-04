using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public static GameObjectManager Instance { get; private set; }

    // 인스펙터에서 연결할 로컬 플레이어 프리팹 (ViewBase를 가진 프리팹)
    [SerializeField] private ViewBase _localPlayerPrefab;
    [SerializeField] private Transform _spawnPoint; // 생성 위치 (없으면 원점)

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // 플레이어 프리팹을 씬에 만들고, 넘겨받은 ViewModel과 바인딩한다.
    public void SpawnLocalPlayer(PlayerViewModel viewModel)
    {
        if (_localPlayerPrefab == null)
        {
            Debug.LogError("로컬 플레이어 프리팹이 연결되지 않았습니다.");
            return;
        }

        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        if (_spawnPoint != null)
        {
            position = _spawnPoint.position;
            rotation = _spawnPoint.rotation;
        }

        // 프리팹을 씬에 생성
        ViewBase viewInstance = Instantiate(_localPlayerPrefab, position, rotation);

        // 생성된 View에 ViewModel을 넘겨 연결(바인딩)
        viewInstance.Bind(viewModel);

        Debug.Log("로컬 플레이어 생성 및 바인딩 완료");
    }
}
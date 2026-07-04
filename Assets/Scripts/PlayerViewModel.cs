public class PlayerViewModel : ViewModelBase
{
    private string _playerName;
    private int _level;
    private int _exp;

    private const int ExpPerLevel = 100;

    // --- 외부에서 읽고 쓰는 속성들 ---

    public string PlayerName
    {
        get { return _playerName; }
        set { SetField(ref _playerName, value, "PlayerName"); }
    }

    public int Level
    {
        get { return _level; }
        private set { SetField(ref _level, value, "Level"); }
    }

    public int Exp
    {
        get { return _exp; }
        private set { SetField(ref _exp, value, "Exp"); }
    }

    // 레벨업까지 필요한 Exp를 화면에 보여주기 위한 읽기 전용 값
    public int ExpToNextLevel { get { return ExpPerLevel; } }

    // --- 생성자: 초기값 세팅 ---

    public PlayerViewModel(string playerName)
    {
        _playerName = playerName;
        _level = 1;
        _exp = 0;
    }

    // --- 바깥(서비스)에서 호출할 기능들 ---

    // Exp를 더하고, 넘치면 레벨업 처리
    public void AddExp(int amount)
    {
        if (amount <= 0)
            return;

        int newExp = _exp + amount;

        // 넘친 만큼 레벨을 올림 (여러 레벨 한 번에 오를 수도 있음)
        while (newExp >= ExpPerLevel)
        {
            newExp = newExp - ExpPerLevel;
            Level = _level + 1; // 속성 set을 통해 방송됨
        }

        Exp = newExp; // 속성 set을 통해 방송됨
    }

    // 이름 변경
    public void ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            return;

        PlayerName = newName; // 속성 set을 통해 방송됨
    }
}
public class PlayerViewModel : ViewModelBase
{
    private string _playerName;
    private int _level;
    private int _exp;
    private int _maxHp;
    private int _currentHp;
    private int _gold;

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

    public int ExpToNextLevel 
    { 
        get { return ExpPerLevel; } 
    }

    public int MaxHp
    {
        get { return _maxHp; }
        private set { SetField(ref _maxHp, value, "MaxHp"); }
    }

    public int CurrentHp
    {
        get { return _currentHp; }
        private set { SetField(ref _currentHp, value, "CurrentHp"); }
    }

    public bool IsDead 
    { 
        get { return _currentHp <= 0; } 
    }

    public int Gold
    {
        get { return _gold; }
        private set { SetField(ref _gold, value, "Gold"); }
    }




    // --- 초기값 세팅 ---
    public PlayerViewModel(string playerName)
    {
        _playerName = playerName;
        _level = 1;
        _exp = 0;
        _maxHp = 100;
        _currentHp = 10;
        _gold = 99999;
    }


    // --- 바깥(서비스)에서 호출할 기능 ---
    // Exp를 더하고, 넘치면 레벨업 처리
    public void AddExp(int amount)
    {
        if (amount <= 0)
            return;

        int newExp = _exp + amount;

        // 레벨 증가
        while (newExp >= ExpPerLevel)
        {
            newExp = newExp - ExpPerLevel;
            Level = _level + 1; 
        }

        Exp = newExp;
    }

    // 이름 변경
    public void ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            return;

        PlayerName = newName;
    }

    // HP 감소
    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;
        if (IsDead)
            return;

        int newHp = _currentHp - amount;
        if (newHp < 0)
            newHp = 0;

        CurrentHp = newHp; // 속성 set을 통해 방송됨
    }

    // HP 증가
    public void Heal(int amount)
    {
        if (amount <= 0)
            return;
        if (IsDead)
            return;

        int newHp = _currentHp + amount;
        if (newHp > _maxHp)
            newHp = _maxHp;

        CurrentHp = newHp;
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
            return;

        Gold = _gold + amount; 
    }

    public bool TrySpendGold(int amount)
    {
        if (amount <= 0)
            return false;

        // 재화가 부족하면 차감하지 않고 실패 반환 (음수 방지)
        if (_gold < amount)
            return false;

        Gold = _gold - amount; // 속성 set을 통해 방송됨
        return true;
    }


}
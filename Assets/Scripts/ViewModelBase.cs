using System.Collections.Generic;
using System.ComponentModel;

// 모든 ViewModel의 부모. 값이 바뀌면 "어떤 속성이 바뀌었는지" 이름을 실어 방송한다.
public abstract class ViewModelBase : INotifyPropertyChanged
{
    // 방송용 이벤트. 이것 하나로 모든 속성 변경을 알린다.
    public event PropertyChangedEventHandler PropertyChanged;

    // 자식 클래스가 값 변경 후 이 함수를 불러 방송한다.
    protected void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // 값이 실제로 달라졌을 때만 바꾸고 방송한다. 바뀌었으면 true를 돌려준다.
    protected bool SetField<T>(ref T field, T newValue, string propertyName)
    {
        // 기존 값과 새 값이 같으면 아무것도 안 함 (불필요한 방송 방지)
        if (EqualityComparer<T>.Default.Equals(field, newValue))
            return false;

        field = newValue;
        OnPropertyChanged(propertyName);
        return true;
    }
}
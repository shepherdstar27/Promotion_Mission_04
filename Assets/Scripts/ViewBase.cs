using System.ComponentModel;
using UnityEngine;

// 모든 View의 부모. ViewModel을 받아 구독하고, 값이 바뀌면 화면을 갱신한다.
public abstract class ViewBase : MonoBehaviour
{
    private ViewModelBase _viewModel;

    // 지금 붙잡고 있는 ViewModel을 외부에서 읽을 수 있게 함
    protected ViewModelBase ViewModel { get { return _viewModel; } }

    // ViewModel을 넘겨받아 연결(바인딩)한다.
    public void Bind(ViewModelBase viewModel)
    {
        // 이미 다른 ViewModel을 붙잡고 있었다면 먼저 구독을 끊는다 (중복 구독 방지)
        Unbind();

        _viewModel = viewModel;
        if (_viewModel == null)
            return;

        // ViewModel의 방송을 구독한다. 값이 바뀌면 HandlePropertyChanged가 불린다.
        _viewModel.PropertyChanged += HandlePropertyChanged;

        // 연결 직후, 현재 값으로 화면을 한 번 맞춘다.
        OnBind();
    }

    // 연결을 끊는다. 구독을 해제해 메모리 누수와 중복 호출을 막는다.
    public void Unbind()
    {
        if (_viewModel == null)
            return;

        _viewModel.PropertyChanged -= HandlePropertyChanged;
        _viewModel = null;
    }

    // 방송이 올 때마다 불리는 함수. 자식 View가 구체적으로 무엇을 갱신할지 정한다.
    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        OnPropertyChanged(args.PropertyName);
    }

    protected virtual void OnDestroy()
    {
        Unbind();
    }

    // --- 자식 View가 채워야 할 부분 ---

    // 바인딩 직후 한 번 호출. 초기 화면 세팅용.
    protected abstract void OnBind();

    // 어떤 속성이 바뀌었을 때 화면의 무엇을 갱신할지 자식이 구현.
    protected abstract void OnPropertyChanged(string propertyName);
}
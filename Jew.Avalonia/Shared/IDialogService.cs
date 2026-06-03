namespace Jew.Avalonia.Shared;

public interface IDialogService
{
    void Show<TWindow>();
    TResult? Show<TWindow, TResult>();
}
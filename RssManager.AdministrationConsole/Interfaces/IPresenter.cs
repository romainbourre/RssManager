namespace RssManager.AdministrationConsole.Interfaces;

internal interface IPresenter<in T>
{
    void Present(T data);
}

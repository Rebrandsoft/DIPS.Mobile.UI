namespace DIPS.Mobile.UI.Components.ContextMenus;

public interface IContextMenuItem : IDisposable
{
    bool IsVisible { get; }
}
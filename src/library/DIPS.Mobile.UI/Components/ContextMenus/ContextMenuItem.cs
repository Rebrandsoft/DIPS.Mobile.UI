namespace DIPS.Mobile.UI.Components.ContextMenus;

/// <summary>
/// A context menu item to use in a context menu
/// </summary>
public partial class ContextMenuItem : Element, IContextMenuItem
{
    internal void SendClicked(ContextMenu contextMenu)
    {
        contextMenu.SendClicked(this);
        Command?.Execute(CommandParameter);
        DidClick?.Invoke(this, EventArgs.Empty);
        ContextMenuEffect.ContextMenuItemClickedCallback?.Invoke(this);
    }
}
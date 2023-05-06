namespace DIPS.Mobile.UI.Components.Pickers
{
    public class SelectableListItem
    {
        public SelectableListItem(string displayName, bool isSelected)
        {
            DisplayName = displayName;
            IsSelected = isSelected;
        }

        public string DisplayName { get;}
        public bool IsSelected { get; }
    }
}
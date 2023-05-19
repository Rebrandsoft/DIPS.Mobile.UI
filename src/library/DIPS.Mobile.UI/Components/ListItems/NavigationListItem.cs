using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Resources.Icons;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class NavigationListItem : ListItem
{
    public NavigationListItem()
    {
        var checkMark = new Image()
        {
            Source = IconLookup.GetIcon(IconName.arrow_right_s_line)
        };

        ContentItem = checkMark;

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, new Binding(nameof(Command), source: this));

        GestureRecognizers.Add(tapGestureRecognizer);
    }
    
    
    
}
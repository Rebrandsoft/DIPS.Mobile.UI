using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;

public partial class SubtitleOptions : BindableObject, IListItemOptions
{
    public void Bind(ListItem listItem)
    {
        SetBinding(BindingContextProperty, new Binding(source: listItem, path:nameof(BindingContext)));
        
        if(listItem.SubtitleLabel is null)
            return;            
        
        listItem.SubtitleLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        listItem.SubtitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        listItem.SubtitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        listItem.SubtitleLabel.FontSize = Sizes.GetSize(SizeName.size_3);
        listItem.SubtitleLabel.TextColor = Colors.GetColor(ColorName.color_neutral_60);

    }
}
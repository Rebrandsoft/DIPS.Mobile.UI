using DIPS.Mobile.UI.Components.Content;
using DIPS.Mobile.UI.Components.Dividers;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(ContentItem))]
public partial class ListItem : ContentView
{
    protected Grid MainContent { get; }

    private VerticalStackLayout RootContent { get; } =
        new() { BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent };

    public Border Border { get; } = new() {BackgroundColor = Colors.GetColor(ColorName.color_system_white)};
    
    public ListItem()
    {
        Border.StrokeShape = new RoundRectangle 
        { 
            CornerRadius = CornerRadius, 
            StrokeThickness = 0 
        };
        
        MainContent = new Grid 
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Star),
                new(GridLength.Auto)
            },
            RowDefinitions = new RowDefinitionCollection()
            {
                new(GridLength.Auto)
            },
            Padding = new Thickness(Sizes.GetSize(SizeName.size_4), 
                Sizes.GetSize(SizeName.size_3),
                Sizes.GetSize(SizeName.size_4),
                Sizes.GetSize(SizeName.size_3))
        };

        Border.Content = MainContent;

        RootContent.Add(Border);
        
        Content = RootContent;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        AddLabel();

#if __ANDROID__
        // To remove margin around border, will be fixed: https://github.com/dotnet/maui/pull/14402
        Border.StrokeThickness = 0;
#endif

        if (HasTopDivider)
        {
            AddDivider(0);
        }

        if (HasBottomDivider)
        {
            AddDivider(RootContent.Count);
        }
    }

    private void AddLabel()
    {
        var label = new Label 
        { 
            FontSize = Sizes.GetSize(SizeName.size_4),
            TextColor = Colors.GetColor(ColorName.color_neutral_90),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Start
        };

        if (string.IsNullOrEmpty(Subtitle))
        {
            label.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
        }
        else
        {
            var title = new Span();
            title.SetBinding(Span.TextProperty, new Binding(nameof(Title), source: this));

            var newLine = new Span { Text = Environment.NewLine };

            var subTitle = new Span { FontSize = Sizes.GetSize(SizeName.size_3), TextColor = Colors.GetColor(ColorName.color_neutral_60)};
            subTitle.SetBinding(Span.TextProperty, new Binding(nameof(Subtitle), source: this));
                
            label.FormattedText = new FormattedString { Spans = { title, newLine, subTitle } };
        }

        label.Margin = new Thickness(0, 0, 10, 0);
        
        MainContent.Add(label);
    }

    private static void CornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem)
            return;

        listItem.Border.StrokeShape = new RoundRectangle { CornerRadius = (CornerRadius)newValue };
    }

    private static void OnContentItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is not ListItem listItem || newValue is not View view)
            return;

        if (listItem.ShouldOverrideContentItemLayoutOptions)
        {
            view.HorizontalOptions = LayoutOptions.End;
            view.VerticalOptions = LayoutOptions.Center;
        }
        
        listItem.MainContent.Add(view, 1);
    }

    private void AddDivider(int row)
    {
        var divider = new Divider();
        if (row > RootContent.Count - 1)
        {
            RootContent.Add(divider);
        }
        else
        {
            RootContent.Insert(row, divider);
        }
    }

}
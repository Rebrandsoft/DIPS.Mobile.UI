namespace DIPS.Mobile.UI.Components.ListItems.Options.Icon;

public partial class Options
{
    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }
    
    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    
    public LayoutOptions VerticalOptions
    {
        get => (LayoutOptions)GetValue(VerticalOptionsProperty);
        set => SetValue(VerticalOptionsProperty, value);
    }

    public Thickness Margin
    {
        get => (Thickness)GetValue(MarginProperty);
        set => SetValue(MarginProperty, value);
    }

    public static readonly BindableProperty MarginProperty = BindableProperty.Create(
        nameof(Margin),
        typeof(Thickness),
        typeof(Options),
        defaultValue: new Thickness(0, 0, Sizes.GetSize(SizeName.size_4), 0));
    
    public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create(
        nameof(VerticalOptions),
        typeof(LayoutOptions),
        typeof(Options),
        defaultValue: LayoutOptions.Center);
    
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(
        nameof(Color),
        typeof(Color),
        typeof(Options));

    public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible),
        typeof(bool),
        typeof(Options),
        defaultValue: true);
}
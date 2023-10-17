using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.ContextMenus;

public partial class ContextMenuItem
{
    
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
        nameof(IsChecked),
        typeof(bool),
        typeof(ContextMenuItem));
    
    /// <summary>
    /// <see cref="Command"/>
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ContextMenuItem));

    /// <summary>
    /// <see cref="CommandParameter"/>
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter),
        typeof(object),
        typeof(ContextMenuItem));
    
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(ContextMenuItem));
    
    public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible),
        typeof(bool),
        typeof(ContextMenuItem),
        defaultValue: true);
    
    /// <summary>
    /// The command to run when the item was clicked
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// The command parameter to send to the command when the item was clicked
    /// </summary>
    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Determines if the <see cref="ContextMenuItem"/> is visible in the context menu
    /// </summary>
    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }
    
    /// <summary>
    /// The clicked event when the item was clicked
    /// </summary>
    public event EventHandler? DidClick;
    
    /// <summary>
    /// The title of the context menu item
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Determines if the native check mark should be added to the item when its tapped
    /// </summary>
    public bool IsCheckable { get; set; }

    /// <summary>
    /// Determines if the item should be checked when its opened
    /// </summary>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }
    
    /// <summary>
    /// The parent of the context menu item
    /// </summary>
    public object? Parent { get; internal set; }

    /// <summary>
    /// <see cref="iOSContextMenuItemOptions"/>
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public iOSContextMenuItemOptions iOSOptions { get; set; } = new();
    
    /// <summary>
    /// <see cref="AndroidContextMenuItemOptions"/>
    /// </summary>
    public AndroidContextMenuItemOptions AndroidOptions { get; set; } = new();
    
    /// <summary>
    /// The icon to be used as a image with the context menu item
    /// </summary>
    public ImageSource? Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}

/// <summary>
/// The Android specific context menu item options
/// </summary>
public class AndroidContextMenuItemOptions
{
    /// <summary>
    /// Set this to override the Context menu item icon with a Android Resource  
    /// </summary>
    /// <remarks>This can be any resource in your Resources drawable, but you can also check out Android.Resource.Drawable.icon-name which is built in</remarks>
    public string IconResourceName { get; set; } = string.Empty;
}

/// <summary>
/// The iOS specific context menu item options
/// </summary>
public class iOSContextMenuItemOptions
{
    /// <summary>
    /// Set this to override the Context menu item icon with a SF Symbol 
    /// </summary>
    /// <remarks>To see all SF Symbols go to https://developer.apple.com/sf-symbols/</remarks>
    public string SystemIconName { get; set; } = string.Empty;
}
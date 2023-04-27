using CoreGraphics;
using DIPS.Mobile.UI.Extensions.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Searching.iOS;

internal class InternalSearchBarHandler : SearchBarHandler
{
    private InternalSearchBar m_searchBar;
    
    public InternalSearchBarHandler() : base(PropertyMapper)
    {
        ActivityIndicatorView = new UIActivityIndicatorView();
    }
    
    public static readonly IPropertyMapper<InternalSearchBar, InternalSearchBarHandler> PropertyMapper = new PropertyMapper<InternalSearchBar, InternalSearchBarHandler>(ViewMapper)
    {
        [nameof(InternalSearchBar.ShowsCancelButton)] = MapShowsCancelButton,
        [nameof(InternalSearchBar.CornerRadius)] = MapCornerRadius,
        [nameof(InternalSearchBar.IconsColor)] = MapIconsColor,
        [nameof(InternalSearchBar.IsBusy)] = MapIsBusy
    };

    private UIActivityIndicatorView ActivityIndicatorView { get; }

    private UIImageView MagnifierIcon { get; set; } 


    private static void MapIsBusy(InternalSearchBarHandler internalSearchBarHandler, InternalSearchBar internalSearchBar)
    {
        if (!internalSearchBar.HasBusyIndication)
            return;

        if (internalSearchBar.IsBusy)
        {
            internalSearchBarHandler.ActivityIndicatorView.Color = internalSearchBar.IconsColor.ToPlatform();
            internalSearchBarHandler.ActivityIndicatorView.StartAnimating();
                
            if (internalSearchBarHandler.PlatformView.SearchTextField.LeftView is not UIImageView uiImageView) //Magnifier icon on the left
                return;                    
                
            var leftViewSize = uiImageView.Frame.Size;
            internalSearchBarHandler.ActivityIndicatorView.Center = new CGPoint(x:
                leftViewSize.Width / 2, y: leftViewSize.Height / 2);
            internalSearchBarHandler.PlatformView.SearchTextField.LeftView = internalSearchBarHandler.ActivityIndicatorView;
        }
        else
        {
           internalSearchBarHandler.ActivityIndicatorView.RemoveFromSuperview();
           internalSearchBarHandler.PlatformView.SearchTextField.LeftView = internalSearchBarHandler.MagnifierIcon;
        }
    }

    private static void MapIconsColor(InternalSearchBarHandler internalSearchBarHandler, InternalSearchBar internalSearchBar)
    {
        if (internalSearchBarHandler.PlatformView.SearchTextField.LeftView is not UIImageView uiImageView) //Magnifier icon on the left
            return;
        
        uiImageView.TintColor = internalSearchBar.IconsColor.ToPlatform();
    }

    private static void MapCornerRadius(InternalSearchBarHandler internalSearchBarHandler, InternalSearchBar internalSearchBar)
    {
        //internalSearchBarHandler.PlatformView.AddCornerRadius(internalSearchBar.CornerRadius, internalSearchBar.BackgroundColor);
    }

    private static void MapShowsCancelButton(InternalSearchBarHandler internalSearchBarHandler, InternalSearchBar internalSearchBar)
    {
        internalSearchBarHandler.PlatformView.ShowsCancelButton = internalSearchBar.ShowsCancelButton;
        
        if(!internalSearchBar.ShowsCancelButton) 
            return;
        
        var cancelButton = internalSearchBarHandler.PlatformView.FindChildView<UIButton>();
        if(cancelButton == null)
            return;
        
        cancelButton.Enabled = true;
    }


    protected override MauiSearchBar CreatePlatformView() => new MauiSearchBar();

    protected override void ConnectHandler(MauiSearchBar platformView)
    {
        base.ConnectHandler(platformView);
        
        m_searchBar = (VirtualView as InternalSearchBar)!;
        
        platformView.SearchBarStyle = UISearchBarStyle.Minimal;

        if (platformView.SearchTextField.LeftView is UIImageView uiImageView) //Magnifier icon on the left
        {
            MagnifierIcon = uiImageView;
        }
        
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        PlatformView.CancelButtonClicked += OnCancelButtonTouchDown;
    }

    private void UnSubscribeToEvents()
    {
        PlatformView.CancelButtonClicked -= OnCancelButtonTouchDown;
    }

    private void OnCancelButtonTouchDown(object? sender, EventArgs e)
    {
        m_searchBar.SendCancelTapped();
    }

    protected override void DisconnectHandler(MauiSearchBar platformView)
    {
        base.DisconnectHandler(platformView);
        
        UnSubscribeToEvents();
    }
}
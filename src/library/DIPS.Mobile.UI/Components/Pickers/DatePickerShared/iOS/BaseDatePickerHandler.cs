using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Platforms.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public abstract class BaseDatePickerHandler : ViewHandler<IDatePicker, DUIDatePicker>
{
    private bool m_isOpen;
    
    protected BaseDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }
    
    public static readonly IPropertyMapper<IDatePicker, BaseDatePickerHandler> BasePropertyMapper = new PropertyMapper<IDatePicker, BaseDatePickerHandler>(ViewMapper)
    {
        [nameof(View.HorizontalOptions)] = MapOverrideHorizontalOptions
    };

    private static void MapOverrideHorizontalOptions(BaseDatePickerHandler handler, IDatePicker dateAndTimePicker)
    {
        handler.PlatformView.SetHorizontalAlignment((dateAndTimePicker as View)!);
    }

    protected abstract override DUIDatePicker CreatePlatformView();

    protected override void ConnectHandler(DUIDatePicker platformView)
    {
        base.ConnectHandler(platformView);

        platformView.ValueChanged += OnValueChanged;
        platformView.EditingDidBegin += OnOpen;
        platformView.EditingDidEnd += OnClose;
        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
        
        if(PlatformView.Mode is UIDatePickerMode.Date or UIDatePickerMode.Time)
            ((VirtualView as View)!).WidthRequest = PlatformView.Mode switch
            {
                UIDatePickerMode.Date => 121,
                UIDatePickerMode.Time => 70
            };
        
        platformView.TintColor = Colors.GetColor(ColorName.color_primary_90).ToPlatform();
    }

    protected abstract void OnValueChanged(object? sender, EventArgs e);

    private void OnOpen(object? sender, EventArgs e)
    {
        m_isOpen = true;
    }

    private void OnClose(object? sender, EventArgs e)
    {
        m_isOpen = false;
    }

    private void TryClose()
    {
        if (!m_isOpen)
            return;

        var currentPresentedUiViewController = Platform.GetCurrentUIViewController();
        currentPresentedUiViewController?.DismissViewController(false, null);
    }

    protected override void DisconnectHandler(DUIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.DisposeLayer();
        platformView.EditingDidBegin -= OnOpen;
        platformView.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}
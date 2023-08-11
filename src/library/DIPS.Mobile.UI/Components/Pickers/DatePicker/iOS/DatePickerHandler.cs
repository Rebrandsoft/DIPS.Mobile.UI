using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : ViewHandler<DatePicker, UIDatePicker>
{
    private bool m_isOpen;

    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Compact, Mode = UIDatePickerMode.Date};
    }

    public List<UIView> getSubviewsOfView(UIView view)
    {
        var subviewArray = new List<UIView>();
        if (view.Subviews.Length == 0)
        {
            return subviewArray;
        }

        foreach (var subview in view.Subviews)
        {
            subviewArray.AddRange(getSubviewsOfView(subview));
        }

        return subviewArray;
    }
    protected override void ConnectHandler(UIDatePicker platformView)
    {
        base.ConnectHandler(platformView);
        platformView.PrintAllChildrenOfView();
        // var uiView = platformView.Subviews.First().Subviews[0]; //<-- 0 her gir oss nesten riktig bakgrunnsfarge
        // uiView.TintColor = UIColor.Green;
        // uiView.BackgroundColor = UIColor.Red;
        platformView.SetDefaultTintColor();

        // platformView.Subviews[0].Subviews[0].Subviews[0].Alpha = 0;
     

        
        
        platformView.ValueChanged += OnDateSelected;
        platformView.EditingDidBegin += OnOpen;
        platformView.EditingDidEnd += OnClose;
        
        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
    }

    private static void ClearBackgroundColorOfSubview(UIView uiView)
    {
        foreach (var subview in uiView.Subviews)
        {
            subview.Alpha = 0;
            if (subview.Subviews.Length > 0)
            {
                ClearBackgroundColorOfSubview(subview);
            }
        }
    }

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

    private partial void AppendPropertyMapper()
    {
        DatePickerPropertyMapper.Add(nameof(DatePicker.HorizontalOptions), MapHorizontalOptions);
        DatePickerPropertyMapper.Add(nameof(DatePicker.Background), MapOverrideBackground);
        DatePickerPropertyMapper.Add(nameof(DatePicker.IgnoreLocalTime), MapIgnoreLocalTime);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MinimumDate), MapMinimumDate);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MaximumDate), MapMaximumDate);
    }

    private static void MapMaximumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MaximumDate is null or null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)datePicker.MaximumDate).ConvertDate();
    }

    private static void MapMinimumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MinimumDate is null or null)
            return;

        handler.PlatformView.MinimumDate = ((DateTime)datePicker.MinimumDate).ConvertDate();
    }

    private void MapOverrideBackground(DatePickerHandler handler, DatePicker datePicker)
    {
    }

    private static void MapHorizontalOptions(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetHorizontalAlignment(datePicker);
    }

    private void OnDateSelected(object? sender, EventArgs e)
    {
        var timeZone = PlatformView.TimeZone;
        if (timeZone == null)
        {
            timeZone = NSTimeZone.LocalTimeZone;
        }
        if (DateTime.TryParse(
                new NSDateFormatter() {DateFormat = "yyyy-MM-dd", TimeZone = timeZone}.StringFor(
                    PlatformView.Date),
                out var selectedDate))
        {
            VirtualView.SelectedDate = selectedDate;
        }

        VirtualView.SelectedDateCommand?.Execute(null);
    }

    private static void MapIgnoreLocalTime(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    public static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetDate(datePicker.SelectedDate.ConvertDate(), true);
    }

    protected override void DisconnectHandler(UIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.ValueChanged -= OnDateSelected;
        platformView.EditingDidBegin -= OnOpen;
        platformView.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}
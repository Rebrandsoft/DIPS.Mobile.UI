namespace DIPS.Mobile.UI.Components.Pickers.NullableDatePickerShared;

public abstract class BaseNullableDatePicker : Grid
{
    protected readonly Switch DateEnabledSwitch;
    
#nullable disable
    private View m_dateOrTimePicker;
#nullable enable

    protected BaseNullableDatePicker()
    {
        ColumnSpacing = Sizes.GetSize(SizeName.size_3);
        
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        AddColumnDefinition(new ColumnDefinition(GridLength.Auto));

        DateEnabledSwitch = new Switch { VerticalOptions = LayoutOptions.Center };
        DateEnabledSwitch.Toggled += OnSwitchToggled;
        
        this.Add(DateEnabledSwitch, 1);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        m_dateOrTimePicker = CreateDateOrTimePicker();
        m_dateOrTimePicker.IsVisible = false;
        m_dateOrTimePicker.VerticalOptions = LayoutOptions.Center;
#if __IOS__
        m_dateOrTimePicker.SetBinding(HorizontalOptionsProperty, new Binding(nameof(HorizontalOptions), BindingMode.OneWay, source: this));
#endif
        
        Add(m_dateOrTimePicker);
        var size = m_dateOrTimePicker.Measure(int.MaxValue, int.MaxValue);
        MinimumHeightRequest = size.Request.Height;
        
        if (!IsDateOrTimeNull())
        {
            DateEnabledSwitch.IsToggled = true;
        }
    }

    protected abstract bool IsDateOrTimeNull();
    protected abstract View CreateDateOrTimePicker();
    protected virtual void OnSwitchToggled(object? sender, ToggledEventArgs e)
    {
        if (m_dateOrTimePicker is null)
            return;
        
        m_dateOrTimePicker.IsVisible = e.Value;
    }
}
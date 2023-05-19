using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker
{
    public static readonly BindableProperty SelectedDateTimeProperty = BindableProperty.Create(
        nameof(SelectedDateTime),
        typeof(DateTime),
        typeof(DateAndTimePicker), defaultBindingMode:BindingMode.TwoWay);

    /// <summary>
    /// The date people selected from the date picker.
    /// </summary>
    public DateTime SelectedDateTime
    {
        get => (DateTime)GetValue(SelectedDateTimeProperty);
        set => SetValue(SelectedDateTimeProperty, value);
    }

    public static readonly BindableProperty SelectedDateTimeCommandProperty = BindableProperty.Create(
        nameof(SelectedDateTimeCommand),
        typeof(ICommand),
        typeof(DateAndTimePicker));


    /// <summary>
    /// The command to be executed when people selected a date from the date picker.
    /// </summary>
    public ICommand? SelectedDateTimeCommand
    {
        get => (ICommand)GetValue(SelectedDateTimeCommandProperty);
        set => SetValue(SelectedDateTimeCommandProperty, value);
    }
    

    public static readonly BindableProperty IgnoreLocalTimeProperty = BindableProperty.Create(
        nameof(IgnoreLocalTime),
        typeof(bool),
        typeof(DateAndTimePicker));

    public bool IgnoreLocalTime
    {
        get => (bool)GetValue(IgnoreLocalTimeProperty);
        set => SetValue(IgnoreLocalTimeProperty, value);
    }
    
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
        nameof(MinimumDate),
        typeof(DateTime?),
        typeof(DateAndTimePicker));

    /// <summary>
    /// Sets the minimum date the DatePicker can choose
    /// </summary>
    public DateTime? MinimumDate
    {
        get => (DateTime?)GetValue(MinimumDateProperty);
        set => SetValue(MinimumDateProperty, value);
    }
        
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
        nameof(MaximumDate),
        typeof(DateTime?),
        typeof(DateAndTimePicker));

    /// <summary>
    /// Sets the maximum date the DatePicker can choose
    /// </summary>
    public DateTime? MaximumDate
    {
        get => (DateTime?)GetValue(MaximumDateProperty);
        set => SetValue(MaximumDateProperty, value);
    }
    
}
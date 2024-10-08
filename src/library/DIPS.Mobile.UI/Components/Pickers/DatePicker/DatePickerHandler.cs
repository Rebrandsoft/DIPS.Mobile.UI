namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler
{
    public DatePickerHandler() : base(PropertyMapper)
    {
    }
    
    public DatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

#if __IOS__
    public static readonly IPropertyMapper<DatePicker, DatePickerHandler> PropertyMapper =
        new PropertyMapper<DatePicker, DatePickerHandler>(BasePropertyMapper)
        {
            [nameof(DatePicker.SelectedDate)] = MapSelectedDate,
            [nameof(DatePicker.IgnoreLocalTime)] = MapIgnoreLocalTime,
            [nameof(DatePicker.MaximumDate)] = MapMaximumDate,
            [nameof(DatePicker.MinimumDate)] = MapMinimumDate
        };
#else
    public static readonly IPropertyMapper<DatePicker, DatePickerHandler> PropertyMapper = new PropertyMapper<DatePicker, DatePickerHandler>(ViewMapper)
    {
        [nameof(DatePicker.SelectedDate)] = MapSelectedDate,
    };
#endif

#if __IOS__
    private static partial void MapMinimumDate(DatePickerHandler handler, DatePicker datePicker);
    private static partial void MapMaximumDate(DatePickerHandler handler, DatePicker datePicker);
    private static partial void MapIgnoreLocalTime(DatePickerHandler handler, DatePicker datePicker);
#endif
    
    private static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker);
}
using Android.Content;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.Platforms.Android;
using Google.Android.Material.DatePicker;
using Microsoft.Maui.Platform;
using Object = Java.Lang.Object;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;

public class MaterialDatePickerFragment : Object, IMaterialDateTimePickerFragment, IMaterialPickerOnPositiveButtonClickListener
{
    private readonly DatePicker m_datePicker;
    private readonly MaterialDatePicker m_materialDatePicker;

    public MaterialDatePickerFragment(DatePicker datePicker)
    {
        m_datePicker = datePicker;
        
        var builder = MaterialDatePicker.Builder.DatePicker();
        SetDatePickerSelection(builder);

        var calendarConstraints = new CalendarConstraints.Builder();

        if (datePicker.MinimumDate != null)
            calendarConstraints.SetStart(datePicker.MinimumDate.Value.ToLong());
        if (datePicker.MaximumDate != null)
            calendarConstraints.SetEnd(datePicker.MaximumDate.Value.ToLong());
        
        m_materialDatePicker = builder.SetCalendarConstraints(calendarConstraints.Build()).Build();
        m_materialDatePicker.AddOnPositiveButtonClickListener(this);

        var fragmentManager = Platform.CurrentActivity!.GetFragmentManager();
        m_materialDatePicker.Show(fragmentManager!, DatePickerService.DatePickerTag);
    }

    private void SetDatePickerSelection(MaterialDatePicker.Builder builder)
    {
        var date = m_datePicker.IgnoreLocalTime ? m_datePicker.SelectedDate : m_datePicker.SelectedDate.ToLocalTime();

        //Java uses the unix epoch, so we have find the total milliseconds from the date people have picked and the UnixEpoch start.
        builder.SetSelection(date.ToLong());
    }

    public bool IsOpen()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(DatePickerService.DatePickerTag);
        return fragment is MaterialDatePicker;
    }

    public void Close()
    {
        var fragment = Platform.CurrentActivity?.GetFragmentManager()?.FindFragmentByTag(DatePickerService.DatePickerTag);
        if (fragment is MaterialDatePicker datePickerFragment)
        {
            datePickerFragment.Dismiss();
        }
    }


    public void OnPositiveButtonClick(Object? p0)
    {
        if (long.TryParse(m_materialDatePicker.Selection?.ToString(), out var milliseconds))
        {
            //Java uses the unix epoch, so we have to create a csharp date time based on the UnixEpoch start with the milliseconds picked by people using the date picker.
            var dateFromJava = DateTime.UnixEpoch.AddMilliseconds(milliseconds);
            m_datePicker.SelectedDate = m_datePicker.IgnoreLocalTime ? dateFromJava : dateFromJava.ToLocalTime();
            m_datePicker.SelectedDateCommand?.Execute(null);
        }
    }
}
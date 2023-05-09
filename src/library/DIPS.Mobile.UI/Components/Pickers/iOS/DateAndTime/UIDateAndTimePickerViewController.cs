using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.iOS.DateTime;

public class UIDateAndTimePickerViewController : UIDateTimePickerViewController
{
    private readonly DateAndTimePicker m_dateAndTimePicker;

    public UIDateAndTimePickerViewController(IDateTimePicker dateTimePicker) : base(dateTimePicker)
    {
        m_dateAndTimePicker = (dateTimePicker as DateAndTimePicker)!;
    }

    protected override void SetDateTimePickerMode(UIDatePicker uiDatePicker)
    {
        uiDatePicker.Mode = UIDatePickerMode.DateAndTime;
    }

    protected override void SetDateTime(UIDatePicker uiDatePicker)
    {
        uiDatePicker.SetDate(m_dateAndTimePicker.SelectedDateTime, m_dateAndTimePicker);
    }

    protected override void OnFinished(UIDatePicker uiDatePicker)
    {
        m_dateAndTimePicker.SelectedDateTime = (System.DateTime)uiDatePicker.Date;
    }
}
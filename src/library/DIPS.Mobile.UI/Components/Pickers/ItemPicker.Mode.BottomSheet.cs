using DIPS.Mobile.UI.Components.BottomSheets;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class ItemPicker
    {
        private void AttachBottomSheet()
        {
            GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = (new Command(() => _ = BottomSheetService.Current.OpenBottomSheet(new PickerBottomSheet(this))))
            });
        }
    }
}
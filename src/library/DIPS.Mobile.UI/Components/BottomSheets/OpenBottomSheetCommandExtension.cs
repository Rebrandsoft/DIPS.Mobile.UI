using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    [ContentProperty(nameof(BottomSheetType))]
    public class OpenBottomSheetCommandExtension : IMarkupExtension<ICommand>
    {
        public Type? BottomSheetType { get; set; }

        public ICommand ProvideValue(IServiceProvider serviceProvider)
        {
            return new Command(() =>
            {
                if (BottomSheetType == null)
                {
                    return;
                }

                var activatedObject = Activator.CreateInstance(BottomSheetType);
                if (activatedObject is BottomSheet theBottomSheet)
                {
                    BottomSheetService.Current.OpenBottomSheet(theBottomSheet);
                }
            });
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
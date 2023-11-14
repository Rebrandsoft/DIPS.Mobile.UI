using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pages
{
    public partial class ContentPage : Microsoft.Maui.Controls.ContentPage
    {
        public static readonly ColorName BackgroundColorName = ColorName.color_neutral_10;

        public ContentPage()
        {
            if (Application.Current == null)
            {
                return;
            }

            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged +=
                OnRequestedThemeChanged; //Can not use AppThemeBindings because that makes the navigation page bar background flash on Android, so we listen to changes and set the color our self
        }

        private void SetColors(AppTheme osAppTheme)
        {
            this.SetAppThemeColor(BackgroundColorProperty, BackgroundColorName);
            
            Microsoft.Maui.Controls.Shell.SetBackgroundColor(this, Colors.GetColor(Shell.Shell.ToolbarBackgroundColorName, osAppTheme));
            Microsoft.Maui.Controls.Shell.SetTitleColor(this, Colors.GetColor(Shell.Shell.ToolbarTitleTextColorName, osAppTheme));
            Microsoft.Maui.Controls.Shell.SetForegroundColor(this, Colors.GetColor(Shell.Shell.ToolbarTitleTextColorName, osAppTheme));
        }
        
        
        private void OnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            SetColors(e.RequestedTheme);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (Application.Current != null)
            {
                Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
            }

        }

    }
}
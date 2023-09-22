namespace DIPS.Mobile.UI.Resources.Styles.Button
{
    public static class ButtonDefaultStyle
    {
        public static Style Current =>
            new(typeof(Components.Buttons.Button)) { 
                Setters =
                {
                    new Setter
                    {
                        Property = Microsoft.Maui.Controls.Button.FontSizeProperty,
                        Value = Sizes.Sizes.GetSize(SizeName.size_4)
                    }
                } 
            };
    }
}
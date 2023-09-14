namespace DIPS.Mobile.UI.Resources.Styles.Chip;

public static class EmptyInputStyle
{
    public static Style Current =>
        new(typeof(Components.Chips.Chip))
        {
            Setters =
            {
                new Setter()
                {
                    Property = Components.Chips.Chip.CornerRadiusProperty,
                    Value = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.ColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_system_white)
                },
                new Setter
                {
                    Property = Components.Chips.Chip.BorderWidthProperty,
                    Value = 1.0,
                },
                new Setter
                {
                    Property = Components.Chips.Chip.BorderColorProperty,
                    Value = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(
                        ColorName.color_neutral_30)
                }
            }
        };
}
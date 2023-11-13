namespace DIPS.Mobile.UI.Resources.Styles.Label;

public class LabelTypeStyle
{
    internal static Style SectionHeader => new(typeof(Components.Labels.Label))
    {
        BasedOn = LabelFontFamilyStyle.UI.BasedOn = LabelWeightStyle.ThreeHundred,
        Setters =
        {
            new Setter
            {
                Property = Microsoft.Maui.Controls.Label.TextColorProperty,
                Value = Colors.Colors.GetColor(ColorName.color_neutral_80)
            }
        }
    };
}
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Chip;

namespace DIPS.Mobile.UI.Resources.Styles
{
    public class StylesExtension : IMarkupExtension<Style>
    {
        /// <summary>
        /// The <see cref="ChipStyle"/> to look for.
        /// </summary>
        public ChipStyle Chip { get; set; }
        
        /// <summary>
        /// The <see cref="ButtonStyle"/> to look for.
        /// </summary>
        public ButtonStyle Button { get; set; }

        public Style ProvideValue(IServiceProvider serviceProvider)
        {
            if (Chip != ChipStyle.None)
            {
                return ChipStyleResources.Styles.TryGetValue(Chip, out var chipStyle) ? chipStyle : new Style(typeof(View));
            }

            if(Button != ButtonStyle.None)
            {
                return ButtonStyleResources.Styles.TryGetValue(Button, out var buttonStyle)
                    ? buttonStyle
                    : new Style(typeof(View));
            }

            return new Style(typeof(View));
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Style>).ProvideValue(serviceProvider);
        }
    }
}
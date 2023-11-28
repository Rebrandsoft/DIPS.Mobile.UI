using Android.Content;
using Android.Graphics;
using Android.Text.Style;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Kotlin.Text;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Labels.Android;

public class MauiTextView : Microsoft.Maui.Platform.MauiTextView
{
    private readonly Label m_label;

    public MauiTextView(Context context, Label label) : base(context)
    {
        m_label = label;
    }

    protected override void OnDraw(Canvas? canvas)
    {
        base.OnDraw(canvas);
        
        var text = "";
        if (m_label.Text is null)
        {
            if (m_label.FormattedText is not null)
                text = m_label.FormattedText.ToString();
        }
        else
        {
            text = m_label.Text;
        }

        m_label.IsTruncated = Layout?.Text != text;
    }

}
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Kotlin.Text;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Labels.Android;

public class MauiTextView : Microsoft.Maui.Platform.MauiTextView
{
    private readonly Label m_label;
    private readonly SpannableString m_newEllipsis;

    public MauiTextView(Context context, Label label) : base(context)
    {
        m_label = label;
    }

    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        
        var availableViewWidth = MeasuredWidth - (float)CompoundPaddingLeft - CompoundPaddingRight;
        var availableTextWidth = availableViewWidth * MaxLines;

        var ellipsizedText = TextUtils.Ellipsize(m_label.Text, Paint, availableTextWidth, Ellipsize);

        if (ellipsizedText != m_label.Text)
        {
            m_label.IsEllipsized = true;
        }
        else
        {
            m_label.IsEllipsized = false;
        }

    }

}
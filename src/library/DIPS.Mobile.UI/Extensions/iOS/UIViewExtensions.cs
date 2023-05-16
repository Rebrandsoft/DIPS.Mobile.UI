using CoreAnimation;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Extensions.iOS
{
    public static class UIViewExtensions
    {
        public static T? FindChildView<T>(this UIView view) where T : UIView
        {
            var queue = new Queue<UIView>();
            queue.Enqueue(view);

            while (queue.Count > 0)
            {
                var descendantView = queue.Dequeue();

                if (descendantView is T result)
                    return result;

                for (var i = 0; i < descendantView.Subviews?.Length; i++)
                    queue.Enqueue(descendantView.Subviews[i]);
            }

            return null;
        }

        public static string PrintAllChildrenOfView(this UIView view, int depth = 0)
        {
            var tabs = "";
            for (int i = 0; i < depth; i++)
            {
                tabs += "\t";
            }

            var returnString = $"\n{tabs}{view.Class.Name}";

            depth++;
            foreach (var subView in view.Subviews)
            {
                returnString += PrintAllChildrenOfView(subView, depth);
            }

            return returnString;
        }


        public static T? FindParentViewOfType<T>(this UIView? view)
            where T : class
        {
            if (view is T t)
                return t;

            while (view != null)
            {
                if (view.Superview is T parent)
                    return parent;

                view = view.Superview;
            }

            return null;
        }
        
        public static UIView? FindParentThatMatches(this UIView? view, Func<UIView, bool> predicate)
        {
            if (view is { } t)
            {
                if (predicate(t))
                {
                    return t;
                }
            }

            while (view != null)
            {
                if (view.Superview is { } parent)
                {
                    if (predicate(parent))
                    {
                        return parent;
                    }
                }

                view = view.Superview;
            }

            return null;
        }
    }
}
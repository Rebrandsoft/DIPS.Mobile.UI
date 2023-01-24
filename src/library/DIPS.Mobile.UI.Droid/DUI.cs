using System;
using Android.Content;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Mobile.UI.Droid
{
    public class DUI
    {
        private static Context s_context;

        /// <summary>
        /// Gets the <see cref="Context"/>.
        /// </summary>
        internal static Context Context
        {
            get
            {
                var page = Application.Current.MainPage ??
                           throw new NullReferenceException($"{nameof(Application.MainPage)} cannot be null");
                var renderer = page.GetRenderer();

                if (renderer?.View.Context is not null)
                    s_context = renderer.View.Context;

                return renderer?.View.Context ??
                       s_context ?? throw new NullReferenceException($"{nameof(Context)} cannot be null");
            }
        }

        /// <summary>
        /// Return a resource identifier for the given resource name. A fully qualified resource name is of the form "package:type/entry". The first two components (package and type) are optional if defType and defPackage, respectively, are specified here.
        /// </summary>
        /// <param name="name">The name of the desired resource.</param>
        /// <param name="defType">Optional default resource type to find, if "type/" is not included in the name. Can be null to require an explicit type.</param>
        /// <param name="defPackage">Optional default package to find, if "package:" is not included in the name. Can be null to require an explicit package.</param>
        /// <returns></returns>
        /// <remarks>Taken from here https://developer.android.com/reference/android/content/res/Resources#getIdentifier(java.lang.String,%20java.lang.String,%20java.lang.String)</remarks>
        internal static int? GetResourceId(string name, string? defType = null, string? defPackage = null)
        {
            return Context.Resources?.GetIdentifier(name, defType, Context.PackageName);
        }

        public static void Init() { }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Components.Resources.LocalizedStrings {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LocalizedStrings {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalizedStrings() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Components.Resources.LocalizedStrings.LocalizedStrings", typeof(LocalizedStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string BottomSheet {
            get {
                return ResourceManager.GetString("BottomSheet", resourceCulture);
            }
        }
        
        internal static string Components {
            get {
                return ResourceManager.GetString("Components", resourceCulture);
            }
        }
        
        internal static string Colors {
            get {
                return ResourceManager.GetString("Colors", resourceCulture);
            }
        }
        
        internal static string Resources {
            get {
                return ResourceManager.GetString("Resources", resourceCulture);
            }
        }
        
        internal static string Sizes {
            get {
                return ResourceManager.GetString("Sizes", resourceCulture);
            }
        }
        
        internal static string Sizes_description {
            get {
                return ResourceManager.GetString("Sizes_description", resourceCulture);
            }
        }
        
        internal static string ContextMenu {
            get {
                return ResourceManager.GetString("ContextMenu", resourceCulture);
            }
        }
    }
}

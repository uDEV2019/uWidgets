﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Calendar.Locales {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Locale {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Locale() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Calendar.Locales.Locale", typeof(Locale).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Calendar.
        /// </summary>
        public static string Calendar {
            get {
                return ResourceManager.GetString("Calendar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track the current date.
        /// </summary>
        public static string Calendar_Date_Subtitle {
            get {
                return ResourceManager.GetString("Calendar_Date_Subtitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date.
        /// </summary>
        public static string Calendar_Date_Title {
            get {
                return ResourceManager.GetString("Calendar_Date_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to First day of week.
        /// </summary>
        public static string Calendar_FirstDayOfWeek {
            get {
                return ResourceManager.GetString("Calendar_FirstDayOfWeek", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track the day of the month.
        /// </summary>
        public static string Calendar_Month_Subtitle {
            get {
                return ResourceManager.GetString("Calendar_Month_Subtitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Month.
        /// </summary>
        public static string Calendar_Month_Title {
            get {
                return ResourceManager.GetString("Calendar_Month_Title", resourceCulture);
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BW.Website.Resource {
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
    public class GlobalResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GlobalResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BW.Website.Resource.GlobalResource", typeof(GlobalResource).Assembly);
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
        ///   Looks up a localized string similar to Do not match with Password.
        /// </summary>
        public static string validComaprePass {
            get {
                return ResourceManager.GetString("validComaprePass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirn Password is required..
        /// </summary>
        public static string validConfirmPassRequire {
            get {
                return ResourceManager.GetString("validConfirmPassRequire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid email address..
        /// </summary>
        public static string validEmailRegex {
            get {
                return ResourceManager.GetString("validEmailRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is required..
        /// </summary>
        public static string validEmailRequire {
            get {
                return ResourceManager.GetString("validEmailRequire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password must have least 6 word..
        /// </summary>
        public static string validPassMinLenght {
            get {
                return ResourceManager.GetString("validPassMinLenght", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password is required..
        /// </summary>
        public static string validPassRequire {
            get {
                return ResourceManager.GetString("validPassRequire", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username must from 6 to 12 word..
        /// </summary>
        public static string validUsernameLenght {
            get {
                return ResourceManager.GetString("validUsernameLenght", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username must not contain special character..
        /// </summary>
        public static string validUsernameRegex {
            get {
                return ResourceManager.GetString("validUsernameRegex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username is required..
        /// </summary>
        public static string validUsernameRequire {
            get {
                return ResourceManager.GetString("validUsernameRequire", resourceCulture);
            }
        }
    }
}

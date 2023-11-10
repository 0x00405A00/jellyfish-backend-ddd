using MobileApp.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Attribute
{
    [System.AttributeUsage(AttributeTargets.Property| AttributeTargets.Field, AllowMultiple =false,Inherited =false)]
    public class PropertyUiDisplayTextAttribute : System.Attribute
    {

        public string DisplayName { get; private set; }
        public string EntryPlaceHolderValue { get; private set; }
        public bool ProtectedTextEntry { get; private set; }
        public bool IsReadonly{ get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName">User Interface value descriptor for label</param>
        /// <param name="readonlyInUi">Readonly input field in ui</param>
        public PropertyUiDisplayTextAttribute(string displayName, bool readonlyInUi = false)
        {

            DisplayName = displayName;
            IsReadonly = readonlyInUi;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName">User Interface value descriptor for label</param>
        /// <param name="readonlyInUi">Readonly input field in ui</param>
        public PropertyUiDisplayTextAttribute(string displayName,string entryPlaceholderValue, bool protectText = false, bool readonlyInUi = false)
        {

            DisplayName = displayName;
            IsReadonly = readonlyInUi;
            EntryPlaceHolderValue = entryPlaceholderValue;  
            ProtectedTextEntry = protectText;
        }

    }
}

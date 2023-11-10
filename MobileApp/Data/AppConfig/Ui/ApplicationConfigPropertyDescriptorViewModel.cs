using CommunityToolkit.Maui.Markup;
using MobileApp.Validation;
using MobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.Data.AppConfig.Ui
{
    public class ApplicationConfigPropertyDescriptorViewModel : BaseViewModel
    {
        private ValidatableObject _value = null;
        private string _displayName = string.Empty;
        private string _entryPlaceholderValue = string.Empty;
        private PropertyInfo _propertyInfo;
        private Type _propertyType;
        private Type _propertyConfigType;
        private bool _isReadOnly;
        private bool _isProtectedTextEntry;

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string EntryPlaceholderValue
        {
            get
            {
                return _entryPlaceholderValue;
            }
            set
            {
                _entryPlaceholderValue = value;
                OnPropertyChanged(nameof(EntryPlaceholderValue));
            }
        }
        public PropertyInfo PropertyInfo
        {
            get
            {
                return _propertyInfo;
            }
            set
            {
                _propertyInfo = value;
                OnPropertyChanged(nameof(PropertyInfo));
            }
        }
        public Type PropertyType
        {
            get
            {
                return _propertyType;
            }
            set
            {
                _propertyType = value;
                OnPropertyChanged(nameof(PropertyType));
            }
        }

        public Type PropertyConfigType
        {
            get
            {
                return _propertyConfigType;
            }
            set
            {
                _propertyConfigType = value;
                OnPropertyChanged(nameof(PropertyConfigType));
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }
        public bool IsProtectedTextEntry
        {
            get
            {
                return _isProtectedTextEntry;
            }
            set
            {
                _isProtectedTextEntry = value;
                OnPropertyChanged(nameof(IsProtectedTextEntry));
            }
        }
        public ValidatableObject Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        /// <summary>
        /// Tmp Value for Calc
        /// </summary>
        public object CalcTmpValue { get; set; }

        public ObservableCollection<object> EnumValues { get; set; } = null;
    }

}

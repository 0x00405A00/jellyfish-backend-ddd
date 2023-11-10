using MobileApp.Attribute;
using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.Data.AppConfig.Ui;
using MobileApp.Validation;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModel.SettingsSubPage
{
    public abstract class AbstractConfigViewModel<T>
        where T : AbstractApplicationConfig, new()
    {
        public T Config { get; private set; } 
        public AbstractConfigViewModel() : this(null)
        {
            
        }
        public AbstractConfigViewModel(T config)
        {
            Init(config);
        }
        private void Init(T config)
        {

            Config = (config == null ? new T() : config);
            MapConfigDataWithDisplayData();
            AddValidations();
        }
        public void SetDefaults()
        {
            Config.SetDefaults();
            MapConfigDataWithDisplayData();
        }
        public abstract void MapConfigDataWithDisplayData();
        public abstract void Safe();
        public abstract void AddValidations();

        public ObservableCollection<ApplicationConfigPropertyDescriptorViewModel> GetInstanceValuesWithUiDisplayNames(MobileApp.Validation.ValidatableObject.ChangedData changedData)
        {
            ObservableCollection<ApplicationConfigPropertyDescriptorViewModel> propValues = new ObservableCollection<ApplicationConfigPropertyDescriptorViewModel>();
            var props = this.GetType().GetProperties();
            if (props.Length > 0)
            {
                foreach (var prop in props)
                {
                    bool isPub = prop.GetAccessors()?.ToList().Find(x => x.IsPublic) != null;

                    if (prop.CanRead && isPub)
                    {
                        var att = prop.GetCustomAttribute<PropertyUiDisplayTextAttribute>();
                        if (att != null)
                        {
                            string propUiDisplayName = att.DisplayName;
                            ValidatableObject currentValue = (ValidatableObject)prop.GetValue(this, null);

                            Type valueType = currentValue.GetType().GetProperties().ToList().FindAll(x=> x.Name == nameof(currentValue.Value)).First().PropertyType;
                            ObservableCollection<object> determinedEnumValues = null;
                            if (valueType.IsEnum)
                            {
                                var enumValues = Enum.GetValues(valueType);
                                determinedEnumValues = new ObservableCollection<object>();
                                foreach (var enumValue in enumValues)
                                {
                                    determinedEnumValues.Add(enumValue);
                                }
                            }
                            currentValue.ChangedDataHandler = changedData;
                            propValues.Add(new ApplicationConfigPropertyDescriptorViewModel
                            {
                                DisplayName = propUiDisplayName,
                                PropertyInfo = prop,
                                PropertyConfigType = this.GetType(),
                                PropertyType = valueType,
                                IsReadOnly = att.IsReadonly,
                                Value = currentValue,
                                EnumValues = determinedEnumValues,
                                EntryPlaceholderValue = att.EntryPlaceHolderValue,
                                IsProtectedTextEntry = att.ProtectedTextEntry,
                            }) ;
                        }
                    }
                }

            }
            return propValues;
        }
    }

}

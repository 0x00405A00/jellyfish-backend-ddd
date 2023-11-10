using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.Data.AppConfig.Ui;
using MobileApp.Handler.AppConfig;
using MobileApp.Service;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MobileApp.Validation;
using MobileApp.Controls;

namespace MobileApp.ViewModel.SettingsSubPage
{
    public class SettingsPageGenericViewModel<T2,T> : AbstractSettingsPageGenericViewModel
        where T2 : AbstractApplicationConfig, new()
        where T : AbstractConfigViewModel<T2>
    {
        private readonly AbstractConfigViewModel<T2> _configViewModel;
        private readonly ApplicationConfigHandler _applicationConfigHandler;
        public bool IsDataChanged { get; set; }
        
        public SettingsPageGenericViewModel(string pageTitle, NavigationService navigationService, ApplicationConfigHandler applicationConfigHandler, AbstractConfigViewModel<T2> configViewModel) : base(pageTitle) 
        {
            _applicationConfigHandler = applicationConfigHandler;
            _configViewModel = configViewModel;

            var propVals = _configViewModel.GetInstanceValuesWithUiDisplayNames(DataChangedEvent);
            PropertyValues = propVals;
            OnPropertyChanged(nameof(PropertyValues));


            SaveConfigCommand = new RelayCommand(SaveConfigAction);
            RestoreDefaultsConfigCommand = new RelayCommand(RestoreDefaultsConfigAction);
        }
        
        public async void SaveConfigAction()
        {
            bool validate = Validate();
            if(!validate)
            {

                NotificationHandler.ToastNotify("Errors occured, check input fields");
                return;
            }
            var allCurrentConfigContextProps = PropertyValues.ToList().FindAll(x => x.PropertyConfigType == typeof(T));
            if(allCurrentConfigContextProps != null)
            {
                foreach (var item in allCurrentConfigContextProps)
                {
                    TypeConverter typeConverter = new TypeConverter();
                    object tmp = null;
                    if(item.Value != null && item.Value.GetType() != item.PropertyInfo.PropertyType)
                    {
                        var converter = TypeDescriptor.GetConverter(item.PropertyType); 

                        tmp = converter.ConvertTo(item.Value,item.PropertyType);
                    }
                    else
                    {
                        tmp = item.Value;
                    }
                    try
                    {
                        item.PropertyInfo.SetValue(_configViewModel, tmp);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
            _configViewModel.Safe();
            var configProps = _applicationConfigHandler.ApplicationConfig.GetType().GetProperties();    
            foreach(var configProp in configProps)
            {
                if(configProp.PropertyType == typeof(T))
                {
                    configProp.SetValue(_applicationConfigHandler.ApplicationConfig, _configViewModel.Config);
                }
            }
            bool safeResponse = _applicationConfigHandler.Safe();
            if (!safeResponse)
                NotificationHandler.ToastNotify("Error: Config could not be saved");
            IsDataChanged = true;
            OnPropertyChanged(nameof(IsDataChanged));
            NotificationHandler.ToastNotify("Config saved");
        }
        public void RestoreDefaultsConfigAction()
        {
            _configViewModel.SetDefaults();
            var propVals = _configViewModel.GetInstanceValuesWithUiDisplayNames(DataChangedEvent);
            PropertyValues = propVals;
            OnPropertyChanged(nameof(PropertyValues));
        }
        private void DataChangedEvent(ValidatableObject validatableObject,object oldValue,object newValue)
        {
            IsDataChanged = true;
            OnPropertyChanged(nameof(IsDataChanged));
        }

    }
}

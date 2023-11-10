using MobileApp.Data.AppConfig.Ui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.ViewModel.SettingsSubPage
{
    public abstract class AbstractSettingsPageGenericViewModel: BaseViewModel
    {
        public string PageTitle { get;private set; }
        private ObservableCollection<ApplicationConfigPropertyDescriptorViewModel> _propertyValues;
        public ObservableCollection<ApplicationConfigPropertyDescriptorViewModel> PropertyValues
        {
            get
            {
                return _propertyValues;
            }
            protected set {
                _propertyValues = value; 
                OnPropertyChanged(nameof(PropertyValues));
            }
        }
        public bool HasProps
        {
            get => PropertyValues != null && PropertyValues.Count != 0;
        }
        public ICommand SaveConfigCommand { get; set; }
        public ICommand RestoreDefaultsConfigCommand { get; set; }

        public virtual bool Validate()
        {
            int nok=0;
            foreach(var item in PropertyValues)
            {
                if(item.Value != null)
                {
                    bool response = item.Value.Validate();
                    if (!response)
                        nok++;
                }
            }
            return nok == 0;
        }
        public void RefreshUi()
        {
            OnPropertyChanged(nameof(PropertyValues));
        }
        public AbstractSettingsPageGenericViewModel(string pageTitle)
        {
            PageTitle = pageTitle;  
        }

    }
}

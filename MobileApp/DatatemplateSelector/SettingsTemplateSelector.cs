using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.Data.AppConfig.Ui;
using MobileApp.Model;
using MobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.DatatemplateSelector
{
    public class SettingsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultSettingsTemplate { get; set; }
        public DataTemplate BooleanSettingsTemplate { get; set; }
        public DataTemplate EnumSettingsTemplate { get; set; }
        public DataTemplate DateOnlySettingsTemplate { get; set; }
        public DataTemplate TimeOnlySettingsTemplate { get; set; }
        public DataTemplate DateTimeSettingsTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (DefaultSettingsTemplate is null)
                throw new InvalidOperationException("No default template setted");
            DataTemplate selectedTemplate = DefaultSettingsTemplate;
            if (item is not ApplicationConfigPropertyDescriptorViewModel)
            {
                throw new InvalidOperationException("item must be a ApplicationConfigPropertyDescriptorViewModel or inherit");
            }

            var model = (ApplicationConfigPropertyDescriptorViewModel)item;
            if (model.PropertyType == typeof(DateOnly))
            {
                selectedTemplate = DateOnlySettingsTemplate;
            }
            else if (model.PropertyType == typeof(TimeOnly))
            {
                selectedTemplate = TimeOnlySettingsTemplate;
            }
            else if (model.PropertyType == typeof(DateTime))
            {
                selectedTemplate = DateTimeSettingsTemplate;
            }
            else if(model.PropertyType == typeof(Enum) || model.PropertyType.IsEnum)
            {
                selectedTemplate = EnumSettingsTemplate;
            }
            else if(model.PropertyType == typeof(bool))
            {
                selectedTemplate = BooleanSettingsTemplate;
            }
            return selectedTemplate;
        }
    }
}

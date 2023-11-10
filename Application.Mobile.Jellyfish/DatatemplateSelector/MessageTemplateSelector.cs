using MobileApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.DatatemplateSelector
{

    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultMessageTemplate { get; set; }
        public DataTemplate LinkMessageTemplate { get; set; }
        public DataTemplate GpsMessageTemplate { get; set; }
        public DataTemplate ImageMessageTemplate { get; set; }
        public DataTemplate VideoMessageTemplate { get; set; }
        public DataTemplate ContactMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var msg = (Message)item;
            var selectedTemplate = msg.IsLink ?
                LinkMessageTemplate : msg.IsGpsMessage ?
                GpsMessageTemplate : (msg.IsImg ?
                (msg.Media.IsImage? ImageMessageTemplate:VideoMessageTemplate) : (msg.IsContact?
                ContactMessageTemplate : DefaultMessageTemplate));
            if (selectedTemplate == null)
            {
                throw new ArgumentNullException();
            }
            return selectedTemplate;
        }
    }
}

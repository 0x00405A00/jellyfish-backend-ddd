using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.DatatemplateSelector
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OpenExistingChatTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            return OpenExistingChatTemplate;
        }
    }
}

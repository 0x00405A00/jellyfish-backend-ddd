using MobileApp.View;
using MobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.Model
{
    public abstract class ViewTemplateModel : BaseViewModel
    {
        public string Title { get; set; }
        public Type ContentViewModelType { get; set; }
        private bool _isSelected = false;
        public virtual bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        public int NotificationCount { get; set; }  
        public ICommand RefreshDataCommand { get; set; }    
        public ViewTemplateModel()
        {

        }
    }

    public class ChatsViewTemplate : ViewTemplateModel
    {

    }
    public class StatusViewTemplate : ViewTemplateModel
    {
    }
    public class CallViewTemplate: ViewTemplateModel
    {
    }

}

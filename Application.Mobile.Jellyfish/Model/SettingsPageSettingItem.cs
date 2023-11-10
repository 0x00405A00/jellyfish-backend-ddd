using MobileApp.ViewModel;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.Model
{
    public class SettingsPageSettingItem : BaseViewModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public PathGeometry SvgPath { get; set; }
        public ICommand ExecCommand { get; set; }
    }
}

using MobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MobileApp.Model
{
    public class CameraMediaModel : BaseViewModel
    {
        public string ImageSourceFilePath { get; set; }
        public ImageSource ImageSource { get; set; } = null;
        public string VideoSourcePath { get; set; }
        public string VideoPathForUi
        {
            get
            {
                string path = VideoSourcePath;
                return path;
            }
        }

        public bool IsImage
        {
            get
            { return ImageSource != null; }
        }
        public CameraMediaModel()
        {

        }
    }
}

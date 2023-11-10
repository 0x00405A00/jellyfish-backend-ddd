using Camera.MAUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.Controls
{
    public class CustomCameraControl : CameraView
    {

        public static readonly BindableProperty CurrentZoomScaleProperty =
            BindableProperty.CreateAttached("CurrentZoomScale", typeof(double), typeof(CustomCameraControl), null, BindingMode.TwoWay, null);

        public double CurrentZoomScale
        {
            get
            {
                return (double)GetValue(CurrentZoomScaleProperty);
            }
            set
            {
                SetValue(CurrentZoomScaleProperty, value);
            }
        }
        public CustomCameraControl()
        {


        }
    }
}

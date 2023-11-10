using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Extension
{
    public static class LocationExtension
    {
        public static string ToGpsString(this Location location) 
        {
            return (location.Latitude + location.Latitude + location.Altitude)+"";
        }
    }
}

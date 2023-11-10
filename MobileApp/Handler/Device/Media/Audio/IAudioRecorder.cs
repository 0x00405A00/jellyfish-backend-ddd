using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Media.Audio
{
    public interface IAudioRecorder
    {
        void StartRecord();
        string StopRecord();
        void PauseRecord();
        void ResetRecord();
    }
}

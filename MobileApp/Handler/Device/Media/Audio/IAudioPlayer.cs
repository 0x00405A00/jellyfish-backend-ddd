using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Media.Audio
{
    public interface IAudioPlayer
    {
        void PlayAudio(string filePath);
        void Pause();
        void Stop();
        string GetCurrentPlayTime();
        bool CheckFinishedPlayingAudio();
    }
}

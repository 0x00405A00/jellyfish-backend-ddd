using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Media;

namespace MobileApp.Handler.Device.Media.Audio.iOS
{
    public class AudioPlayerHandler
    {
        /*AVPlayer _player;
        NSObject notificationHandle;
        NSUrl url;
        private bool isFinishedPlaying;
        private bool isPlaying;

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                if (_player.Rate == 1 && _player.Error == null)
                    isPlaying = true;
                else
                    isPlaying = false;
            }
        }
        public AudioPlayerHandler()
        {
            RegisterNotification();
        }
        ~AudioPlayerHandler()
        {
            UnregisterNotification();
        }
        public void PlayAudio(string filePath)
        {
            isFinishedPlaying = false;
            if (_player == null)
            {
                url = NSUrl.FromString(filePath);
                AVPlayerItem avPlayerItem = new AVPlayerItem(URL);
                _player = new AVPlayer(avPlayerItem);
                _player.AutomaticallyWaitsToMinimizeStalling = false;
                _player.Volume = 1;
                _player.Play();
                IsPlaying = true;
                isFinishedPlaying = false;
            }
            else if (_player != null && !IsPlaying)
            {
                _player.Play();
                IsPlaying = true;
                isFinishedPlaying = false;
            }
        }
        public void Pause()
        {
            if (_player != null && IsPlaying)
            {
                _player.Pause();
                IsPlaying = false;
            }
        }
        public void Stop()
        {
            if (_player != null)
            {
                _player.Dispose();
                IsPlaying = false;
                _player = null;
            }
        }
        public string GetCurrentPlayTime()
        {
            if (_player != null)
            {
                var positionTimeSeconds = _player.CurrentTime.Seconds;
                TimeSpan currentTime = TimeSpan.FromSeconds(positionTimeSeconds);
                string currentPlayTime = string.Format("{0:mm\\:ss}", currentTime);
                return currentPlayTime;
            }
            return null;
        }
        public bool CheckFinishedPlayingAudio()
        {
            return isFinishedPlaying;
        }
        private void RegisterNotification()
        {
            notificationHandle = NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, HandleNotification);
        }
        private void UnregisterNotification()
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(notificationHandle);
        }
        private void HandleNotification(NSNotification notification)
        {
            isFinishedPlaying = true;
            Stop();
        }*/
    }
}

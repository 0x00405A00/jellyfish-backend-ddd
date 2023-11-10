using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Handler.Device.Media.Audio.iOS
{
    public class AudioRecorderHandler : AbstractAudioRecorderHandler
    {
        /*AVAudioRecorder recorder;
        NSUrl url;
        NSError error;
        NSDictionary settings;
        string audioFilePath;
        public AudioRecorderHandler()
        {
            InitializeAudioSession();
        }
        private bool InitializeAudioSession()
        {
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return false;
            }
            err = audioSession.SetActive(true);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return false;
            }
            return false;
        }
        public void PauseRecord()
        {
            recorder.Pause();
        }
        public void ResetRecord()
        {
            recorder.Dispose();
            recorder = null;
        }
        public void StartRecord()
        {
            if (recorder == null)
            {
                string fileName = "/Record_" + DateTime.UtcNow.ToString("ddMMM_hhmmss") + ".wav";
                var docuFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                audioFilePath = docuFolder + fileName;
                url = NSUrl.FromFilename(audioFilePath);
                NSObject[] values = new NSObject[]
                {
              NSNumber.FromFloat(44100.0f),
              NSNumber.FromInt32((int)AudioToolbox.AudioFormatType.LinearPCM),
              NSNumber.FromInt32(2),
              NSNumber.FromInt32(16),
              NSNumber.FromBoolean(false),
              NSNumber.FromBoolean(false)
                };
                NSObject[] key = new NSObject[]
                {
              AVAudioSettings.AVSampleRateKey,
              AVAudioSettings.AVFormatIDKey,
              AVAudioSettings.AVNumberOfChannelsKey,
              AVAudioSettings.AVLinearPCMBitDepthKey,
              AVAudioSettings.AVLinearPCMIsBigEndianKey,
              AVAudioSettings.AVLinearPCMIsFloatKey
                };
                settings = NSDictionary.FromObjectsAndKeys(values, key);
                recorder = AVAudioRecorder.Create(url, new AudioSettings(settings), out error);
                recorder.PrepareToRecord();
                recorder.Record();
            }
            else
            {
                recorder.Record();
            }
        }
        public string StopRecord()
        {
            if (recorder == null)
            {
                return string.Empty;
            }
            recorder.Stop();
            recorder = null;
            return audioFilePath;
        }*/
        public override void SetUserDeniedAction()
        {
            UserDeniedAction = () => { };
        }

        public override void SetUserRationalAction()
        {
            UserRationalAction = () => { };
        }
    }
}

using Camera.MAUI.ZXingHelper;
using Camera.MAUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Collections.Specialized;
using CommunityToolkit.Maui.Views;
using MobileApp.Service;
using SkiaSharp;
using MobileApp.Model;
using System.Timers;

namespace MobileApp.ViewModel
{
    public class CameraHandlerPageViewModel : BaseViewModel
    {
        private readonly NavigationService _navigationService;
        private readonly ChatPageViewModel _chatPageViewModel;//The Chat

        private string _pictureDescription = null;
        public string PictureDescription//Pciture description that is added to the text message
        {
            get
            {
                return _pictureDescription;
            }
            set
            {
                _pictureDescription = value;
            }
        }
        private ObservableCollection<CameraMediaModel> _imageSources= new ObservableCollection<CameraMediaModel>();
        public ObservableCollection<CameraMediaModel> SnapShots 
        { 
            get
            {
                return _imageSources;    
            }

        }
        private CameraMediaModel _selectedImageModel;
        public CameraMediaModel SelectedMediaModel//The Current selected Image in PictureSelection Mode after take photo action
        {
            get
            {
                return _selectedImageModel;
            }
            set
            {
                _selectedImageModel = value;
                OnPropertyChanged(nameof(SelectedMediaModel));
                OnPropertyChanged(nameof(IsSelectedMediaAPicture));
                OnPropertyChanged(nameof(ISelectedMediaAVideo));
            }
        }

        public bool IsSelectedMediaAPicture
        {
            get
            {
                return IsPictureSelectionMode && SelectedMediaModel != null && SelectedMediaModel.IsImage;
            }
        }
        public bool ISelectedMediaAVideo
        {
            get
            {
                return IsPictureSelectionMode && SelectedMediaModel != null && !SelectedMediaModel.IsImage;
            }
        }

        public IEnumerable<FlashMode> FlashModes = Enum.GetValues<FlashMode>().Cast<FlashMode>();
        private FlashMode _flashMode = FlashMode.Auto;
        private IEnumerator<FlashMode> _flashModeEnumerator;
        public int FlashModeInt
        {
            get
            {
                return ((int)CameraFlashMode);
            }
        }
        public FlashMode CameraFlashMode
        {
            set 
            { 
                _flashMode = value;
                OnPropertyChanged(nameof(CameraFlashMode));
                OnPropertyChanged(nameof(FlashModeInt));
            }
            get
            { 
                return _flashMode;
            }
        }
        private CameraInfo camera = null;
        public CameraInfo Camera
        {
            get => camera;
            set
            {
                camera = value;
                OnPropertyChanged(nameof(Camera));
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
                AutoStartPreview = true;
                OnPropertyChanged(nameof(AutoStartPreview));
            }
        }
        private bool _useFrontCamera = false;
        private ObservableCollection<CameraInfo> cameras = new();
        public ObservableCollection<CameraInfo> Cameras
        {
            get => cameras;
            set
            {
                cameras = value;
                OnPropertyChanged(nameof(Cameras));
            }
        }
        private bool _isPictureSelectionMode = false;
        public bool IsPictureSelectionMode//Mode after taken some photos/videos
        {
            get
            {
                return _isPictureSelectionMode;
            }
            set
            {
                _isPictureSelectionMode = value;
                OnPropertyChanged(nameof(IsPictureSelectionMode));
            }
        }
        private float _zoomFactor = 1.0f;
        public float ZoomFactor//Camera Current Zoom
        {
            get => _zoomFactor;
            set

            {
                _zoomFactor = value;
                OnPropertyChanged(nameof(ZoomFactor));
            }
        }
        public int NumCameras
        {
            set
            {
                if (value > 0)
                    Camera = Cameras.First();
            }
        }
        private MicrophoneInfo micro = null;
        public MicrophoneInfo Microphone
        {
            get => micro;
            set
            {
                micro = value;
                OnPropertyChanged(nameof(Microphone));
            }
        }
        private ObservableCollection<MicrophoneInfo> micros = new();
        public ObservableCollection<MicrophoneInfo> Microphones
        {
            get => micros;
            set
            {
                micros = value;
                OnPropertyChanged(nameof(Microphones));
            }
        }
        public int NumMicrophones
        {
            set
            {
                if (value > 0)
                    Microphone = Microphones.First();
            }
        }
        public BarcodeDecodeOptions BarCodeOptions { get; set; }
        public string BarcodeText { get; set; } = "No barcode detected";
        public bool AutoStartPreview { get; set; } = true;
        public bool AutoStartRecording { get; set; } = false;
        private Result[] barCodeResults;
        public Result[] BarCodeResults
        {
            get => barCodeResults;
            set
            {
                barCodeResults = value;
                if (barCodeResults != null && barCodeResults.Length > 0)
                    BarcodeText = barCodeResults[0].Text;
                else
                    BarcodeText = "No barcode detected";
                OnPropertyChanged(nameof(BarcodeText));
            }
        }
        private bool takeSnapshot = false;
        public bool TakeSnapshot
        {
            get => takeSnapshot;
            set
            {
                takeSnapshot = value;
                OnPropertyChanged(nameof(TakeSnapshot));
            }
        }
        public float SnapshotSeconds { get; set; } = 0f;
        public string Seconds
        {
            get => SnapshotSeconds.ToString();
            set
            {
                if (float.TryParse(value, out float seconds))
                {
                    SnapshotSeconds = seconds;
                    OnPropertyChanged(nameof(SnapshotSeconds));
                }
            }
        }
        private string _selectedCameraMode = null;
        public string SelectedCameraMode
        {
            get => _selectedCameraMode;
            set 
            { 
                _selectedCameraMode = value; 
                OnPropertyChanged(nameof(SelectedCameraMode));
                OnPropertyChanged(nameof(IsCameraModeVideoRecMode));
            }
        }
        private bool _activeVideoRecording = false;
        public bool ActiveVideoRecording
        {
            get
            {
                return _activeVideoRecording;
            }
            set
            {
                _activeVideoRecording= value;
                RecordingTimerMethod();
            }
        }
        public bool IsCameraModeVideoRecMode
        {
            get
            {
                return _selectedCameraMode == _cameraModes.Last();
            }
        }
        public DateTime RecordingStartTime { get; set; } = DateTime.MinValue;
        public TimeSpan RecordingElapsedTime { get; set; } = TimeSpan.Zero;
        public double RecordingProgressBar { get; set; } = 0;
        public System.Timers.Timer RecordingTimer { get; private set; }   

        private ObservableCollection<string> _cameraModes = new ObservableCollection<string> { "Photo","Video" };
        public ObservableCollection<string> CameraModes
        {
            get
            {
                return _cameraModes;
            }
            set
            {
                _cameraModes = value;   
            }
        }
        public CameraView CameraView { get; set; }

        public bool AbortAction { get; private set; } = false;//Boolean value that indicates the state when the BackButton or Return Button is pressed. Import for closing actions from previes view: e.g. CamMode->PictureSelection Mode, on back button press doesnt return to chat. It close first the pictureselectionmode and show the cam. Another return press move bring us back to chat
        public bool HasSnapShots { get { return _imageSources != null && _imageSources.Count != 0; } }//The State if some photos are taken
        public ICommand BackButtonCommand { get; private set; }//Command action when return button is pressed. Derived from CustomContentPage Control
        public ICommand ControlFlashModeCommand { get; private set; }
        public ICommand PickPhotoCommand { get; private set; }
        public ICommand ChangeCameraCommand { get; private set; }
        public ICommand StartCamera { get; set; }
        public ICommand StopCamera { get; set; }
        public string RecordingFile { get; set; }
        public ICommand StartRecording { get; set; }
        public ICommand StopRecording { get; set; }
        public ICommand DeleteMediaFromTakenMediaCommand { get; set; }//Command that delete some photos in the taken snapshots collection
        public ICommand PrepeareForSendingCommand { get; private set; } //Command that bring us to pictureselction mode after taken some photos.
        public CameraHandlerPageViewModel(NavigationService navigationService, ChatPageViewModel chatPageViewModel)
        {
            _navigationService = navigationService;
            BarCodeOptions = new BarcodeDecodeOptions
            {
                AutoRotate = true,
                PossibleFormats = { ZXing.BarcodeFormat.QR_CODE },
                ReadMultipleCodes = false,
                TryHarder = true,
                TryInverted = true
            };
            OnPropertyChanged(nameof(BarCodeOptions));
            StartCamera = new Command(() =>
            {
                AutoStartPreview = true;
                OnPropertyChanged(nameof(AutoStartPreview));
            });
            StopCamera = new Command(() =>
            {
                AutoStartPreview = false;
                OnPropertyChanged(nameof(AutoStartPreview));
            });
#if IOS
            RecordingFile = Path.Combine(FileSystem.Current.CacheDirectory, "Video.mov");
#else
        RecordingFile = Path.Combine(FileSystem.Current.CacheDirectory, "Video.mp4");
#endif
            OnPropertyChanged(nameof(RecordingFile));
            StartRecording = new Command(() =>
            {
                AutoStartRecording = true;
                OnPropertyChanged(nameof(AutoStartRecording));
            });
            StopRecording = new Command(() =>
            {
                AutoStartRecording = false;
                OnPropertyChanged(nameof(AutoStartRecording));
                //VideoSource = MediaSource.FromFile(RecordingFile);
                //OnPropertyChanged(nameof(VideoSource));
            });
            _flashModeEnumerator = FlashModes.GetEnumerator();
            var resp = _flashModeEnumerator.MoveNext();
            CameraFlashMode = _flashModeEnumerator.Current;
            ControlFlashModeCommand = new RelayCommand(ControlFlashModeAction);
            PickPhotoCommand = new RelayCommand(PickPhotoAction);
            ChangeCameraCommand = new RelayCommand(ChangeCameraAction);
            DeleteMediaFromTakenMediaCommand = new RelayCommand<CameraMediaModel>(DeleteMediaFromTakenMediaAction);
            BackButtonCommand = new RelayCommand(BackButtonAction);
            PrepeareForSendingCommand = new RelayCommand(PrepeareForSendingAction);
            _chatPageViewModel = chatPageViewModel;
            SelectedCameraMode = CameraModes.First();
            OnPropertyChanged(nameof(AutoStartPreview));
        }
        ~CameraHandlerPageViewModel()
        {

        }
        private void PrepeareForSendingAction()
        {
            if(!IsPictureSelectionMode)
            {

                this.IsPictureSelectionMode = true;
                OnPropertyChanged(nameof(IsPictureSelectionMode));
                SelectedMediaModel = SnapShots.First();
            }
            else
            {
                SendAction();   
            }
        }
        private void SendAction()
        {

            _chatPageViewModel.TakenPhotos = this.SnapShots;
            _chatPageViewModel.Text = PictureDescription;
            _chatPageViewModel.SendMessageAction();
            CloseCameraPage();

        }
        private void BackButtonAction()
        {
            AbortAction = true;
            if (SnapShots.Count > 0)
            {
                SnapShots.Clear();

            }
            if(this.IsPictureSelectionMode)
            {
                this.IsPictureSelectionMode = false;
            }
            else if(!this.IsPictureSelectionMode)
                CloseCameraPage();
        }
        private void CloseCameraPage()
        {
            _navigationService.CloseCurrentPage();
        }
        private void DeleteMediaFromTakenMediaAction(CameraMediaModel imageSource)
        {
            if(imageSource != null)
            {
                if(SnapShots != null && SnapShots.Contains(imageSource))
                {
                    try
                    {
                        SnapShots.Remove(imageSource);
                        if (imageSource == SelectedMediaModel)
                        {
                            SelectedMediaModel = (SnapShots.Count > 0 ? SnapShots.First() : null);
                        }
                        OnPropertyChanged(nameof(SnapShots));
                        OnPropertyChanged(nameof(HasSnapShots));
                        if(SnapShots.Count== 0)
                        {
                            IsPictureSelectionMode = false;
                            OnPropertyChanged(nameof(IsPictureSelectionMode));
                        }
                    }
                    catch(Exception ex)
                    {

                    }

                }
            }
        }
        private void ChangeCameraAction()
        {
            if (Camera != null && Cameras != null)
            {
                _useFrontCamera = !_useFrontCamera;
                AutoStartPreview = false;
                var cam = Cameras.ToList().Find(x => (_useFrontCamera ? (x.Position == CameraPosition.Front) : (x.Position == CameraPosition.Back)));
                Task.Delay(500);

                Camera = cam;
                Task.Delay(500);
                AutoStartPreview = true;
            }
        }
        private void ControlFlashModeAction()
        {
            if (_flashModeEnumerator.MoveNext())
            {

            }
            else
            {
                _flashModeEnumerator.Reset();
                _flashModeEnumerator.MoveNext();
            }
            CameraFlashMode = _flashModeEnumerator.Current;
        }
        private async void PickPhotoAction()
        {
            if(CameraFlashMode == FlashMode.Enabled)
            {
                //await Flashlight.Default.TurnOnAsync();

            }
            if(IsCameraModeVideoRecMode)
            {
                ActiveVideoRecording = !ActiveVideoRecording;
                OnPropertyChanged(nameof(ActiveVideoRecording));
                
            }
            else
            {
                TakeSnapshot = false;
                TakeSnapshot = true;
            }
        }
        private void RecordingTimerMethod()
        {
            if (RecordingTimer == null)
            {
                RecordingTimer = new System.Timers.Timer();
            }
            if (ActiveVideoRecording)
            {
                RecordingTimer.AutoReset = true;
                RecordingTimer.Enabled = true;
                RecordingTimer.Interval = 1000;
                RecordingTimer.Elapsed += (s, e) => {
                    var elapsedTime = DateTime.Now - RecordingStartTime;
                    RecordingElapsedTime = elapsedTime;
                    RecordingProgressBar = (100.0 / 60.0 * elapsedTime.Seconds) / 100.0;
                    OnPropertyChanged(nameof(RecordingElapsedTime));
                    OnPropertyChanged(nameof(RecordingProgressBar));
                };
                RecordingStartTime = DateTime.Now;
                RecordingTimer.Start();

            }
            else
            {

                RecordingTimer.Stop();
                RecordingElapsedTime = TimeSpan.Zero;
                RecordingProgressBar = 0;
            }
        }
        public async void AddCapturedMedia(CameraMediaModel cameraImageModel)
        {
            if(cameraImageModel != null)
            {
                if (CameraFlashMode == FlashMode.Enabled)
                {
                    //await Flashlight.Default.TurnOffAsync();

                }
                this.SnapShots.Add(cameraImageModel);
                if(SelectedMediaModel == null)
                {
                    SelectedMediaModel = cameraImageModel;  
                }
                OnPropertyChanged(nameof(SnapShots));
                OnPropertyChanged(nameof(HasSnapShots));

            }
        }
    }
}

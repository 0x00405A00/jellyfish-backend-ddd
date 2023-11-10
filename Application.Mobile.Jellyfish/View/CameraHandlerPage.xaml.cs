using MobileApp.Controls;
using MobileApp.ViewModel;
using System.Windows.Input;
using Camera.MAUI;
using ImageFormat = Camera.MAUI.ImageFormat;
using CommunityToolkit.Maui.Views;
using Microsoft.Extensions.Logging;

namespace MobileApp.View;

public partial class CameraHandlerPage : CustomContentPage, IDisposable
{
    private double currentScale = 1;
    private double startScale = 1;

    public CameraHandlerPage()
	{
        try
        {
            InitializeComponent();

        }
        catch (Exception ex)
        {
            NotificationHandler.ToastNotify(ex.Message);
        }

	}
    ~CameraHandlerPage() { 
    

    }
    private string _currentRecFile = null;
    private async void Button_Clicked(object sender, EventArgs e)
    {
        CameraHandlerPageViewModel viewModel = this.BindingContext as CameraHandlerPageViewModel;
        string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, DateTime.Now.Ticks + (viewModel.IsCameraModeVideoRecMode?(".mp4") :(".jpg")));
        if (viewModel.IsCameraModeVideoRecMode)
        {
            try
            {

                if (viewModel.ActiveVideoRecording)
                {
                    _currentRecFile = localFilePath;
                    var camRes = await Camera.StartRecordingAsync(_currentRecFile);
                    if (camRes == CameraResult.Success)
                    {

                    }
                    else
                    {
                        await Camera.StopRecordingAsync();
                        viewModel.ActiveVideoRecording = false;
                        NotificationHandler.ToastNotify("Error: No permissions to record a video");
                    }
                }
                else
                {
                    var camRes = await Camera.StopRecordingAsync();

                    if (camRes == CameraResult.Success)
                    {
                        var model = new Model.CameraMediaModel() { VideoSourcePath = _currentRecFile };
                        viewModel.AddCapturedMedia(model);
                    }
                    else
                    {

                        NotificationHandler.ToastNotify("Error: No permissions to save the recording");
                    }
                }
            }
            catch(Exception ex)
            {
                NotificationHandler.ToastNotify(ex.Message);
            }
        }
        else
        {
            bool writeResponse = await Camera.SaveSnapShot(ImageFormat.JPEG, localFilePath);
            if(writeResponse) { 
                viewModel.AddCapturedMedia(new Model.CameraMediaModel { ImageSourceFilePath = localFilePath, ImageSource = Camera.SnapShot });
            }
            else
            {
                NotificationHandler.ToastNotify("Error: Save snapshot failed");
            }

        }
    }
    protected override void OnDisappearing()
    {
        
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {

                if (Camera.SnapShot != null)
                {
                    bool resp = await Camera.SnapShot?.Cancel();

                }
                if (Camera.SnapShotStream != null)
                {
                    Camera.SnapShotStream?.Close();

                }
                //Camera.SnapShotStream?.Dispose();
            }
            catch (Exception ex)
            {

            }
            if (await Camera.StopCameraAsync() == CameraResult.Success)
            {

            }
        });
        
        //this.Dispose();
        base.OnDisappearing();
    }
    protected override void OnHandlerChanged()
    {
        try
        {
            base.OnHandlerChanged();

        }
        catch (Exception ex)
        {

        }
    }
    private void Camera_CamerasLoaded(object sender, EventArgs e)
    {
        Camera.Camera = Camera.Cameras.First();
        Camera.Microphone = Camera.Microphones.First();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            
            if (await Camera.StartCameraAsync() == CameraResult.Success)
            {

            }
        });
    }
    private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        currentScale += (e.Scale - 1) * startScale;
        currentScale = Math.Max(1, currentScale);
        if (e.Status == GestureStatus.Started)
        {
            startScale = Content.Scale;
            Content.AnchorX = 0;
            Content.AnchorY = 0;
        }
        if (e.Status == GestureStatus.Running)
        {
            currentScale += (e.Scale - 1) * startScale;
            currentScale = Math.Max(1, currentScale);

            Camera.ZoomFactor = (float)currentScale;
        }
    }

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {

    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
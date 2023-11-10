using MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.ViewModel
{
    public class MediaPlayerPageViewModel : BaseViewModel
    {
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
                return SelectedMediaModel != null && SelectedMediaModel.IsImage;
            }
        }
        public bool ISelectedMediaAVideo
        {
            get
            {
                return SelectedMediaModel != null && !SelectedMediaModel.IsImage;
            }
        }
        public ICommand BackButtonCommand { get; private set; }

        private ObservableCollection<CameraMediaModel> _media = new ObservableCollection<CameraMediaModel>();
        public ObservableCollection<CameraMediaModel> Media
        {
            get
            {
                return _media;
            }

        }
        public MediaPlayerPageViewModel()
        {

            BackButtonCommand = new RelayCommand(BackButtonAction);
        }

        public void SetVideoModel(List<CameraMediaModel> data)
        {
            data.ForEach(x => Media.Add(x));    
            SelectedMediaModel = data.First();

            OnPropertyChanged(nameof(SelectedMediaModel));    
        }
        public void BackButtonAction()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MobileApp.Data.SqlLite.Schema;
using MobileApp.Handler.Device.Extension;
using MobileApp.ViewModel;
using Application.Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish;

namespace MobileApp.Model
{

    public class Message : BaseViewModel
    {

        private Guid _msgId;
        public Guid MessageId
        {
            get { return _msgId; }
            set { _msgId = value; }
        }
        private string _text;
        private bool _received;
        private DateTime _messageDateTime;
        public DateTime MessageDateTime
        {
            get { return _messageDateTime; }
            set
            {
                _messageDateTime = value;
                OnPropertyChanged();
            }
        }
        private CameraMediaModel _media;
        public CameraMediaModel Media
        {
            get { return _media; }
            set
            {
                _media = value;
                OnPropertyChanged();
            }
        }
        private User _contact;
        public User Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                OnPropertyChanged();
            }
        }
        private Location _location;
        public Location Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
                OnPropertyChanged("LocationStr");
            }
        }
        public string LocationStr
        {
            get 
            {
                string str = string.Format("Lat: {0}, Lon: {1}", _location.Latitude, _location.Longitude);
                return str; 
            }
        }
        public bool IsGpsMessage
        {
            get { return _location != null; }
        }
        private bool _sendToBackend;
        public bool SendToBackend
        {
            get
            { return _sendToBackend; }
            set
            {
                _sendToBackend = value;
                OnPropertyChanged();
                OnPropertyChanged("SendToBackendActionProgressBar");
            }
        }
        private bool _receiverGetMessage;
        public bool ReceiverGetMessage
        {
            get
            { return _receiverGetMessage; }
            set
            {
                _receiverGetMessage = value;
                OnPropertyChanged();
            }
        }
        public bool SendToBackendActionProgressBar
        {
            get
            { return !_sendToBackend; }
        }
        private bool _readed;
        public bool Readed
        {
            get
            { return _readed; }
            set
            {
                _readed = value;
                OnPropertyChanged();
            }
        }
        public Uri ExtractedUrlFromText
        {
            get
            {

                if (!string.IsNullOrEmpty(_text))
                {
                    var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    var matches = linkParser.Matches(_text);
                    if (matches != null && matches.Count() > 0)
                    {
                        var match = matches.First();

                        if (match != null)
                        {
                            string urlPartOfText = match.Value;
                            if (!string.IsNullOrEmpty(urlPartOfText))
                            {

                                Uri uriResult;
                                bool result = Uri.TryCreate(urlPartOfText, UriKind.Absolute, out uriResult)
                                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                return result ? uriResult : null;
                            }
                        }
                    }

                }
                return null;
            }
        }
        private bool _isLink;
        public bool IsLink
        {
            get
            {
                return ExtractedUrlFromText != null;
            }

        }

        public string FoundedUrlInText
        {
            get
            {
                return ExtractedUrlFromText != null ? ExtractedUrlFromText.ToString() : null;
            }
        }
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        public bool Received
        {
            get { return _received; }
            set
            {
                _received = value;
                OnPropertyChanged();
            }
        }
        public bool IsImg
        {
            get { return _media != null; }
        }
        public bool IsContact
        {
            get { return Contact != null; }
        }
        public bool HasText
        {
            get { return !string.IsNullOrWhiteSpace(this.Text); }
        }
        public Message()
        {
            
        }
        public Message(MessageDTO messageDTO)
        {
            this.Text = messageDTO.Text;
            this.MessageId = messageDTO.Uuid;
            this.Received = true;
            this.MessageDateTime = messageDTO.CreationDateTime;
            this.SendToBackend = true;
        }
        public Message(MessageEntity messageEntity)
        {
            
        }

    }
}

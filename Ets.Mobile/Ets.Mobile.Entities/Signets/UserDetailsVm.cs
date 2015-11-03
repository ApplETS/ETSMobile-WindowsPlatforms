using System.Runtime.Serialization;
using Windows.UI.Xaml.Media;
using ReactiveUI;
using Splat;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class UserDetailsVm : ReactiveObject
    {
        private string _lastName;
        [DataMember] public string LastName
        {
            get { return _lastName; }
            set { this.RaiseAndSetIfChanged(ref _lastName, value); } 
        }

        private string _firstName;
        [DataMember] public string FirstName
        {
            get { return _firstName; }
            set { this.RaiseAndSetIfChanged(ref _firstName, value); } 
        }

        private string _permanentCode;
        [DataMember] public string PermanentCode 
        { 
            get { return _permanentCode; }
            set { this.RaiseAndSetIfChanged(ref _permanentCode, value); } 
        }

        private string _balance;
        [DataMember] public string Balance 
        { 
            get { return _balance; }
            set { this.RaiseAndSetIfChanged(ref _balance, value); }
        }

        private bool _isMan;
        [DataMember] public bool IsMan 
        { 
            get { return _isMan; } 
            set { this.RaiseAndSetIfChanged(ref _isMan, value); } 
        }

        private string _username;
        [DataMember] public string Username
        {
            get { return _username; }
            set
            {
                this.RaiseAndSetIfChanged(ref _username, value);
                Email = _username + "@ens.etsmtl.ca";
            }
        }

        private string _email;
        [DataMember]
        public string Email
        {
            get { return _email; }
            set { this.RaiseAndSetIfChanged(ref _email, value); }
        }

        private IBitmap _imageSource;
        public IBitmap Image
        {
            get { return _imageSource; }
            set
            {
                this.RaiseAndSetIfChanged(ref _imageSource, value);
                ImageSource = value.ToNative();
            }
        }

        private ImageSource _imgSource;
        public ImageSource ImageSource
        {
            get { return _imgSource; }
            set { this.RaiseAndSetIfChanged(ref _imgSource, value); }
        }
    }
}
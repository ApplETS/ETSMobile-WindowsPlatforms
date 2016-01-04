using System.Runtime.Serialization;
using ReactiveUI;
using ReactiveUI.Extensions;

namespace Ets.Mobile.Entities.Moodle
{
    [DataContract]
    public class MoodleSiteInfoVm : ReactiveObject, IMergeableObject<MoodleSiteInfoVm>
    {
        #region IMergeableObject

        public bool Equals(MoodleSiteInfoVm x, MoodleSiteInfoVm y)
        {
            return x.Username == y.Username
                && x.FullName == y.FullName
                && x.SiteName == y.SiteName
                && x.SiteUrl == y.SiteUrl
                && x.UserId == y.UserId
                && x.FirstName == y.FirstName
                && x.LastName == y.LastName
                && x.UserPictureUrl == y.UserPictureUrl;
        }

        public int GetHashCode(MoodleSiteInfoVm obj)
        {
            return obj.Username.GetHashCode() ^
                   obj.FullName.GetHashCode() ^
                   obj.SiteName.GetHashCode() ^
                   obj.SiteUrl.GetHashCode() ^
                   obj.UserId.GetHashCode() ^
                   obj.FirstName.GetHashCode() ^
                   obj.LastName.GetHashCode() ^
                   obj.UserPictureUrl.GetHashCode();
        }

        public void MergeWith(MoodleSiteInfoVm other)
        {
            Username = other.Username;
            FullName = other.FullName;
            SiteName = other.SiteName;
            SiteUrl = other.SiteUrl;
            UserId = other.UserId;
            FirstName = other.FirstName;
            LastName = other.LastName;
            UserPictureUrl = other.UserPictureUrl;
        }

        #endregion

        private string _siteName;
        [DataMember]
        public string SiteName
        {
            get { return _siteName; }
            set { this.RaiseAndSetIfChanged(ref _siteName, value); }
        }
        private string _userName;
        [DataMember]
        public string Username
        {
            get { return _userName; }
            set { this.RaiseAndSetIfChanged(ref _userName, value); }
        }
        private string _firstName;
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { this.RaiseAndSetIfChanged(ref _firstName, value); }
        }
        private string _lastName;
        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set { this.RaiseAndSetIfChanged(ref _lastName, value); }
        }
        private string _fullName;
        [DataMember]
        public string FullName
        {
            get { return _fullName; }
            set { this.RaiseAndSetIfChanged(ref _fullName, value); }
        }
        private int _userId;
        [DataMember]
        public int UserId
        {
            get { return _userId; }
            set { this.RaiseAndSetIfChanged(ref _userId, value); }
        }
        private string _siteUrl;
        [DataMember]
        public string SiteUrl
        {
            get { return _siteUrl; }
            set { this.RaiseAndSetIfChanged(ref _siteUrl, value); }
        }
        private string _userPictureUrl;
        [DataMember]
        public string UserPictureUrl
        {
            get { return _userPictureUrl; }
            set { this.RaiseAndSetIfChanged(ref _userPictureUrl, value); }
        }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;
using ReactiveUI;
using ReactiveUI.Extensions;

namespace Ets.Mobile.Entities.Moodle
{
    public class MoodleCourseModuleVm : ReactiveObject, IMergeableObject<MoodleCourseModuleVm>
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseModuleVm x, MoodleCourseModuleVm y)
        {
            return x.Id == y.Id
                && x.IsVisible == y.IsVisible
                && x.Name == y.Name
                && x.Instance == y.Instance
                && x.Description == y.Description
                && x.ModIcon == y.ModIcon
                && x.ModName == y.ModName
                && x.ModPlural == y.ModPlural
                && x.Indent == y.Indent
                && x.Url == y.Url
                && x.Contents == y.Contents;
        }

        public int GetHashCode(MoodleCourseModuleVm obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.IsVisible.GetHashCode() ^
                   obj.Name.GetHashCode() ^
                   obj.Instance.GetHashCode() ^
                   obj.Description.GetHashCode() ^
                   obj.ModIcon.GetHashCode() ^
                   obj.ModName.GetHashCode() ^
                   obj.ModPlural.GetHashCode() ^
                   obj.Indent.GetHashCode() ^
                   obj.Url.GetHashCode() ^
                   obj.Contents.GetHashCode();
        }

        public void MergeWith(MoodleCourseModuleVm other)
        {
            Id = other.Id;
            IsVisible = other.IsVisible;
            Name = other.Name;
            Instance = other.Instance;
            Description = other.Description;
            ModIcon = other.ModIcon;
            ModName = other.ModName;
            ModPlural = other.ModPlural;
            Indent = other.Indent;
            Url = other.Url;
            Contents = other.Contents;
        }

        #endregion

        private int _id;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }
        private string _name;

        [DataMember]
        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }
        private int? _instance;

        [DataMember]
        public int? Instance
        {
            get { return _instance; }
            set { this.RaiseAndSetIfChanged(ref _instance, value); }
        }
        private string _description;

        [DataMember]
        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }
        private bool? _isVisible;

        [DataMember]
        public bool? IsVisible
        {
            get { return _isVisible; }
            set { this.RaiseAndSetIfChanged(ref _isVisible, value); }
        }
        private string _modIcon;

        [DataMember]
        public string ModIcon
        {
            get { return _modIcon; }
            set { this.RaiseAndSetIfChanged(ref _modIcon, value); }
        }
        private string _modName;

        [DataMember]
        public string ModName
        {
            get { return _modName; }
            set { this.RaiseAndSetIfChanged(ref _modName, value); }
        }
        private string _modPlural;

        [DataMember]
        public string ModPlural
        {
            get { return _modPlural; }
            set { this.RaiseAndSetIfChanged(ref _modPlural, value); }
        }
        private int? _indent;

        [DataMember]
        public int? Indent
        {
            get { return _indent; }
            set { this.RaiseAndSetIfChanged(ref _indent, value); }
        }
        private string _url;

        [DataMember]
        public string Url
        {
            get { return _url; }
            set { this.RaiseAndSetIfChanged(ref _url, value); }
        }
        private MoodleCourseModuleContentVm[] _contents;

        [DataMember]
        public MoodleCourseModuleContentVm[] Contents
        {
            get { return _contents; }
            set { this.RaiseAndSetIfChanged(ref _contents, value); }
        }
    }
}
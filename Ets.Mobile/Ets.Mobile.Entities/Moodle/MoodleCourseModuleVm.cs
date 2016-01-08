using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;
using ReactiveUI.Extensions;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Moodle
{
    public class MoodleCourseModuleVm : ReactiveObject, IMergeableObject<MoodleCourseModuleVm>, ICustomColor
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
            SetNewColor(new ColorVm(other.Color));

            if (other.Contents != null && Contents != null)
            {
                foreach (var tuple in Contents.Where(c => other.Contents.Any(x => c.FileName == x.FileName)).Select(m => new Tuple<MoodleCourseModuleContentVm, MoodleCourseModuleContentVm>(m, other.Contents.First(x => m.FileName == x.FileName))))
                {
                    tuple.Item1.MergeWith(tuple.Item2);
                }
            }
            if (Contents == null && other.Contents != null)
            {
                Contents = other.Contents;
            }
        }

        #endregion

        #region ICustomColor Implementation

        private string _color;
        [DataMember]
        public string Color
        {
            get { return _color; }
            set { this.RaiseAndSetIfChanged(ref _color, value); }
        }

        public void SetNewColor(ColorVm color)
        {
            // Set Value for Store
            if (string.IsNullOrEmpty(Color) || Color != color.HexColor)
            {
                Color = color.HexColor;
            }
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
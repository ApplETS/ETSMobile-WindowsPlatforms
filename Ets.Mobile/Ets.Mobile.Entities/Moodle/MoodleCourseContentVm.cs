using ReactiveUI;
using ReactiveUI.Extensions;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Moodle
{
    public class MoodleCourseContentVm : ReactiveObject, IMergeableObject<MoodleCourseContentVm>
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseContentVm x, MoodleCourseContentVm y)
        {
            return x.Id == y.Id
                && x.IsVisible == y.IsVisible
                && x.Modules.Equals(y.Modules)
                && x.Name == y.Name
                && x.Summary == y.Summary
                && x.SummaryFormat == y.SummaryFormat;
        }

        public int GetHashCode(MoodleCourseContentVm obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.IsVisible.GetHashCode() ^
                   obj.Modules.GetHashCode() ^
                   obj.Name.GetHashCode() ^
                   obj.Summary.GetHashCode() ^
                   obj.SummaryFormat.GetHashCode();
        }

        public void MergeWith(MoodleCourseContentVm other)
        {
            Id = other.Id;
            IsVisible = other.IsVisible;
            Modules = other.Modules;
            Name = other.Name;
            Summary = other.Summary;
            SummaryFormat = other.SummaryFormat;
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
        private bool _isVisible;

        [DataMember]
        public bool IsVisible
        {
            get { return _isVisible; }
            set { this.RaiseAndSetIfChanged(ref _isVisible, value); }
        }
        private string _summary;

        [DataMember]
        public string Summary
        {
            get { return _summary; }
            set { this.RaiseAndSetIfChanged(ref _summary, value); }
        }
        private int _summaryFormat;

        [DataMember]
        public int SummaryFormat
        {
            get { return _summaryFormat; }
            set { this.RaiseAndSetIfChanged(ref _summaryFormat, value); }
        }
        private MoodleCourseModuleVm[] _modules;

        [DataMember]
        public MoodleCourseModuleVm[] Modules
        {
            get { return _modules; }
            set { this.RaiseAndSetIfChanged(ref _modules, value); }
        }
    }
}

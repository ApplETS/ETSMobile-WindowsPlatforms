using ReactiveUI;
using ReactiveUI.Extensions;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Moodle
{
    public class MoodleCourseModuleContentVm : ReactiveObject, IMergeableObject<MoodleCourseModuleContentVm>
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseModuleContentVm x, MoodleCourseModuleContentVm y)
        {
            return x.ModuleType == y.ModuleType
                && x.FileName == y.FileName
                && x.FilePath == y.FilePath
                && x.FileSize == y.FileSize
                && x.FileUrl == y.FileUrl
                && x.TimeCreated == y.TimeCreated
                && x.TimeModified == y.TimeModified
                && x.SortOrder == y.SortOrder
                && x.UserId == y.UserId
                && x.Author == y.Author
                && x.License == y.License;
        }

        public int GetHashCode(MoodleCourseModuleContentVm obj)
        {
            return obj.ModuleType.GetHashCode() ^
                   obj.FileName.GetHashCode() ^
                   obj.FilePath.GetHashCode() ^
                   obj.FileSize.GetHashCode() ^
                   obj.FileUrl.GetHashCode() ^
                   obj.TimeCreated.GetHashCode() ^
                   obj.TimeModified.GetHashCode() ^
                   obj.SortOrder.GetHashCode() ^
                   obj.UserId.GetHashCode() ^
                   obj.Author.GetHashCode() ^
                   obj.License.GetHashCode();
        }

        public void MergeWith(MoodleCourseModuleContentVm other)
        {
            ModuleType = other.ModuleType;
            FileName = other.FileName;
            FilePath = other.FilePath;
            FileSize = other.FileSize;
            FileUrl = other.FileUrl;
            TimeCreated = other.TimeCreated;
            TimeModified = other.TimeModified;
            SortOrder = other.SortOrder;
            UserId = other.UserId;
            Author = other.Author;
            License = other.License;
        }

        #endregion

        private string _moodleType;

        [DataMember]
        public string ModuleType
        {
            get { return _moodleType; }
            set { this.RaiseAndSetIfChanged(ref _moodleType, value); }
        }
        private string _fileName;

        [DataMember]
        public string FileName
        {
            get { return _fileName; }
            set { this.RaiseAndSetIfChanged(ref _fileName, value); }
        }
        private string _filePath;

        [DataMember]
        public string FilePath
        {
            get { return _filePath; }
            set { this.RaiseAndSetIfChanged(ref _filePath, value); }
        }
        private int? _fileSize;

        [DataMember]
        public int? FileSize
        {
            get { return _fileSize; }
            set { this.RaiseAndSetIfChanged(ref _fileSize, value); }
        }
        private string _fileUrl;

        [DataMember]
        public string FileUrl
        {
            get { return _fileUrl; }
            set { this.RaiseAndSetIfChanged(ref _fileUrl, value); }
        }
        private object _timeCreated;

        [DataMember]
        public object TimeCreated
        {
            get { return _timeCreated; }
            set { this.RaiseAndSetIfChanged(ref _timeCreated, value); }
        }
        private int? _timeModified;

        [DataMember]
        public int? TimeModified
        {
            get { return _timeModified; }
            set { this.RaiseAndSetIfChanged(ref _timeModified, value); }
        }
        private int? _sortOrder;

        [DataMember]
        public int? SortOrder
        {
            get { return _sortOrder; }
            set { this.RaiseAndSetIfChanged(ref _sortOrder, value); }
        }
        private object _userId;

        [DataMember]
        public object UserId
        {
            get { return _userId; }
            set { this.RaiseAndSetIfChanged(ref _userId, value); }
        }
        private object _author;

        [DataMember]
        public object Author
        {
            get { return _author; }
            set { this.RaiseAndSetIfChanged(ref _author, value); }
        }
        private object _license;

        [DataMember]
        public object License
        {
            get { return _license; }
            set { this.RaiseAndSetIfChanged(ref _license, value); }
        }
    }
}
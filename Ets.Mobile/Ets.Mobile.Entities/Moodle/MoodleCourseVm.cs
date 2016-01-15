using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;
using ReactiveUI.Extensions;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Moodle
{
    [DataContract]
    public class MoodleCourseVm : ReactiveObject, IMergeableObject<MoodleCourseVm>, ICustomColor
    {
        #region IMergeableObject

        public bool Equals(MoodleCourseVm x, MoodleCourseVm y)
        {
            return x.Id == y.Id
                && x.ShortName == y.ShortName
                && x.FullName == y.FullName
                && x.IdNumber == y.IdNumber
                && x.Summary == y.Summary;
        }

        public int GetHashCode(MoodleCourseVm obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.ShortName.GetHashCode() ^
                   obj.FullName.GetHashCode() ^
                   obj.IdNumber.GetHashCode() ^
                   obj.Summary.GetHashCode();
        }

        public void MergeWith(MoodleCourseVm other)
        {
            Id = other.Id;
            ShortName = other.ShortName;
            FullName = other.FullName;
            IdNumber = other.IdNumber;
            Summary = other.Summary;
            SetNewColor(new ColorVm(other.Color));
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

        #region Moodle Properties

        private int _id;
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        private string _shortName;
        [DataMember]
        public string ShortName
        {
            get { return _shortName; }
            set { this.RaiseAndSetIfChanged(ref _shortName, value); }
        }

        private string _fullName;
        [DataMember]
        public string FullName
        {
            get { return _fullName; }
            set { this.RaiseAndSetIfChanged(ref _fullName, value); }
        }

        private string _idNumber;
        [DataMember]
        public string IdNumber
        {
            get { return _idNumber; }
            set { this.RaiseAndSetIfChanged(ref _idNumber, value); }
        }

        private string _summary;
        [DataMember]
        public string Summary
        {
            get { return _summary; }
            set { this.RaiseAndSetIfChanged(ref _summary, value); }
        }

        #endregion

        private string _semester;
        [DataMember]
        public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }
        private string _courseName;
        [DataMember]
        public string CourseName
        {
            get { return _courseName; }
            set { this.RaiseAndSetIfChanged(ref _courseName, value); }
        }
        private string[] _groups;
        [DataMember]
        public string[] Groups
        {
            get { return _groups; }
            set { this.RaiseAndSetIfChanged(ref _groups, value); }
        }

        public string Url => $"https://ena.etsmtl.ca/course/view.php?id={Id}";
    }
}
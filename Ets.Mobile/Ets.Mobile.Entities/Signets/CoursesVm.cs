using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;
using ReactiveUI.Extensions;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class CourseVm : ReactiveObject, ICustomColor, IMergeableObject<CourseVm>
    {
        #region IMergeableObject

        public bool Equals(CourseVm x, CourseVm y)
        {
            return x.Acronym == y.Acronym
                && x.Semester == y.Semester;
        }

        public int GetHashCode(CourseVm obj)
        {
            return obj.Acronym.GetHashCode() ^ obj.Semester.GetHashCode();
        }

        public void MergeWith(CourseVm other)
        {
            Acronym = other.Acronym;
            Semester = other.Semester;
            Group = other.Group;
            Grade = other.Grade;
            Name = other.Name;
            SetNewColor(new ColorVm(other.Color));
        }

        #endregion

        private string _acronym;
        [DataMember] public string Acronym
        {
            get { return _acronym; }
            set { this.RaiseAndSetIfChanged(ref _acronym, value); }
        }

        private string _group;
        [DataMember] public string Group
        {
            get { return _group; }
            set { this.RaiseAndSetIfChanged(ref _group, value); }
        }

        private string _semester;
        [DataMember] public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private string _program;
        [DataMember] public string Program
        {
            get { return _program; }
            set { this.RaiseAndSetIfChanged(ref _program, value); }
        }

        private string _grade;
        [DataMember] public string Grade
        {
            get { return _grade; }
            set { this.RaiseAndSetIfChanged(ref _grade, value); }
        }

        private double _credits;
        [DataMember] public double Credits
        {
            get { return _credits; }
            set { this.RaiseAndSetIfChanged(ref _credits, value); }
        }

        private string _name;
        [DataMember] public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

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
    }
}
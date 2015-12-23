using System;
using System.Runtime.Serialization;
using Windows.UI;
using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;
using ReactiveUI.Extensions;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class ScheduleVm : ReactiveObject, ICustomColor, IMergeableObject<ScheduleVm>, IDisposable
    {
        #region IMergeableObject

        public bool Equals(ScheduleVm x, ScheduleVm y)
        {
            return x.Name == y.Name
                && x.StartDate == y.StartDate
                && x.EndDate == y.EndDate
                && x.CourseAndGroup == y.CourseAndGroup;
        }

        public int GetHashCode(ScheduleVm obj)
        {
            return obj.Name.GetHashCode() ^
                   obj.StartDate.ToString("O").GetHashCode() ^
                   obj.EndDate.ToString("O").GetHashCode() ^
                   obj.CourseAndGroup.GetHashCode();
        }

        public void MergeWith(ScheduleVm other)
        {
            StartDate = other.StartDate;
            EndDate = other.EndDate;
            CourseAndGroup = other.CourseAndGroup;
            Location = other.Location;
            Description = other.Description;
            SetNewColor(new ColorVm(other.Color));
        }

        #endregion

        private string _semester;
        [DataMember]
        public string Semester
        {
            get { return _semester; }
            set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private DateTime _startDate;
        [DataMember] public DateTime StartDate
        {
        	get { return _startDate; }
        	set { this.RaiseAndSetIfChanged(ref _startDate, value.ToLocalTime()); }
        }

        private DateTime _endDate;
        [DataMember] public DateTime EndDate
        {
        	get { return _endDate; }
            set { this.RaiseAndSetIfChanged(ref _endDate, value.ToLocalTime()); }
        }

        private string _courseAndGroup;
        [DataMember] public string CourseAndGroup
        {
        	get { return _courseAndGroup; }
            set { this.RaiseAndSetIfChanged(ref _courseAndGroup, value); }
        }

        private string _name;
        [DataMember] public string Name
        {
        	get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private string _location;
        [DataMember] public string Location
        {
        	get { return _location; }
            set { this.RaiseAndSetIfChanged(ref _location, value); }
        }

        private string _description;
        [DataMember] public string Description
        {
        	get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }

        private string _title;
        [DataMember] public string Title
        {
        	get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        public string Time => $"{StartDate.ToString(@"hh\:mm tt")}-{EndDate.ToString(@"hh\:mm tt")}";

        [DataMember]
        public string ActivityName => $"{CourseAndGroup.Substring(0, CourseAndGroup.IndexOf("-", StringComparison.Ordinal))}: {Title}";

        // TODO : Make this work so that the user can see the time remaining before his class occurs
        public bool _isTimeRemainingVisible = false;
        public bool IsTimeRemainingVisible
        {
            get { return _isTimeRemainingVisible; }
            set { this.RaiseAndSetIfChanged(ref _isTimeRemainingVisible, value); }
        }

        // TODO : Make this work so that the user can see the time remaining before his class occurs
        public string _timeRemaining = "0";
        public string TimeRemaining
        {
            get { return _timeRemaining; }
            set { this.RaiseAndSetIfChanged(ref _timeRemaining, value); }
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

        public void Dispose() {}
    }
}
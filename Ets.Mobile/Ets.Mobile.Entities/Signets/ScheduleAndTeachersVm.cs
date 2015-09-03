using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;
using System.Runtime.Serialization;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class ScheduleAndTeachersVm : ReactiveObject
    {
        [DataMember]
        public List<ActivityVm> Activities { get; set; }

        public List<TeacherVm> Teachers { get; set; }
    }

    [DataContract]
    public class ActivityVm : ICustomColor
    {
        [DataMember]
        public string Acronym { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public int Day { get; set; }

        [DataMember]
        public string DayName { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsPrincipalActivity { get; set; }

        [DataMember]
        public TimeSpan StartHour { get; set; }

        [DataMember]
        public TimeSpan EndHour { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Title { get; set; }

        public string Time => $"{new DateTime(1, 1, 1, StartHour.Hours, StartHour.Minutes, StartHour.Seconds).ToString(@"hh\:mm tt")}-{new DateTime(1, 1, 1, EndHour.Hours, EndHour.Minutes, EndHour.Seconds).ToString(@"hh\:mm tt")}";

        [DataMember]
        public string ActivityName => $"{Acronym}: {Title}";

        #region ICustomColor Implementation

        [DataMember] public byte A { get; set; }
        [DataMember] public byte R { get; set; }
        [DataMember] public byte G { get; set; }
        [DataMember] public byte B { get; set; }

        private Color _color;
        public Color Color
        {
            get { return Color.FromArgb(A, R, G, B); }
            set
            {
                _color = value;
                A = _color.A;
                R = _color.R;
                G = _color.G;
                B = _color.B;
            }
        }
        
        public SolidColorBrush Brush => new SolidColorBrush(Color);

        #endregion
    }

    public class TeacherVm : ReactiveObject
    {
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { this.RaiseAndSetIfChanged(ref _lastName, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { this.RaiseAndSetIfChanged(ref _firstName, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { this.RaiseAndSetIfChanged(ref _email, value); }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { this.RaiseAndSetIfChanged(ref _location, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { this.RaiseAndSetIfChanged(ref _phone, value); }
        }

        private bool _isPrimaryTeacher;
        public bool IsPrimaryTeacher
        {
            get { return _isPrimaryTeacher; }
            set { this.RaiseAndSetIfChanged(ref _isPrimaryTeacher, value); }
        }
    }
}

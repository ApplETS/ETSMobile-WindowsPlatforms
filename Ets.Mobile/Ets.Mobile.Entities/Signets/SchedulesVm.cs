using System;
using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class ScheduleVm : ReactiveObject, ICustomColor
    {
    	private DateTime _startDate;
        [DataMember] public DateTime StartDate
        {
        	get { return _startDate; }
        	set { this.RaiseAndSetIfChanged(ref _startDate, value); }
        }

        private DateTime _endDate;
        [DataMember] public DateTime EndDate
        {
        	get { return _endDate; }
            set { this.RaiseAndSetIfChanged(ref _endDate, value); }
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
        public string Title
        {
        	get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        public string Time => $"{StartDate.ToString(@"hh\:mm tt")}-{EndDate.ToString(@"hh\:mm tt")}";

        [DataMember]
        public string ActivityName => $"{CourseAndGroup.Substring(0, CourseAndGroup.IndexOf("-", StringComparison.Ordinal))}: {Title}";

        #region ICustomColor Implementation

        [DataMember]
        public byte A { get; set; }
        [DataMember]
        public byte R { get; set; }
        [DataMember]
        public byte G { get; set; }
        [DataMember]
        public byte B { get; set; }

        public void SetNewColor(ColorVm color)
        {
            // Set Value for Store
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public SolidColorBrush Brush => new SolidColorBrush(Color.FromArgb(A, R, G, B));

        #endregion
    }
}

using System;
using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    public class ScheduleVm : ReactiveObject, ICustomColor
    {
    	private DateTime _startDate;
        public DateTime StartDate
        {
        	get { return _startDate; }
        	set { this.RaiseAndSetIfChanged(ref _startDate, value); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
        	get { return _endDate; }
            set { this.RaiseAndSetIfChanged(ref _endDate, value); }
        }

        private string _courseAndGroup;
        public string CourseAndGroup
        {
        	get { return _courseAndGroup; }
            set { this.RaiseAndSetIfChanged(ref _courseAndGroup, value); }
        }

        private string _name;
        public string Name
        {
        	get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        private string _location;
        public string Location
        {
        	get { return _location; }
            set { this.RaiseAndSetIfChanged(ref _location, value); }
        }

        private string _description;
        public string Description
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
}

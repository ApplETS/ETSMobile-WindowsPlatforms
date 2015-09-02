using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Ets.Mobile.Entities.Signets.Interfaces;
using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    [DataContract]
    public class CourseVm : ReactiveObject, ICustomColor
    {
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

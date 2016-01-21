using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    public class CourseIntervalVm : ReactiveObject
    {
        private string _acronym;
        public string Acronym 
        { 
            get { return _acronym; }
            set { this.RaiseAndSetIfChanged(ref _acronym, value); } 
        }

        private string _group;
        public string Group 
        { 
        	get { return _group; }
        	set { this.RaiseAndSetIfChanged(ref _group, value); }
        }

        private string _semester;
        public string Semester
        { 
        	get { return _semester; }
        	set { this.RaiseAndSetIfChanged(ref _semester, value); }
        }

        private string _program;
        public string Program
        { 
        	get { return _program; }
        	set { this.RaiseAndSetIfChanged(ref _program, value); }
        }

        private string _grade;
        public string Grade 
        { 
        	get { return _grade; }
        	set { this.RaiseAndSetIfChanged(ref _grade, value); }
        }

        private double _credits;
        public double Credits 
        { 
        	get { return _credits; } 
        	set { this.RaiseAndSetIfChanged(ref _credits, value); } 
        }

        private string _name;
        public string Name 
        { 
        	get { return _name; }
        	set { this.RaiseAndSetIfChanged(ref _name, value); }
        }
    }
}
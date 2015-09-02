using ReactiveUI;

namespace Ets.Mobile.Entities.Signets
{
    public class TeammateVm : ReactiveObject
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
    }
}

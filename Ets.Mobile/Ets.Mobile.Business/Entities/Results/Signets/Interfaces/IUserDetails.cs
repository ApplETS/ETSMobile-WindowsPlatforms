namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface IUserDetails
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        string PermanentCode { get; set; }
        string Balance { get; set; }
        bool IsMan { get; set; }
        string ErrorMessage { get; set; }
    }
}
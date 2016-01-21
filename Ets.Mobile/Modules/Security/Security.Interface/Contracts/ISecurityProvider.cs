namespace Security.Contracts
{
    public interface ISecurityProvider
    {
        string HashMd5(string str);
    }
}
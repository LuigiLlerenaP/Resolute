namespace Resolute.Contracts
{
    public interface IFault
    {
        string Code { get; }
        string Message { get; }
    }
}

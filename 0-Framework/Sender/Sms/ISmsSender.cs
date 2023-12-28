namespace _0_Framework.Sender.Sms
{
    public interface ISmsSender
    {
        Task<int> SendByKavenagarAsync(string message, string PhoneNumber);
    }
}

namespace BusinessCardManager.Domain.Exceptions
{
    public class InvalidPhoneException : Exception
    {
        public InvalidPhoneException(string message) : base(message) { }
    }
}

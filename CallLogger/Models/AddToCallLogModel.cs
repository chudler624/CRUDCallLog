namespace CallLogger.Models
{
    public class AddToCallLogModel
    {
        public DateTime TimeOfCall { get; set; }
        public string NameOfCaller { get; set; }
        public string CallerPhoneNumber { get; set; }
        public string IntendedRecipient { get; set; }
        public string MessageForRecipient { get; set; }
    }
}

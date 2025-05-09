namespace ADBDataExtractor.Models
{
    public class CallLog
    {
        public int ID { get; set; }
        public string PhoneNumber { get; set; }
        public string CallType { get; set; }
        public string Duration { get; set; }
    }
}
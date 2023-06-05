namespace PackingMachine.Core.Domain.Communication
{
    public class Error
    {
        public string NameEvent { get; set; }
        public string Message { get; set; }
        public Error (string nameEvent,string message)
        {
            NameEvent=nameEvent;
            Message=message;
        }
        public Error ( ) { }
    }
}

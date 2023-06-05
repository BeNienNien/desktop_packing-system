namespace PackingMachine.core.Domain.Model
{
    public class ErrorMessage
    {
        public string TimeStamp { get; set; }
        public string MachineID { get; set; }
        public string NameEvent { get; set; }
        public bool ACK { get; set; }

        public ErrorMessage (string timeStamp,string machineID,string nameEvent,bool ack)
        {
            TimeStamp=timeStamp;
            MachineID=machineID;
            NameEvent=nameEvent;
            ACK=ack;
        }
    }
}

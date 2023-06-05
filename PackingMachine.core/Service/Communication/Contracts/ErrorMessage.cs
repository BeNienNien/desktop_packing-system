using System;

namespace PackingSystemServiceContainers;

public class ErrorMessage
{
    public DateTime TimeStamp { get; set; }
    public string MachineID { get; set; }
    public string NameEvent { get; set; }
    public bool ACK { get; set; }
}

public class ErrorMqttMessage
{
    public DateTime TimeStamp { get; set; }
    public string MachineID { get; set; }
    public string NameEvent { get; set; }
    public bool ACK { get; set; }
}
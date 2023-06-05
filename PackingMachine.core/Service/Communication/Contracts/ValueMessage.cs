using System;

namespace PackingSystemServiceContainers;

public class ValueMessage
{
#pragma warning disable CS8618
    public string MachineId { get; set; }
    public DateTime Timestamp { get; set; }
    public int ItemId { get; set; }
    public int CompletedProduct { get; set; }
    public int ErrorProduct { get; set; }
    public double WorkingTime { get; set; }
    public double ExecutionTime { get; set; }
    public int SumActualPro { get; set; }
#pragma warning restore CS8618
}

public class ValueMqttMessage
{
    public DateTime Timestamp { get; set; }
    public int ItemId { get; set; }
    public int CompletedProduct { get; set; }
    public int ErrorProduct { get; set; }
    public double WorkingTime { get; set; }
    public double ExecutionTime { get; set; }
    public int SumActualPro { get; set; }
}
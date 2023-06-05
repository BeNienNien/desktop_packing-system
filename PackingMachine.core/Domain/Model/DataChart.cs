using System;

namespace PackingMachine.core.Domain.Model
{
    public class DataChart
    {
        public DateTime TimeStamp { get; set; }
        public Double ExecutionTime { get; set; }
        public string MachineId { get; set; }

        public DataChart (string machineId,DateTime timeStamp,double executionTime)
        {
            TimeStamp=timeStamp;
            ExecutionTime=executionTime;
            MachineId=machineId;

        }
    }
}

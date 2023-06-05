using MassTransit;
using PackingMachine.core.ViewModel.WarningViewModel;
using System;
using System.Threading.Tasks;

namespace PackingSystemServiceContainers;

public class ErrorMessageConsumer: IConsumer<ErrorMessage>
{
    public async Task Consume (ConsumeContext<ErrorMessage> context)
    {
        var message = context.Message;
        WarningViewModel.ErrorMessage(message);
        Console.WriteLine("ErrorMessage: MachineID: {0}, TimeStamp: {1}, NameEvent: {2}, ACK: {3}",message.MachineID,message.TimeStamp,message.NameEvent,message.ACK);
    }
}

using MassTransit;
using PackingMachine.core.ViewModel.MainSuperViewModel;
using PackingMachine.core.ViewModel.WarningViewModel;
using System;
using System.Threading.Tasks;

namespace PackingSystemServiceContainers;

public class ValueMessageConsumer: IConsumer<ValueMessage>
{
    public async Task Consume (ConsumeContext<ValueMessage> context)
    {
        var message = context.Message;
        SupervisorViewModel.Sender(message);
        EmployeeProViewModel.Sender(message);
        WarningViewModel.Sender(message);
        Console.WriteLine("ValueMessage: MachineId: {0}, Timestamp: {1}, ItemId: {2}, CompletedProduct: {3}, ErrorProduct: {4}, WorkingTime:{5}, ExecutionTime:{6}, SumActualPro: {7}",message.MachineId,message.Timestamp,message.ItemId,message.CompletedProduct,message.ErrorProduct,message.WorkingTime,message.ExecutionTime,message.SumActualPro);
    }
}

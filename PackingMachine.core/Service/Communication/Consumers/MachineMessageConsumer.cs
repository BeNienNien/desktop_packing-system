using MassTransit;
using PackingMachine.core.ViewModel.MainSuperViewModel;
using PackingMachine.core.ViewModel.SettingViewModel;
using System;
using System.Threading.Tasks;

namespace PackingSystemServiceContainers;

public class MachineMessageConsumer: IConsumer<MachineMessage>
{
    public async Task Consume (ConsumeContext<MachineMessage> context)
    {
        var message = context.Message;
        MainSettingViewModel.MachineMessage(message);
        SupervisorViewModel.MachineMessage(message);
        Console.WriteLine("MachineMessage: MachineId: {0}, Timestamp: {1}, MachineStatus: {2}",message.MachineId,message.Timestamp,message.MachineStatus);
    }
}

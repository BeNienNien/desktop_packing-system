using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PackingSystemServiceContainers;
using System;

namespace PackingMachine.HostBuilder
{
    public static class AddGRPC
    {
        public static IHostBuilder AddGRPCRead (this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                //services.AddSingleton<MQTTStore>();
                // services.AddSingleton<CycleMessageConsumer>((IServiceProvider serviceprovider) => { return new CycleMessageConsumer(serviceprovider.GetRequiredService<MQTTStore>()); });
                services.AddSingleton<IBusControl>((IServiceProvider serviceprovider) =>
                {
                    return Bus.Factory.CreateUsingGrpc(x =>
                {
                    x.Host(h =>
                    {
                        h.Host="127.0.0.1";
                        h.Port=8182;
                        h.AddServer(new Uri("http://127.0.0.1:8181"));
                    });

                    x.ReceiveEndpoint("event-listener",e =>
                    {
                        e.Consumer<ValueMessageConsumer>( );
                        e.Consumer<MachineMessageConsumer>( );
                        e.Consumer<ErrorMessageConsumer>( );
                    });
                });
                });
            });

            return host;
        }

    }
}

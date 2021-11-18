using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Yellfage.Wst.Communication;
using Yellfage.Wst.Interior;
using Yellfage.Wst.Interior.Communication;
using Yellfage.Wst.Interior.Connection;
using Yellfage.Wst.Interior.Disconnection;
using Yellfage.Wst.Interior.Filters;
using Yellfage.Wst.Interior.Invocation;
using Yellfage.Wst.Interior.Mapping;
using Yellfage.Wst.Interior.Notification;

namespace Yellfage.Wst
{
    public static class IServiceCollectionExtensions
    {
        public static IWstHubConfigurator<TMarker> AddWstHub<TMarker>(
            this IServiceCollection services)
        {
            services.AddHub<TMarker>();
            services.AddWorkers<TMarker>();

            services.AddServices<TMarker>();

            var builder = new WstHubConfigurator<TMarker>(services);

            builder.AddDefaultBus();
            builder.AddDefaultCache();
            builder.AddDefaultClientCache();

            return builder;
        }

        private static void AddServices<TMarker>(this IServiceCollection services)
        {
            services
                .AddSingleton<MarkerService<TMarker>>()
                .AddSingleton<IMessageTypeResolver<TMarker>, MessageTypeResolver<TMarker>>()
                .AddSingleton<IClientClaimsPrincipalFactory<TMarker>, ClientClaimsPrincipalFactory<TMarker>>()
                .AddSingleton<IClientManager<TMarker>, ClientManager<TMarker>>()
                .AddSingleton<IGroupManager<TMarker>, GroupManager<TMarker>>()
                .AddSingleton<IClientFactory<TMarker>, ClientFactory<TMarker>>()
                .AddSingleton<IMethodExecutor<TMarker>, MethodExecutor<TMarker>>()
                .AddSingleton<IAgreementFactory<TMarker>, AgreementFactory<TMarker>>()
                .AddSingleton<IAgreementRequestProcessor<TMarker>, AgreementRequestProcessor<TMarker>>()
                .AddSingleton<IInvocationMessageProcessorFactory<TMarker>, InvocationMessageProcessorFactory<TMarker>>()
                .AddSingleton<IMessageDeserializerFactory<TMarker>, MessageDeserializerFactory<TMarker>>()
                .AddSingleton<IMessageDispatcherFactory<TMarker>, MessageDispatcherFactory<TMarker>>()
                .AddSingleton<IMessageReceiverFactory<TMarker>, MessageReceiverFactory<TMarker>>()
                .AddSingleton<IMessageTransmitterFactory<TMarker>, MessageTransmitterFactory<TMarker>>()
                .AddSingleton<IProtocolProvider<TMarker>, ProtocolProvider<TMarker>>()
                .AddSingleton<IReceptionProvider<TMarker>, ReceptionProvider<TMarker>>()
                .AddSingleton<IConnectionProcessorFactory<TMarker>, ConnectionProcessorFactory<TMarker>>()
                .AddSingleton<IConnectionRequestProcessor<TMarker>, ConnectionRequestProcessor<TMarker>>()
                .AddSingleton<IClientDisconnectorFactory<TMarker>, ClientDisconnectorFactory<TMarker>>()
                .AddSingleton<IFilterExecutor<TMarker>, FilterExecutor<TMarker>>()
                .AddSingleton<IFilterExplorer<TMarker>, FilterExplorer<TMarker>>()
                .AddSingleton<IArgumentBinderFactory<TMarker>, ArgumentBinderFactory<TMarker>>()
                .AddSingleton<IArgumentConverterFactory<TMarker>, ArgumentConverterFactory<TMarker>>()
                .AddSingleton<IHandlerExecutorFactory<TMarker>, HandlerExecutorFactory<TMarker>>()
                .AddSingleton<IHandlerFactory<TMarker>, HandlerFactory<TMarker>>()
                .AddSingleton<IHandlerStore<TMarker>, HandlerStore<TMarker>>()
                .AddSingleton<IInvocationExecutorFactory<TMarker>, InvocationExecutorFactory<TMarker>>()
                .AddSingleton<INotifiableInvocationResponderFactory<TMarker>, NotifiableInvocationResponderFactory<TMarker>>()
                .AddSingleton<IRegularInvocationResponderFactory<TMarker>, RegularInvocationResponderFactory<TMarker>>()
                .AddSingleton<IHandlerMapper<TMarker>, HandlerMapper<TMarker>>()
                .AddSingleton<IHubMapper<TMarker>, HubMapper<TMarker>>()
                .AddSingleton<IWorkerMapper<TMarker>, WorkerMapper<TMarker>>()
                .AddSingleton<IClientNotifierFactory<TMarker>, ClientNotifierFactory<TMarker>>();
        }

        private static void AddHub<TMarker>(this IServiceCollection services)
        {
            foreach (Type type in Assembly.GetEntryAssembly()!.DefinedTypes)
            {
                if (typeof(Hub<TMarker>).IsAssignableFrom(type))
                {
                    services.AddSingleton(typeof(IHub<TMarker>), type);

                    return;
                }
            }

            throw new InvalidOperationException(
                $"Unable to add Hub with the \"{nameof(TMarker)}\" marker " +
                $"to the dependency injection container: the Hub not found");
        }

        private static void AddWorkers<TMarker>(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetEntryAssembly()!;

            foreach (Type type in assembly.DefinedTypes)
            {
                if (typeof(Worker<TMarker>).IsAssignableFrom(type))
                {
                    services.AddScoped(type);
                }
            }
        }
    }
}

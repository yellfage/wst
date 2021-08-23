using System;
using System.Threading;
using System.Threading.Tasks;

using Yellfage.Wst.Caching;
using Yellfage.Wst.Interior.Communication;
using Yellfage.Wst.Interior.Disconnection;

namespace Yellfage.Wst.Interior
{
    internal class Client<TMarker> : IClient<TMarker>
    {
        public string Id { get; }
        public IClientClaimsPrincipal User { get; }
        public IClientCache<TMarker> Cache { get; }

        private IMessageTransmitter MessageTransmitter { get; }
        private IClientDisconnector ClientDisconnector { get; }

        public Client(
            string id,
            IClientClaimsPrincipal user,
            IClientCache<TMarker> cache,
            IMessageTransmitter messageTransmitter,
            IClientDisconnector clientDisconnector)
        {
            Id = id;
            User = user;
            Cache = cache;
            MessageTransmitter = messageTransmitter;
            ClientDisconnector = clientDisconnector;
        }

        public async Task NotifyAsync(string handlerName, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, Array.Empty<object>(), cancellationToken);
        }

        public async Task NotifyAsync<TArgument1>(string handlerName, TArgument1 argument1, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2>(string handlerName, TArgument1 argument1, TArgument2 argument2, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3, TArgument4>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3, argument4 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3, argument4, argument5 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3, argument4, argument5, argument6 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, TArgument7 argument7, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3, argument4, argument5, argument6, argument7 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, TArgument7 argument7, TArgument8 argument8, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8, TArgument9>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, TArgument7 argument7, TArgument8 argument8, TArgument9 argument9, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9 }, cancellationToken);
        }

        public async Task NotifyAsync<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TArgument7, TArgument8, TArgument9, TArgument10>(string handlerName, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, TArgument7 argument7, TArgument8 argument8, TArgument9 argument9, TArgument10 argument10, CancellationToken cancellationToken = default)
        {
            await NotifyAsync(handlerName, new object?[] { argument1, argument2, argument3, argument4, argument5, argument6, argument7, argument8, argument9, argument10 }, cancellationToken);
        }

        public async Task NotifyAsync(string handlerName, object?[] arguments, CancellationToken cancellationToken = default)
        {
            if (handlerName is null)
            {
                throw new ArgumentNullException(nameof(handlerName));
            }

            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            var message = new OutgoingNotifiableInvocationMessage(handlerName, arguments);

            await MessageTransmitter.TransmitAsync(message, cancellationToken);
        }

        public async Task DisconnectAsync(CancellationToken cancellationToken = default)
        {
            await DisconnectAsync("", cancellationToken);
        }

        public async Task DisconnectAsync(string reason, CancellationToken cancellationToken = default)
        {
            await ClientDisconnector.DisconnectAsync(reason, cancellationToken);
        }
    }
}

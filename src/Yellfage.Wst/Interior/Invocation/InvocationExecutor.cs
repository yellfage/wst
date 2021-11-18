using System.Threading.Tasks;

using Yellfage.Wst.Interior.Filters;

namespace Yellfage.Wst.Interior.Invocation
{
    internal class InvocationExecutor<TMarker> : IInvocationExecutor<TMarker>
    {
        private IHandlerStore<TMarker> HandlerStore { get; }
        private IFilterExecutor<TMarker> FilterExecutor { get; }
        private IHandlerExecutor<TMarker> HandlerExecutor { get; }
        private IInvocationResponder<TMarker> InvocationResponder { get; }

        public InvocationExecutor(
            IHandlerStore<TMarker> handlerStore,
            IFilterExecutor<TMarker> filterExecutor,
            IHandlerExecutor<TMarker> handlerExecutor,
            IInvocationResponder<TMarker> invocationResponder)
        {
            HandlerStore = handlerStore;
            FilterExecutor = filterExecutor;
            HandlerExecutor = handlerExecutor;
            InvocationResponder = invocationResponder;
        }

        public async Task ExecuteAsync(IInvocationContext<TMarker> context)
        {
            if (HandlerStore.TryGet(context.HandlerName, out IHandler? handler))
            {
                try
                {
                    object? result = await FilterExecutor.ExecuteAsync(
                        handler.Filters,
                        context,
                        () => HandlerExecutor.ExecuteAsync(handler, context));

                    await InvocationResponder.ReplyAsync(context, result);
                }
                catch (ArgumentBindingException exception)
                {
                    await InvocationResponder.ReplyErrorAsync(context, exception.Message);
                }
                catch (InvocationException exception)
                {
                    await InvocationResponder.ReplyErrorAsync(context, exception.Message);
                }
                catch
                {
                    await InvocationResponder.ReplyErrorAsync(context, "An unknown error has occurred");

                    throw;
                }
            }
            else
            {
                await InvocationResponder.ReplyErrorAsync(
                    context,
                    $"The \"{context.HandlerName}\" handler not found");
            }
        }
    }
}

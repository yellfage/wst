using System.Reflection;
using System.Threading.Tasks;

namespace Yellfage.Wst.Internal
{
    internal class MethodExecutor
    {
        private MethodInfo Info { get; }
        private bool IsAwaitable { get; }

        public MethodExecutor(MethodInfo info)
        {
            Info = info;
            IsAwaitable = info.IsAwaitable();
        }

        public async Task<object?> ExecuteAsync(
            object? obj,
            params object?[] args)
        {
            dynamic? result = Info.Invoke(obj, args);

            if (IsAwaitable)
            {
                await result;

                return result!.GetAwaiter().GetResult();
            }

            return result;
        }
    }
}

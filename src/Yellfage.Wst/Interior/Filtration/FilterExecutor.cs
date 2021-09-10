using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Yellfage.Wst.Filtration;

namespace Yellfage.Wst.Interior.Filtration
{
    internal class FilterExecutor : IFilterExecutor
    {
        public async Task ExecuteAsync<TMarker>(
            IEnumerable<IFilter> filters,
            IInvocationExecutionContext<TMarker> context,
            Func<Task> endpoint)
        {
            await filters
                .Reverse()
                .Aggregate(endpoint, (next, filter) => () => filter.ApplyAsync(context, next))
                .Invoke();
        }
    }
}

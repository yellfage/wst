using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Yellfage.Wst.Filtration;

namespace Yellfage.Wst.Interior.Filtration
{
    internal class FilterExplorer : IFilterExplorer
    {
        public IEnumerable<IFilter> Explore(MemberInfo member)
        {
            return member
                .GetCustomAttributes()
                .OfType<IFilter>()
                .OrderBy(filter => filter.Priority);
        }
    }
}

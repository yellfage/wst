using System.Collections.Generic;

namespace Yellfage.Wst.Internal
{
    internal class HubClientManager<T> : ClientManager<T>, IHubClientManager<T>
    {
        public HubClientManager(IDictionary<string, IClient<T>> clients) : base(clients)
        {
        }
    }
}

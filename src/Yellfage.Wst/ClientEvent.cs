namespace Yellfage.Wst
{
    public abstract class ClientEvent<TMarker>
    {
        public IClientManager<TMarker> Target { get; }

        public ClientEvent(IClientManager<TMarker> target)
        {
            Target = target;
        }
    }
}

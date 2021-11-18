using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;

using Yellfage.Wst.Filters;

namespace Yellfage.Wst.Sample.Echo
{
    public class AuthorizationFilterAttribute : DefaultAuthorizationFilterAttribute
    {
        public override Task<object?> OnFailureAsync<TMarker>(
            IInvocationContext<TMarker> context,
            AuthorizationResult authorizationResult)
        {
            // Handle the authorization failure in your own way

            return base.OnFailureAsync(context, authorizationResult);
        }
    }
}

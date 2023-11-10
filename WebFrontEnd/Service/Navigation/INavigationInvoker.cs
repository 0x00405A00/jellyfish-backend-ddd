using Microsoft.AspNetCore.Components.Routing;

namespace WebFrontEnd.Service.Navigation
{
    public interface INavigationInvoker : IDisposable
    {
        void LocationChanged(object sender, LocationChangedEventArgs locationChangedEventArgs);
    }
}

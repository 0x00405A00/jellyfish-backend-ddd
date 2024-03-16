using Microsoft.JSInterop;
using Shared.Infrastructure.Backend.SignalR;

namespace WebFrontEnd.Service
{
    public static class JavascriptInvokableMethods
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider AddJavascriptInvokableMethods(this IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;//for creating scopes etc to consume di services inside static class
            return serviceProvider;
        }
        [JSInvokable]
        public static Task<int[]> ReturnArrayAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetService<SignalRClient>();
            return Task.FromResult(new int[] { 1, 2, 3 });
        }
        [JSInvokable]
        public static Task Void()
        {
            return Task.CompletedTask;
        }
    }
}

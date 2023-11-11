using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.DependencyInjection;
using WebFrontEnd.DataTransferObject.SessionStorage;
using WebFrontEnd.Service.WebStorage;

namespace WebFrontEnd.Service.Navigation
{
    public class ExtendedNavigationManager : IExtendedNavigationManager
    {
        private ISessionStorage _sessionStorage;

        private SessionDTO _session;
        private readonly IServiceProvider serviceProvider;

        public bool IsInitialized {  get; private set; }
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<INavigationInvoker> NavigationChangedSubscriber { get; private set; } = new List<INavigationInvoker>();


        public ExtendedNavigationManager(IServiceProvider serviceProvider, NavigationManager navigationManager, ISessionStorage sessionStorage) 
        {
            this.serviceProvider = serviceProvider;
            this._sessionStorage = sessionStorage;
            NavigationManager = navigationManager;
        }

        public async void Initialize()
        {
            if (IsInitialized)
                return;
            _session = await _sessionStorage.GetSession(CancellationToken.None);
            NavigationManager.RegisterLocationChangingHandler(OnLocationChanging);
            IsInitialized = true;
        }

        public ValueTask OnLocationChanging(LocationChangingContext context)
        {
            var data = _session.General;
            data.LastRoute = context.TargetLocation;
            _sessionStorage.SetSession(_session, CancellationToken.None);
            NavigationManagerLocationChanged(context.TargetLocation, new LocationChangedEventArgs(context.TargetLocation, context.IsNavigationIntercepted));
            return ValueTask.CompletedTask;
        }
        public void NavigationManagerLocationChanged(object sender, LocationChangedEventArgs locationChangedEventArgs)
        {
            foreach(var subscriber in NavigationChangedSubscriber)
            {
                subscriber.LocationChanged(sender, locationChangedEventArgs);
            }
        }

        public void Subscribe(INavigationInvoker navigationInvoker)
        {
            CheckInitializedState();
            if (NavigationChangedSubscriber.Contains(navigationInvoker))
                return;
            var list = NavigationChangedSubscriber.ToList();
            list.Add(navigationInvoker);    
            NavigationChangedSubscriber = list.AsEnumerable(); 
        }

        public void Unsubscribe(INavigationInvoker navigationInvoker)
        {
            CheckInitializedState();
            if (!NavigationChangedSubscriber.Contains(navigationInvoker))
                return;
            var list = NavigationChangedSubscriber.ToList();
            list.Remove(navigationInvoker);
            NavigationChangedSubscriber = list.AsEnumerable();
        }
        private void CheckInitializedState()
        {
            if (!IsInitialized)
            {
                throw new NotImplementedException($"service is not initialized, call {nameof(this.Initialize)} before use");
            }
        }
    }
    public interface IExtendedNavigationManager
    {
        public NavigationManager NavigationManager { get; set; }

        
        void Initialize();
        ValueTask OnLocationChanging(LocationChangingContext context);
        void NavigationManagerLocationChanged(object sender, LocationChangedEventArgs locationChangedEventArgs);
        void Subscribe(INavigationInvoker navigationInvoker);
        void Unsubscribe(INavigationInvoker navigationInvoker);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Service
{

    public class NavigationService : INavigation
    {
        private readonly IServiceProvider _services;

        protected INavigation Navigation
        {
            get
            {
                INavigation navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;
                if (navigation is not null)
                    return navigation;
                return null;
            }
        }

        public IReadOnlyList<Page> ModalStack
        {
            get
            {
                return Navigation.ModalStack;
            }
        }

        public IReadOnlyList<Page> NavigationStack
        {
            get
            {
                return Navigation.NavigationStack;
            }
        }

        public NavigationService(IServiceProvider services)
        {
            _services = services;
        }

        public Task NavigateToRootPage()
        {
            var page = _services.GetService<MainPage>();

            if (page is not null)
                return Navigation.PushAsync(page, true);

            throw new InvalidOperationException($"Unable to resolve type RootPage");
        }

        public void InsertPageBefore(Page page, Page before)
        {
            Navigation.InsertPageBefore(page, before);
        }

        public Task<Page> PopAsync()
        {
            return Navigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            return Navigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync()
        {
            return Navigation.PopAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return Navigation.PopAsync(animated);
        }

        public Task PopToRootAsync()
        {
            return Navigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            return Navigation.PopToRootAsync(animated);
        }

        public Task PushAsync(Page page)
        {
            Task t = null;
            try
            {
                t = Navigation.PushAsync(page);
            }
            catch(Exception ex)
            {

            }
            return t;
        }

        public Task PushAsync(Page page, bool animated)
        {
            return Navigation.PushAsync(page, animated);
        }

        public Task PushModalAsync(Page page)
        {
            return Navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return PushModalAsync(page, animated);
        }

        public void RemovePage(Page page)
        {
            Navigation.RemovePage(page);
        }
        public void CloseCurrentPage()
        {
            if (this.NavigationStack == null|| this.NavigationStack !=null&& this.NavigationStack.Count<2)
                return;

            var current = this.NavigationStack.ToList().Last();
            if(current != null)
            {
                this.RemovePage(current);
            }
        }
        public void PrevoiusPage(Page page)
        {
            var stackToList = Navigation.NavigationStack.ToList();
            var current = stackToList[stackToList.Count - 1];
            var prev = stackToList[stackToList.Count - 2];
            Navigation.RemovePage(page);
        }
    }
}

using System;

namespace Radio.Helpers
{
    public static class NavigationHelper
    {

        public static INavigationService NavigationService { get; set; }

    }

    public interface INavigationService
    {
        void Navigate(Type type);
    }
}

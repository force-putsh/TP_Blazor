using System;
namespace TP_Blazor.Shared
{
    public partial class NavMenu
    {
        public NavMenu()
        {
        }
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}


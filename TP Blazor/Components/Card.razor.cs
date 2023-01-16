using Microsoft.AspNetCore.Components;

namespace TP_Blazor.Components
{
    public partial class Card<TItem>
    {
        [Parameter]
        public RenderFragment<TItem> CardBody { get; set; }

        [Parameter]
        public RenderFragment<TItem> CardHeader { get; set; }

        [Parameter]
        public RenderFragment CardFooter { get; set; }
    }
}

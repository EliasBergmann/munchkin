using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Munchkin.Components
{
    public partial class SelectCardType : ComponentBase
    {
        [Parameter] public EventCallback<Type> CardTypeSelected { get; set; }

        private async Task OnCardTypeSelected(Type type)
        {
            await CardTypeSelected.InvokeAsync(type).ConfigureAwait(false);
        }
    }
}

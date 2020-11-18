using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace CookBook.Web.Shared
{
    public partial class OnlineStatusIndicator
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public RenderFragment OnlineFragment { get; set; }

        [Parameter]
        public RenderFragment OfflineFragment { get; set; }

        [Parameter]
        public bool IsOnline { get; set; }

        [Parameter]
        public EventCallback<bool> IsOnlineChanged { get; set; }

        [JSInvokable("OnlineStatus.StatusChanged")]
        public async Task OnStatusChanged(bool isOnline)
        {
            if (IsOnline != isOnline)
            {
                IsOnline = isOnline;
                OnlineStatusIndicator.IsOnlineStatic = isOnline;

                StateHasChanged();

                await IsOnlineChanged.InvokeAsync(IsOnline);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await jsRuntime.InvokeVoidAsync("OnlineStatus.Initialize", DotNetObjectReference.Create(this));
        }

        public async ValueTask DisposeAsync()
        {
            await jsRuntime.InvokeVoidAsync("OnlineStatus.Dispose");
        }

        public static bool IsOnlineStatic { get; set; }
    }
}
using CommunityToolkit.Maui.Core.Handlers;
using CommunityToolkit.Maui.Core.Views;

namespace MauiPopupInMemory;

public class MyPopupHandler : PopupHandler
{
    protected override void DisconnectHandler(MauiPopup platformView)
    {
        if (App.EnableLogicalChildRemoval)
        {
            (platformView.Control.VirtualView as ContentPage).Parent.RemoveLogicalChild(platformView.Control.VirtualView as Element);
            (platformView.Control.VirtualView as ContentPage).Parent = null;
        }
        
        base.DisconnectHandler(platformView);
    }
}
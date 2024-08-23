using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace MauiPopupInMemory;

public partial class MainPage : ContentPage
{
    // add to this collection on any page to see count
    // decrease when pages are popped.
    private static List<WeakReference> WeakReferences = new();
    
    public MainPage()
    {
        InitializeComponent();
        UpdateReferencesAliveLabelText();
        SetFixEnabledLabelText();
    }

    private void UpdateReferencesAliveLabelText()
    {
        int referencesAliveCount = WeakReferences.Count(weakReference => weakReference.IsAlive);
        ReferencesAliveLabel.Text = $"There {(referencesAliveCount > 1 ? "are" : "is" )} {referencesAliveCount} alive";
    }

    private void ShowTestPopup(object sender, EventArgs e)
    {
        var popup = new TestPopup(); 
        
        WeakReferences.Add(new WeakReference(popup));
        App.Current.MainPage.ShowPopup(popup);
        
        UpdateReferencesAliveLabelText();

        return;
    }

    private void CollectGarbage(object sender, EventArgs e)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Toast.Make("Collected Garbage").Show();

        UpdateReferencesAliveLabelText();
    }
    
    private void PushPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        
        UpdateReferencesAliveLabelText();
    }

    private void ToggleFix(object sender, EventArgs e)
    {
        App.EnableLogicalChildRemoval = !App.EnableLogicalChildRemoval;
        SetFixEnabledLabelText();
    }

    private void SetFixEnabledLabelText()
    {
        FixEnabledLabel.Text = App.EnableLogicalChildRemoval ? "Fix Enabled" : "Fix Disabled";
    }

}

public class TestPopup : Popup
{
    public TestPopup()
    {
        var content = new VerticalStackLayout()
        {
            Padding = new Thickness(50, 20)
        };
        
        content.Add(new Label { Text = "This is a test popup."});
        Content = content;
    }
}
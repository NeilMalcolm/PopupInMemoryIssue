namespace MauiPopupInMemory;

public partial class App : Application
{
    public static bool EnableLogicalChildRemoval = false;
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}
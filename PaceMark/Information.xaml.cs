namespace PaceMark;

public partial class Information : ContentPage
{
    public Information()
    {
        InitializeComponent();
    }

    async void OnRecClicked(object sender, EventArgs args)
    {
        await Browser.OpenAsync("https://www.youtube.com/watch?v=BnwdpR_2idA", BrowserLaunchMode.SystemPreferred);
    }
    async void OnDssClicked(object sender, EventArgs args)
    {
        await Launcher.OpenAsync("http://maps.google.com/?daddr=Dewan+Seri+Sarjana");
    }

}
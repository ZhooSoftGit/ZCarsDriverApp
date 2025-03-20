using CommunityToolkit.Maui.Views;
using ZCarsDriver.NavigationExtension;
using ZhooSoft.Core;

namespace ZCarsDriver.DPopup;

public partial class CustomPopup : Popup
{
	public CustomPopup()
	{
		InitializeComponent();
        BindingContext = new BookingRequestViewModel();
        if (BindingContext is BookingRequestViewModel vm)
        {
            vm.CurrentPopup = this;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        //Close();
        //ServiceHelper.GetService<IAppNavigation>().LaunchDriverDashBoard();
        if (BindingContext is BookingRequestViewModel vm)
        {
            vm.AcceptCommand.Execute(null);
        }
    }
}
using ZhooSoft.Auth.ViewModel;

namespace ZhooSoft.Auth.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
	}
}
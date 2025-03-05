using ZhooSoft.Auth.ViewModel;

namespace ZhooSoft.Auth.Views;

public partial class OTPVerificationPage : ContentPage
{
	public OTPVerificationPage()
	{
		InitializeComponent();
		BindingContext = new OTPVerificationViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is OTPVerificationViewModel viewModel)
        {
            viewModel.OnAppearing();
        }
    }
}
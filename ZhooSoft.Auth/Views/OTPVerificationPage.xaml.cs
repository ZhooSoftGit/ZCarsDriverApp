using System.Runtime.CompilerServices;
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

    private void otp1_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue != null && e.NewTextValue.Length > 0)
        {
            otp2.Focus();
        }
    }

    private void otp2_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue != null && e.NewTextValue.Length > 0)
        {
            otp3.Focus();
        }
    }

    private void otp3_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue != null && e.NewTextValue.Length > 0)
        {
            otp4.Focus();
        }
    }

    private void otp4_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (otp1.Text == string.Empty && otp2.Text == string.Empty && otp3.Text == string.Empty && otp4.Text == string.Empty)
        {
            otp1.Focus();
        }
    }
}
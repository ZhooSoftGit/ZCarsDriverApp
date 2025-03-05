using ZhooSoft.Auth.Views;

namespace ZCarsDriver
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var nvPage = new NavigationPage(new LoginPage());
          
            return new Window(nvPage);
        }
    }
}
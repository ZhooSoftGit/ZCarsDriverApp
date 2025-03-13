using ZhooSoft.Auth.Views;

namespace ZCarsDriver
{
    public partial class App : Application
    {
        #region Constructors

        public App()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var nvPage = new NavigationPage(new LoginPage());

            return new Window(nvPage);
        }

        private void CheckLogin()
        {
        }

        #endregion
    }
}

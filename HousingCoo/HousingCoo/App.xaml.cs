using HousingCoo.Presentation;
using Xamarin.Forms;
using Xamarin.Presentation.Navigation;

namespace HousingCoo {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            //var main = new MasterMenuPage();
            MainPage = new Presentation.Auth.SignInPage();
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}

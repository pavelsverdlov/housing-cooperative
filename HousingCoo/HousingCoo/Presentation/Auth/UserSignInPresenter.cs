using Xamarin.Forms;
using Xamarin.Presentation.Auth;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Pages.States;

namespace HousingCoo.Presentation.Auth {
    public class UserSignInPresenter : SignInPresenter {
        public IPageNavigator Page { get; }
        public PageStates State { get; }

        public UserSignInPresenter() {
            Page = new PageNavigatorViewModel();
            State = PageStates.Normal;
        }

        public override void OnLogIn() {
            Application.Current.MainPage = new MasterMenuPage();
        }
    }
}

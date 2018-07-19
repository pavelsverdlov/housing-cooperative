using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Voting {
    public class AddNewVotingViewState : BaseViewState, IAddNewActivityViewState {
        public string IconSource { get; set; }
        public string UserName { get; set; }
        public string Info { get; set; }
        public ButtonModel Add { get; set; }

        public AddNewVotingViewState() {
            IconSource = "icon.png";
            UserName = "UserName";
            Info = "Administrator";
            Add = new ButtonModel { IsVisible = true, Title = "Add" };
        }
    }

    public class AddNewVotingController : BaseController, IAddNewActivityController {
        public ICommand AddClickCommand { get; }
        public AddNewVotingController() {
            AddClickCommand = new Command(OnAdd);
        }

        private void OnAdd(object obj) {

        }
    }
    public class AddNewVotingPresenter : BasePresenter<AddNewVotingViewState, AddNewVotingController>,
        IPageNavigatorSupporting {
        //
        public IPageNavigator PageNavigator { get; }

        public AddNewVotingPresenter() {
            PageNavigator = new PageNavigatorViewModel {
                Title = "Add new voiting"
            };
        }
    }


}

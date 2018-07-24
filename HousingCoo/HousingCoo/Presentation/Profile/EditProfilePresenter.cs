using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using Xamarin.Forms;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social.Profiles;

namespace HousingCoo.Presentation.Profile {

    public class EditProfileController : BaseController {
        internal EditProfilePresenter Presenter;

        public Command SaveNewPerson { get; }

        public EditProfileController() {
            SaveNewPerson = new Command(OnSaveNewPerson);
        }

        private void OnSaveNewPerson(object obj) {
            Presenter.Save();
        }
    }

    public class EditProfileeViewState : BaseViewState, IEditProfileViewState {
        public string IconSource { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class EditProfilePresenter : BasePresenter<EditProfileeViewState, EditProfileController>, IPageNavigatorSupporting {
        readonly IPeopleAdd repo;
        PeopleModel model;

        public IPageNavigator PageNavigator { get; }
        public EditProfilePresenter() : this(Bootstrapper.Instance.Resolver.Get<IPeopleAdd>()) { }
        public EditProfilePresenter(IPeopleAdd repo) {
            PageNavigator = new PageNavigatorAdapter {
                Title = "Edit profile"
            };
            this.repo = repo;
        }
        protected override void Init(EditProfileeViewState vs, EditProfileController con) {
            base.Init(vs, con);
            con.Presenter = this;
        }

        internal void Save() {
            PeopleModel m = new Domain.Model.PeopleModel {
                Name = ViewState.FirstName
            };
            repo.Add(m);
            model = m;
            PageNavigator.Navigation.PopToRootAsync(true);
        }
    }


}

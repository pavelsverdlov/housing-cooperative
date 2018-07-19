using HousingCoo.Presentation.Profile;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Pages.Menu;

namespace HousingCoo.Presentation {
    public class MasterDetailController : Xamarin.Presentation.Pages.MasterDetailController {
        internal MasterDetailPresenter Presenter;

        public Command EditProfile { get; }

        public MasterDetailController() {
            EditProfile = new Command(OnEditProfile);
        }

        void OnEditProfile(object obj) {
            Presenter.EditProfile();
        }

        protected override void OnItemSelected(NavPageMenuItem item) {
            Presenter.MenuSelected(item);
        }
    }

    public class MasterDetailPresenter : Xamarin.Presentation.Pages.MasterDetailPresenter<MasterDetailController> {
        readonly IXamLogger logger;
        readonly ICommutator commutator;

        public MasterDetailPresenter() : this(
          Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
          Bootstrapper.Instance.Resolver.Get<ICommutator>()) { }
        public MasterDetailPresenter(IXamLogger logger, ICommutator commutator) {
            this.logger = logger;
            this.commutator = commutator;
            Page.IsPresented = false;
            Page.Title = "Housing Cooperative";
        }

        protected override void Init(MasterDetailViewState vs, MasterDetailController con) {
            base.Init(vs, con);
            var i = 0;
            var items = new List<NavPageMenuItem>(new[] {
                    new NavPageMenuItem { Id = i,   Title = "People" ,TargetType = typeof(People.PeopleListPage),
                        Image = "ic_people_black_18dp.png" },
                    new NavPageMenuItem { Id = ++i, Title = "Messages" ,TargetType = typeof(Messaging.PrivateMessageListPage),
                    Image = StaticResources.Icons.MessageBlack},
                    new NavPageMenuItem { Id = ++i, Title = "Notifications" ,TargetType = typeof(Notification.NotificationListPage),
                    Image ="baseline_notifications_black_18dp.png"},
                    //new NavPageMenuItem { Id = ++i, Title = "Cashbox" ,TargetType = typeof(Profile.EditProfilePage) },
                    //new NavPageMenuItem { Id = 0, Title = "Personal Information", TargetType = typeof(Profile.EditProfilePage) }
                });

            vs.Push((nameof(MasterDetailViewState.MenuItems), items));
            con.Presenter = this;
        }

        internal async void EditProfile() {
            try {
                await commutator.GoToPage<EditProfilePresenter>(Page.Navigation);
                ((MasterMenuPage)Application.Current.MainPage).IsPresented = false;
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }

        internal async void MenuSelected(NavPageMenuItem item) {
            try {
                await commutator.GoToPage(Page.Navigation, item.TargetType);
                //Page.IsPresented = false;
                ((MasterMenuPage)Application.Current.MainPage).IsPresented = false;
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }
    }
}

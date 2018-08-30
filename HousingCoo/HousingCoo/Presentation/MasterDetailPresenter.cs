using HousingCoo.Presentation.Profile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Pages.Menu;
using Xamarin.Presentation.Pages.Tab;

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

    public class TabPageSwither : Xamarin.Presentation.Framework.BaseNotify, ITabPresenter, ITabController {
        class TabPage : TabPageItem {
            public TabPage(IPageNavigator pageNavigator) : base(pageNavigator) {
            }
        }

        public ITabPage Content { get; private set; }
        public Command<string> TabSelected { get; }
        public IPageNavigator PageNavigator { get; }

        public TabPageSwither(IPageNavigator pageNavigator) {
            PageNavigator = pageNavigator;
            TabSelected = new Command<string>(OnTabSelected);
        }

        void OnTabSelected(string tab) {
            switch (int.Parse(tab)) {
                case 0:
                    OpenVotingPage();
                    break;
                case 1:
                    OpenPeoplePage();
                    break;
                case 2:
                    OpenPrivateMessagesPage();
                    break;
                case 3:
                    OpenNotificationPage();
                    break;
            }
            SetPropertyChanged(nameof(Content));
        }

        internal void OpenVotingPage() {
            PageNavigator.Title = "Voting";
            Content = new TabPage(PageNavigator) { Index = 0 };
            SetPropertyChanged(nameof(Content));
        }
        internal void OpenPeoplePage() {
            PageNavigator.Title = "People";
            Content = new TabPage(PageNavigator) { Index = 1 };
            SetPropertyChanged(nameof(Content));
        }
        internal void OpenPrivateMessagesPage() {
            PageNavigator.Title = "Private messages";
            Content = new TabPage(PageNavigator) { Index = 2 };
            SetPropertyChanged(nameof(Content));
        }
        internal void OpenNotificationPage() {
            PageNavigator.Title = "Notification";
            Content = new TabPage(PageNavigator) { Index = 3 };
            SetPropertyChanged(nameof(Content));
        }
    }

    public class MasterDetailPresenter : Xamarin.Presentation.Pages.MasterDetailPresenter<MasterDetailController>{

        readonly IXamLogger logger;
        readonly ICommutator commutator;

        public TabPageSwither TabPages { get; }

        public MasterDetailPresenter() : this(
          Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
          Bootstrapper.Instance.Resolver.Get<ICommutator>()) { }
        public MasterDetailPresenter(IXamLogger logger, ICommutator commutator) {
            this.logger = logger;
            this.commutator = commutator;
        //    Page.IsPresented = false;
            PageNavigator.Title = "Housing Cooperative";
            TabPages = new TabPageSwither(PageNavigator);
            TabPages.OpenVotingPage();
        }

        protected override void Init(MasterDetailViewState vs, MasterDetailController con) {
            base.Init(vs, con);
            var i = 0;
            //var items = new List<NavPageMenuItem>(new[] {
            //        new NavPageMenuItem { Id = i,   Title = "People" ,TargetType = typeof(People.PeopleListPage),
            //            Image = "ic_people_black_18dp.png" },
            //        new NavPageMenuItem { Id = ++i, Title = "Messages" ,TargetType = typeof(Messaging.PrivateMessageListPage),
            //            Image = StaticResources.Icons.MessageBlack},
            //        new NavPageMenuItem { Id = ++i, Title = "Notifications" ,TargetType = typeof(Notification.NotificationListPage),
            //            Image ="baseline_notifications_black_24dp.png"},
            //        //new NavPageMenuItem { Id = ++i, Title = "Cashbox" ,TargetType = typeof(Profile.EditProfilePage) },
            //        //new NavPageMenuItem { Id = 0, Title = "Personal Information", TargetType = typeof(Profile.EditProfilePage) }
            //    });

           // items.ForEach(x=> ViewState.MenuItems.Add(x));
            con.Presenter = this;
        }

        internal async void EditProfile() {
            try {
                await commutator.GoToPage<EditProfilePresenter>(PageNavigator.Navigation);
                ((MasterMenuPage)Application.Current.MainPage).IsPresented = false;
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }

        internal async void MenuSelected(NavPageMenuItem item) {
            try {
                //DispatcherEx.BeginRise(() => {
                
                var view = await Task.Run(() => commutator.GetView(item.TargetType));

                var nav = new NavigationPage(view);
                PageNavigator.UpdateDetail(nav);
                var pres = view.BindingContext as IPageNavigatorSupporting;
                pres?.PageNavigator.UpdateNavigation(nav);

                await Task.Delay(30);

                PageNavigator.IsPresented = false;

                //((MasterMenuPage)Application.Current.MainPage).IsPresented = false;
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }



    }
}

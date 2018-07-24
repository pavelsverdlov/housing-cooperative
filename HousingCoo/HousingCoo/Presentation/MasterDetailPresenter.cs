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
        class TabPage : ITabPage {
            public int Index { get; set; }
        }

        public ITabPage Content { get { return current; } }
        public Command<string> TabSelected { get; }

        TabPage current;
        readonly IMasterDetailPageNavigator page;

        public TabPageSwither(IMasterDetailPageNavigator page) {
            TabSelected = new Command<string>(OnTabSelected);
            this.page = page;
            OnTabSelected("0");
        }

        void OnTabSelected(string tab) {
            current = new TabPage { Index = int.Parse(tab) };

            switch (current.Index) {
                case 0:
                    page.Title = "Voting";
                    break;
                case 1:
                    page.Title = "People";
                    break;
            }

            SetPropertyChanged(nameof(Content));
        }
    }

    public class MasterDetailPresenter : Xamarin.Presentation.Pages.MasterDetailPresenter<MasterDetailController>{

        readonly IXamLogger logger;
        readonly ICommutator commutator;

        public TabPageSwither TabPages { get;}

        public MasterDetailPresenter() : this(
          Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
          Bootstrapper.Instance.Resolver.Get<ICommutator>()) { }
        public MasterDetailPresenter(IXamLogger logger, ICommutator commutator) {
            this.logger = logger;
            this.commutator = commutator;
        //    Page.IsPresented = false;
            Page.Title = "Housing Cooperative";
            TabPages = new TabPageSwither(Page);
        }

       

        protected override void Init(MasterDetailViewState vs, MasterDetailController con) {
            base.Init(vs, con);
            var i = 0;
            var items = new List<NavPageMenuItem>(new[] {
                    new NavPageMenuItem { Id = ++i, Title = "Votings" ,TargetType = typeof(Voting.VoitingListPage),
                        Image = "baseline_how_to_vote_black_24dp.png"},
                    new NavPageMenuItem { Id = i,   Title = "People" ,TargetType = typeof(People.PeopleListPage),
                        Image = "ic_people_black_18dp.png" },
                    new NavPageMenuItem { Id = ++i, Title = "Messages" ,TargetType = typeof(Messaging.PrivateMessageListPage),
                        Image = StaticResources.Icons.MessageBlack},
                    new NavPageMenuItem { Id = ++i, Title = "Notifications" ,TargetType = typeof(Notification.NotificationListPage),
                        Image ="baseline_notifications_black_24dp.png"},
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
                //DispatcherEx.BeginRise(() => {
                
                var view = await Task.Run(() => commutator.GotView(item.TargetType));

                var nav = new NavigationPage(view);
                Page.UpdateDetail(nav);
                var pres = view.BindingContext as IPageNavigatorSupporting;
                pres?.PageNavigator.UpdateNavigation(nav);

                await Task.Delay(30);

                Page.IsPresented = false;

                //((MasterMenuPage)Application.Current.MainPage).IsPresented = false;
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }



    }
}

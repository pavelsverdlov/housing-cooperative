using System;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Messaging {
    public class PrivateMessListController : BaseController, IItemSelectedController<PeopleViewState> {
        internal PrivateMessListPresenter Presenter;
        public Command<PeopleViewState> ItemSelectedCommand { get; }

        public PrivateMessListController() {
            ItemSelectedCommand = new Command<PeopleViewState>(OnItemSelected);
        }

        private void OnItemSelected(PeopleViewState vs) {
            Presenter.ShowPrivateMessagingWith(vs);
        }
    }
    public class PrivateMessListViewState : CollectionViewState<PeopleViewState> {
        public PrivateMessListViewState() {
            for (int i = 0; i < 10; ++i) {
                ViewCollection.Add(new PeopleViewState { IconSource = "person.png", Name = "Some Friend " + i, Info = "Last message text ..." + i });
            }
        }
    }
    public class PrivateMessListPresenter : BasePresenter<PrivateMessListViewState, PrivateMessListController>,
        IPageNavigatorSupporting {
        readonly IXamLogger logger;
        readonly ICommutator commutator;

        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator PageNavigator { get; }

        public PrivateMessListPresenter() : this(
          Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
          Bootstrapper.Instance.Resolver.Get<ICommutator>()) { }
        public PrivateMessListPresenter(IXamLogger logger, ICommutator commutator) {
            PageNavigator = new PageNavigatorViewModel() {
                IconSource = StaticResources.Icons.MessageWhite,
                Title = "Private messages"
            };
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;
            this.logger = logger;
            this.commutator = commutator;
        }

        protected override void Init(PrivateMessListViewState vs, PrivateMessListController con) {
            base.Init(vs, con);
            con.Presenter = this;
        }

        private void OnListRefreshed() {

        }

        public async void ShowPrivateMessagingWith(PeopleViewState vs) {
            try {
                var vm = await commutator.GoToPage<PrivateMessagingPresenter>(PageNavigator.Navigation);
                vm.ShowMessagingWith(vs);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }
    }
}

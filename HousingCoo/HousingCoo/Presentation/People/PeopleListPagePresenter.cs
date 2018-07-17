using System;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.People {
    public class PeopleListController : BaseController, IItemSelectedController<PeopleViewState> {
        internal PeopleListPagePresenter Presenter;
        public Command<PeopleViewState> ItemSelectedCommand { get; }

        public PeopleListController() {
            ItemSelectedCommand = new Command<PeopleViewState>(OnItemSelected);
        }
        void OnItemSelected(PeopleViewState vs) {
            Presenter.ShowPeoplePreview(vs);
        }
    }
    public class PeopleListViewState : CollectionViewState <PeopleViewState> {
        public PeopleListViewState() {
            for(var i = 0; i < 10; ++i) {
                ViewCollection.Add(new PeopleViewState { IconSource = "person.png", Name = "Name" + i, Info = "some info" + i });
            }
        }
    }
    public class PeopleListPagePresenter : BasePresenter<PeopleListViewState, PeopleListController>, IPageNavigatorSupporting {
        readonly IXamLogger logger;
        readonly ICommutator commutator;

        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator Page { get; }

        public PeopleListPagePresenter() : this(
          Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
          Bootstrapper.Instance.Resolver.Get<ICommutator>()) { }
        public PeopleListPagePresenter(IXamLogger logger, ICommutator commutator) {
            Page = new PageNavigatorViewModel() { IconSource = StaticResources.Icons.PeopleWhite };
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;
            this.logger = logger;
            this.commutator = commutator;
        }

        protected override void Init(PeopleListViewState vs, PeopleListController con) {
            base.Init(vs, con);
            con.Presenter = this;
        }

        void OnListRefreshed() {
            
        }

        internal async void ShowPeoplePreview(PeopleViewState vs) {
            try {
                var pres = await commutator.GoToPage<PreviewPeoplePresenter>(Page.Navigation);
                pres.ShowPeople(vs);
            } catch(Exception ex) {
                logger.Error(ex);
            }
        }
    }
}

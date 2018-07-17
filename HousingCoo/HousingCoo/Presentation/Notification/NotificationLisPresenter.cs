using Xamarin.Forms;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Notification {
    public class NotificationListController : BaseController, IItemSelectedController<CommentViewState> {
        public Command<CommentViewState> ItemSelectedCommand { get; }

        public NotificationListController() {
            ItemSelectedCommand = new Command<CommentViewState>(OnItemSelected);
        }
        void OnItemSelected(CommentViewState obj) {

        }
    }
    public class NotificationListViewState : CollectionViewState<CommentViewState> {
        public NotificationListViewState() {
            for (var i = 0; i < 10; ++i) {
                ViewCollection.Add(new CommentViewState { IconSource = "payment.png", Message = "School for March 2018",
                    Title = $"Monthly payment 1{i}0$" });
            }
        }
    }
    public class NotificationLisPresenter : BasePresenter<NotificationListViewState, NotificationListController> {
        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator Page { get; }
        public NotificationLisPresenter() {
            Page = new PageNavigatorViewModel() { IconSource = StaticResources.Icons.StarWhite };
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;
        }

        void OnListRefreshed() {

        }
    }
}

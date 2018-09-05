using System.Collections.Generic;
using System.Linq;
using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Pages.Tab;
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
        }
    }
    public class NotificationListPresenter : BasePresenter<NotificationListViewState, NotificationListController>,
        INotificationConsumer, IPageNavigatorSupporting, ITabPageNavigatorUpdating {
        readonly IXamLogger logger;
        readonly INotificationProducer producer;

        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator PageNavigator { get; set; }

        public AccountModel Account { get; }


        public NotificationListPresenter() : this(
           Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
           Bootstrapper.Instance.Resolver.Get<INotificationProducer>()) { }
        public NotificationListPresenter(IXamLogger logger, INotificationProducer producer) {
            //PageNavigator = new PageNavigatorAdapter() {
            //    IconSource = StaticResources.Icons.StarGold,
            //    Title = "Notifications"
            //};
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;

            //TODO: get account from domain layer
            Account = new AccountModel();
            this.logger = logger;
            this.producer = producer;

            producer.SubstrubeTo(this);
        }

        void OnListRefreshed() {

        }
        int index = 0;
      

        public void OnNotificationsReceived(IEnumerable<NotificationModel> comments) {
            //this.Page.Navigation.se
            PageNavigator.IconSource = StaticResources.Icons.StarWhite;
            comments.ForEach(x => ViewState.ViewCollection.Insert(0, new CommentViewState {
                IconSource = index % 2 == 0 ? "payment.png" : "add_persone_notification.png",
                Message = index % 2 == 0 ? x.Message : "Vasy Pupkin joined",
                Title = index % 2 == 0 ? x.Title : "Today new Person joined"
            }));
            index++;
        }

        public void NavigatorPageChanged()
        {
            DispatcherEx.BeginRise(() => {
                PageNavigator.ToolbarMenu.Clear();
            });
        }
    }
}

using HousingCoo.Presentation.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Messaging
{
    public class PrivateMessListController : BaseController, IItemSelectedController<PeopleViewState> {
        public Command<PeopleViewState> ItemSelectedCommand { get; }

        public PrivateMessListController() {
            ItemSelectedCommand = new Command<PeopleViewState>(OnItemSelected);
        }
        void OnItemSelected(PeopleViewState obj) {

        }
    }
    public class PrivateMessListViewState : CollectionViewState<PeopleViewState> {
        public PrivateMessListViewState() {
            for (var i = 0; i < 10; ++i) {
                ViewCollection.Add(new PeopleViewState { IconSource = "icon.png", Name = "Some Friend " + i, Info = "Last message text ..." + i });
            }
        }
    }
    public class PrivateMessListPresenter : BasePresenter<PrivateMessListViewState, PrivateMessListController> {
        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator Page { get; }
        public PrivateMessListPresenter() {
            Page = new PageNavigatorViewModel() {IconSource = StaticResources.Icons.MessageWhite };
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;
        }

        void OnListRefreshed() {

        }
    }
}

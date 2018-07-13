using HousingCoo.Presentation.Common;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;

namespace HousingCoo.Presentation.People {
    public class PeopleListController : BaseController, IItemSelectedController<PeopleViewModel> {
        public Command<PeopleViewModel> ItemSelectedCommand { get; }

        public PeopleListController() {
            ItemSelectedCommand = new Command<PeopleViewModel>(OnItemSelected);
        }
        void OnItemSelected(PeopleViewModel obj) {
            
        }
    }
    public class PeopleListViewState : CollectionViewState <PeopleViewModel> {
        public PeopleListViewState() {
            for(var i = 0; i < 10; ++i) {
                ViewCollection.Add(new PeopleViewModel { IconSource = "icon.png", Name = "Name" + i, Info = "some info" + i });
            }
        }
    }

    public class PeopleViewModel {
        public string IconSource { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

    }
    public class PeopleListPagePresenter : BasePresenter<PeopleListViewState, PeopleListController> {
        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator Page { get; }
        public PeopleListPagePresenter() {
            Page = new PageNavigatorViewModel() { Title = "People" };
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;
        }

        void OnListRefreshed() {
            
        }
    }


}

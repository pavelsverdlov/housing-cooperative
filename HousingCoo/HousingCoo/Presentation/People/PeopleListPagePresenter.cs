﻿using HousingCoo.Presentation.Common;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.People {
    public class PeopleListController : BaseController, IItemSelectedController<PeopleViewState> {
        public Command<PeopleViewState> ItemSelectedCommand { get; }

        public PeopleListController() {
            ItemSelectedCommand = new Command<PeopleViewState>(OnItemSelected);
        }
        void OnItemSelected(PeopleViewState obj) {
            
        }
    }
    public class PeopleListViewState : CollectionViewState <PeopleViewState> {
        public PeopleListViewState() {
            for(var i = 0; i < 10; ++i) {
                ViewCollection.Add(new PeopleViewState { IconSource = "icon.png", Name = "Name" + i, Info = "some info" + i });
            }
        }
    }
    public class PeopleListPagePresenter : BasePresenter<PeopleListViewState, PeopleListController> {
        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator Page { get; }
        public PeopleListPagePresenter() {
            Page = new PageNavigatorViewModel() { IconSource = StaticResources.Icons.PeopleWhite };
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;
        }

        void OnListRefreshed() {
            
        }
    }
}

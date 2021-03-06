﻿using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using HousingCoo.Presentation.Common;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Pages.Tab;
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
        }
    }
    public class PrivateMessListPresenter : HousingDefCollectionPresenter<PrivateMessListViewState, PrivateMessListController, PeopleViewState>,
        IMessagingPeopleListConsumer , ITabPageNavigatorUpdating {
        readonly IMessagingPeopleListProducer producer;

        public PeopleModel Account { get; }
        public PeopleModel MessageTo { get; private set; }

        public PrivateMessListPresenter() : this(Bootstrapper.Instance.Resolver.Get<IMessagingPeopleListProducer>()) { }
        public PrivateMessListPresenter(IMessagingPeopleListProducer producer) {
            PageNavigator.IconSource = StaticResources.Icons.MessageWhite;
            PageNavigator.Title = "Private messages";

            Account = new PeopleModel();
            MessageTo = new PeopleModel();
            this.producer = producer;
            producer.Receive(this);
        }
        // <ToolbarItem Icon="ic_message_white_24dp.png" Command="{Binding Source={x:Reference Name=page}, Path=BindingContext.Controller.SendMessage}" />
        protected override void Init(PrivateMessListViewState vs, PrivateMessListController con) {
            base.Init(vs, con);
            con.Presenter = this;
        }

        protected override void OnListRefreshed() {
            producer.Receive(this);
        }

        public async void ShowPrivateMessagingWith(PeopleViewState vs) {
            try {
                var vm = await commutator.GoToPage<PrivateMessagingPresenter>(PageNavigator.Navigation);
                vm.ShowMessagingWith(vs);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }

        public void OnReceived(IEnumerable<PeopleModel> mess) {
            mess.ForEach(x => ViewState.ViewCollection.Add(new PeopleViewState {
                IconSource = "person.png", Name = x.Name, Info = "last message text" }));
        }

        public void NavigatorPageChanged() {
            DispatcherEx.BeginRise(() => {
                PageNavigator.ToolbarMenu.Clear();
            });
        }
    }
}

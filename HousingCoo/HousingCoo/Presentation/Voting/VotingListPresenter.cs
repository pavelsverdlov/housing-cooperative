using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Voting {
    public class VotingListController : BaseController, IActivityHeaderController, IItemSelectedController<VotingHeaderPresenter> {
        public Command<VotingHeaderPresenter> ItemSelectedCommand { get; }
        public ICommand AddNewVoting { get; }

        public Command<ButtonModel> ClickCommand { get; }

        public VotingListPresenter Presenter;
        public VotingListController() {
            ClickCommand = new Command<ButtonModel>(OnClick);
            ItemSelectedCommand = new Command<VotingHeaderPresenter>(OnItemSelected);
            AddNewVoting = new Command(OnAddNewVoting);
        }

        private void OnAddNewVoting(object _) {
            Presenter.OpenAddNewVoitingPage();
        }

        private void OnItemSelected(VotingHeaderPresenter vm) {
            Presenter.OnVotingSelected(vm.ViewState, vm.Model);
        }

        private void OnClick(ButtonModel obj) {

        }
    }
    public class VotingListViewState : CollectionViewState<VotingHeaderPresenter> {

    }

    public class VotingHeaderPresenter : ActivityHeaderPresenter<VotingListController> {
        public VotingModel Model { get; }
        public VotingHeaderPresenter(VotingModel item) {
            this.Model = item;
        }
    }
    public class VotingListPresenter : BasePresenter<VotingListViewState, VotingListController>,
        IVotingListConsumer, IPageNavigatorSupporting {
        readonly ICommutator commutator;
        readonly IVotingListProducer sender;
        readonly IXamLogger logger;
        public ListViewPullToRefreshViewModel PullToRefresh { get; }
        public IPageNavigator PageNavigator { get; }
        public VotingListPresenter() : this(
            Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
            Bootstrapper.Instance.Resolver.Get<ICommutator>(),
            Bootstrapper.Instance.Resolver.Get<IVotingListProducer>()) { }
        public VotingListPresenter(IXamLogger logger, ICommutator commutator, IVotingListProducer sender) {
            this.logger = logger;
            this.commutator = commutator;
            this.sender = sender;
            PageNavigator = new PageNavigatorViewModel() {
                IconSource = StaticResources.Icons.HomeWhite,
                Title = "Voiting"
            };
            Controller.Presenter = this;
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnListRefreshed;
            //  PullToRefresh.IsRefreshing = true;
            sender.ReceiveNextPage(this);
        }
        public void OnPageReceived(IEnumerable<VotingModel> votings) {
            var list = new List<VotingHeaderPresenter>();
            foreach (var item in votings) {
                var vm = new VotingHeaderPresenter(item);
                vm.ViewState.Push(
                    (nameof(ActivityViewState.Title), item.Title),
                    (nameof(ActivityViewState.ActorName), item.ActorName),
                    (nameof(ActivityViewState.Body), item.Body),
                    (nameof(ActivityViewState.Dates), new ActivityDatesState {
                        DateCreated = item.DateOpened.ToString("MM dd"),
                        DateClosed = item.DateClosed.ToString("MM dd"),
                    }),
                    (nameof(ActivityViewState.Verb), item.Verb)
                );
                ViewState.ViewCollection.Add(vm);
            }
            //PullToRefresh.IsRefreshing = false;
        }
        private void OnListRefreshed() {

        }
        public async void OnVotingSelected(ActivityViewState viewState, VotingModel model) {
            try {
                var vm = await commutator.GoToPage<VotingDetailPresenter>(PageNavigator.Navigation);
                vm.ShowVoting(viewState, model);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }
        public async void OpenAddNewVoitingPage() {
            try {
                await commutator.GoToPage<AddNewVotingPresenter>(this.PageNavigator.Navigation);
            } catch (Exception ex) {
                logger.Error(ex);
            }
        }
    }
}

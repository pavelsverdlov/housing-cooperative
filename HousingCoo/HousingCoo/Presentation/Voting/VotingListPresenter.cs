using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using HousingCoo.Presentation.Common;
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

    public class VotingListPresenter : HousingDefCollectionPresenter<VotingListViewState, VotingListController, VotingHeaderPresenter>,
        IVotingListConsumer, IPageNavigatorSupporting {

        readonly IVotingListProducer sender;


        public int Index => 0;

        public VotingListPresenter() : this(Bootstrapper.Instance.Resolver.Get<IVotingListProducer>()) { }
        public VotingListPresenter(IVotingListProducer sender) {
            this.sender = sender;
            PageNavigator.Title = "Voiting";
            PageNavigator.ToolbarMenu = new List<ToolbarItem> {
                new ToolbarItem(){
                    Icon = "baseline_create_white_24dp.png",
                    Command = Controller.AddNewVoting
                }
            };
            Controller.Presenter = this;
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
                        DateCreated = item.DateOpened.ToString("MM/dd/yyyy"),
                        DateClosed = item.DateClosed.ToString("MM/dd/yyyy"),
                    }),
                    (nameof(ActivityViewState.Verb), item.Verb)
                );
                vm.ViewState.ForcePush(nameof(ActivityViewState.Header));

                ViewState.ViewCollection.Insert(0, vm);               
            }
            //PullToRefresh.IsRefreshing = false;
        }

        protected override void OnListRefreshed() {
            sender.ReceiveNextPage(this);
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

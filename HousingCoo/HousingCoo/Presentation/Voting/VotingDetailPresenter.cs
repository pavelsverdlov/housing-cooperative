using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Voting {
    public class VotingController : BaseController, IActivityDetailController, IActivityHeaderController {
        public VotingDetailPresenter VM;

        public Command<ButtonModel> ClickCommand { get; }
        public Command<CommentViewState> ItemSelectedCommand { get; }

        

        public VotingController() {
            
            ClickCommand = new Command<ButtonModel>(OnClickCommand);
            ItemSelectedCommand = new Command<CommentViewState>(OnItemSelected);
        }

     

        private void OnItemSelected(CommentViewState obj) {

        }

        private void OnClickCommand(ButtonModel obj) {

        }
    }
    public class VotingViewState : BaseViewState {

    }

    public class VotingDetailPresenter : BasePresenter<VotingViewState, VotingController>,
        IActivityDetailsView<VotingController, VotingController>,

        IVotingCommentsConsumer {
        public ActivityDetailPresenter<VotingController> DetailViewModel { get; }
        public ActivityHeaderPresenter<VotingController> HeaderViewModel { get; }
        public IPageNavigator PageNavigator { get; }
        public ListViewPullToRefreshViewModel PullToRefresh { get; }

        public VotingModel Model { get; private set; }

        private readonly IVotingCommentsProducer sender;
        public VotingDetailPresenter() : this(Bootstrapper.Instance.Resolver.Get<IVotingCommentsProducer>()) { }

        public VotingDetailPresenter(IVotingCommentsProducer sender) {
            this.sender = sender;
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnPullToRefreshed;
            PageNavigator = new PageNavigatorViewModel();
            HeaderViewModel = new ActivityHeaderPresenter<VotingController>();
            DetailViewModel = new ActivityDetailPresenter<VotingController>();
            DetailViewModel.Controller.VM = this;
        }

        private void OnPullToRefreshed() {

        }
        public void ShowVoting(ActivityViewState viewState, VotingModel model) {
            this.Model = model;
            PageNavigator.Title = viewState.Title;
            HeaderViewModel.ViewState.Push(viewState);
            PullToRefresh.IsRefreshing = true;
            sender.ReceiveComments(this);
        }

        public void OnCommentsReceived(IEnumerable<CommentVotingModel> comments) {
            var list = new ObservableCollection<CommentViewState>();
            foreach (var item in comments) {
                var vs = new CommentViewState {
                    IconSource = "icon.png",
                    Title = item.Actor,
                    Message = item.Message
                };
                list.Add(vs);
            }
            DetailViewModel.ViewState.Push((nameof(DetailViewModel.ViewState.Comments), list));
            PullToRefresh.IsRefreshing = false;
        }
    }
}

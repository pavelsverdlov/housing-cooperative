using HousingCoo.Domain.Interactors;
using HousingCoo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Navigation;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social;
using Xamarin.Presentation.Social.Comments;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Voting {
    public class VotingController : BaseController, IActivityDetailController, IActivityHeaderController {
        public VotingDetailPresenter Presenter;

        public Command<ButtonModel> ClickCommand { get; }
        public Command<CommentViewState> ItemSelectedCommand { get; }
        public Command<Entry> CommentAdded { get; }
        public Command CameraActivated { get; }
        public Command AttachFileActivated { get; }

        public VotingController() {
            ClickCommand = new Command<ButtonModel>(OnClickCommand);
            ItemSelectedCommand = new Command<CommentViewState>(OnItemSelected);
            CommentAdded = new Command<Entry>(OnCommentAdded);
            CameraActivated = new Command(OnCameraActivated);
            AttachFileActivated = new Command(OnAttachFileActivated);
        }

        void OnAttachFileActivated(object obj) {

        }

        void OnCameraActivated(object obj) {

        }

        void OnCommentAdded(Entry entry) {
            Presenter.AddNewComment(entry.Text);
            entry.Text = null;
        }

        void OnItemSelected(CommentViewState obj) {

        }

        void OnClickCommand(ButtonModel obj) {

        }
    }
    public class VotingViewState : BaseViewState {
      
    }

    public class VotingDetailPresenter : BasePresenter<VotingViewState, VotingController>,
        IActivityDetailsView<VotingController, VotingController>,
        IVotingCommentsConsumer {

        public ActivityDetailPresenter<VotingController> DetailViewModel { get; }
        public ActivityHeaderPresenter<VotingController> HeaderViewModel { get; }
        public IPageNavigator Page { get; }
        public ListViewPullToRefreshViewModel PullToRefresh { get; }

        public VotingModel Model { get; private set; }

        readonly IVotingCommentsProducer sender;

        public VotingDetailPresenter() : this(
            Bootstrapper.Instance.Resolver.Get<IVotingCommentsProducer>()) { }

        public VotingDetailPresenter(IVotingCommentsProducer sender) {
            this.sender = sender;
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnPullToRefreshed;
            Page = new PageNavigatorViewModel();
            HeaderViewModel = new ActivityHeaderPresenter<VotingController>();
            DetailViewModel = new ActivityDetailPresenter<VotingController>();
            DetailViewModel.Controller.Presenter = this;
        }

        private void OnPullToRefreshed() {

        }
        public void ShowVoting(ActivityViewState viewState, VotingModel model) {
            this.Model = model;
            Page.Title = viewState.Title;
            HeaderViewModel.ViewState.Push(viewState);
            PullToRefresh.IsRefreshing = true;
            sender.ReceiveComments(this);
        }

        public void OnCommentsReceived(IEnumerable<CommentVotingModel> comments) {
            var list = new ObservableCollection<CommentViewState>();
            foreach (var item in comments) {
                var vs = new CommentViewState {
                    IconSource = "person.png",
                    Title = item.Actor,
                    Message = item.Message
                };
                list.Add(vs);
            }
            DetailViewModel.ViewState.Push((nameof(DetailViewModel.ViewState.Comments), list));
            PullToRefresh.IsRefreshing = false;
        }

        internal void AddNewComment(string message) {
            var prev = DetailViewModel.ViewState.Comments;
            prev.Add(new CommentViewState {
                IconSource = "person.png",
                Title = "Test additing message",
                Message = message
            });
            DetailViewModel.ViewState.Push((nameof(DetailViewModel.ViewState.Comments), prev));
        }
    }
}

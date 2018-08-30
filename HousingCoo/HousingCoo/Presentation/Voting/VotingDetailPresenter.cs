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

        public ICommand ClickCommand { get; }
        public Command<CommentViewState> ItemSelectedCommand { get; }
        public Command<Entry> CommentAdded { get; }
        public Command CameraActivated { get; }
        public ICommand ItemAppeared { get; }

        public VotingController() {
            ClickCommand = new Command<object>(OnClickCommand);
            ItemSelectedCommand = new Command<CommentViewState>(OnItemSelected);
            CommentAdded = new Command<Entry>(OnCommentAdded);
            CameraActivated = new Command(OnCameraActivated);
            ItemAppeared = new Command(OnItemAppeared);
        }

        void OnItemAppeared(object obj) {

        }

        void OnCameraActivated(object obj) {

        }

        void OnCommentAdded(Entry entry) {
            Presenter.AddNewComment(entry.Text);
            entry.Text = null;
        }

        void OnItemSelected(CommentViewState obj) {

        }

        void OnClickCommand(object obj) {

        }
    }
    public class VotingViewState : BaseViewState {
      
    }

    public class VotingDetailPresenter : BasePresenter<VotingViewState, VotingController>,
        IActivityDetailsView<VotingController, VotingController>,
        IVotingCommentsConsumer, IPageNavigatorSupporting {

        public ActivityDetailPresenter<VotingController> DetailViewModel { get; }
        public ActivityHeaderPresenter<VotingController> HeaderViewModel { get; }
        public IPageNavigator PageNavigator { get; }
        public ListViewPullToRefreshViewModel PullToRefresh { get; }

        public VotingModel Model { get; private set; }

        readonly IVotingCommentsProducer sender;
        readonly IVotingCommentAdd commentsRepo;

        public VotingDetailPresenter() : this(
            Bootstrapper.Instance.Resolver.Get<IVotingCommentsProducer>(),
            Bootstrapper.Instance.Resolver.Get<IVotingCommentAdd>()) { }

        public VotingDetailPresenter(IVotingCommentsProducer sender, IVotingCommentAdd commentsRepo) {
            this.sender = sender;
            this.commentsRepo = commentsRepo;
            PullToRefresh = new ListViewPullToRefreshViewModel();
            PullToRefresh.Refreshed += OnPullToRefreshed;
            PageNavigator = new PageNavigatorAdapter  {  };
            HeaderViewModel = new ActivityHeaderPresenter<VotingController>();
            DetailViewModel = new ActivityDetailPresenter<VotingController>();
            DetailViewModel.Controller.Presenter = this;
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
            var actor = "Test additing message";
            //var prev = DetailViewModel.ViewState.Comments;
            //prev.Add(new CommentViewState {
            //    IconSource = "person.png",
            //    Title = actor,
            //    Message = message
            //});
            //DetailViewModel.ViewState.Push((nameof(DetailViewModel.ViewState.Comments), prev));

            commentsRepo.Add(Model, new CommentVotingModel {
                Message = message,
                Actor = actor
            });

            sender.ReceiveComments(this);
        }
    }
}

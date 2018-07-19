using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Presentation.Controls;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Infrastructure;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social;
using Xamarin.Presentation.Social.Comments;
using Xamarin.Presentation.Social.Messaging;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Messaging {
    public class PrivateMessagingController : BaseController, IItemSelectedController<PrivateMessageViewState>,
        ICommentAddingController {

        internal PrivateMessagingPresenter Presenter;

        public Command<PrivateMessageViewState> ItemSelectedCommand { get; }
        public Command<Entry> CommentAdded {get;}
        public Command CameraActivated {get;}
        public Command AttachFileActivated {get;}

        public PrivateMessagingController() {
            ItemSelectedCommand = new Command<PrivateMessageViewState>(OnItemSelected);
            CommentAdded = new Command<Entry>(OnCommentAdded);
            CameraActivated = new Command<string>(OnCameraActivated);
            AttachFileActivated = new Command<string>(OnAttachFileActivated);
        }

        void OnAttachFileActivated(string obj) {
        
        }

        void OnCameraActivated(string obj) {
        
        }

        void OnCommentAdded(Entry entry) {
            Presenter.AddNewComment(entry.Text);
            entry.Text = null;
        }

        void OnItemSelected(PrivateMessageViewState obj) {

        }
    }

    public class PrivateMessagingViewState : CollectionViewState<PrivateMessageViewState>,
        IAccountShortInfoViewState, ICommentAddingViewState {
        public string IconSource { get; set; }
        public string UserName { get; set; }
        public string Info { get; set; }

        public string CommenterIconSource { get; set; }
        public string AddCommentEntryPlaceholder { get; set; }

        public PrivateMessagingViewState() {
            CommenterIconSource = "person.png";
            AddCommentEntryPlaceholder = "Write a message ...";

            for (int i = 0; i < 10; ++i) {
                ViewCollection.Add(new PrivateMessageViewState {
                    Type = i % 2 == 0 ? PrivateMessageTypes.Incoming : PrivateMessageTypes.Outgoing,
                    Message = "A dorsal view of a female Nephila pilipes, a species of golden silk orb-weaver spider found in East and Southeast Asia as well as Australia.",
                    Date = DateTime.Now.ToString(StaticResources.DateTimeFormat)
                });
            }
        }
    }


    public class PrivateMessagingPresenter :
        BasePresenter<PrivateMessagingViewState, PrivateMessagingController>,
        IPrivateMessagingPresenter<PrivateMessagingViewState>,
        IExtendedListPullToRefreshSupported,
        IPageNavigatorSupporting {

        public IPageNavigator PageNavigator { get; }
        public ListViewPullToRefreshViewModel PullToRefresh { get; }

        public PrivateMessagingPresenter() {
            PageNavigator = new PageNavigatorViewModel() { IconSource = StaticResources.Icons.MessageWhite };
            PullToRefresh = new ListViewPullToRefreshViewModel();
        }

        protected override void Init(PrivateMessagingViewState vs, PrivateMessagingController con) {
            base.Init(vs, con);
            con.Presenter = this;
        }

        public void ShowMessagingWith(PeopleViewState vs) {
            PageNavigator.Title = vs.Name;
            ViewState.Push(
                (nameof(PrivateMessagingViewState.IconSource), vs.IconSource),
                (nameof(PrivateMessagingViewState.UserName), vs.Name),
                (nameof(PrivateMessagingViewState.Info), vs.Info)
                );   
        }

        internal void AddNewComment(string message) {
            ViewState.ViewCollection.Add(new PrivateMessageViewState {
                Type = PrivateMessageTypes.Outgoing,
                Date = DateTime.Now.ToString(StaticResources.DateTimeFormat),
                Message = message
            });
        }
    }


}

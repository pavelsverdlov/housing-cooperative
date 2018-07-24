using HousingCoo.Domain.Interactors;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Presentation;
using Xamarin.Presentation.Framework.VSVVM;
using Xamarin.Presentation.Pages;
using Xamarin.Presentation.Social;
using Xamarin.Presentation.Social.Activities;
using Xamarin.Presentation.Social.States;

namespace HousingCoo.Presentation.Voting {
    public class AddNewVotingViewState : BaseViewState, IAddNewActivityViewState {
        public string IconSource { get; set; }
        public string UserName { get; set; }
        public string Info { get; set; }
        public ButtonModel Add { get; set; }



        public AddNewVotingViewState() {
            IconSource = "icon.png";
            UserName = "UserName";
            Info = "Administrator";
            Add = new ButtonModel { IsVisible = true, Title = "Add" };
        }
    }

    public class AddNewVotingController : BaseController, IAddNewActivityController {
        internal AddNewVotingPresenter Presenter;

        public Command<NewActivitySnapshot> ShareActivity { get; }

        public AddNewVotingController() {
            ShareActivity = new Command<NewActivitySnapshot>(OnAdd);
        }

        private void OnAdd(NewActivitySnapshot data) {
            Presenter.AddNewVoting(data);
        }
    }

    public class AddNewVotingPresenter : BasePresenter<AddNewVotingViewState, AddNewVotingController>,
        IPageNavigatorSupporting {
        readonly IXamLogger logger;
        readonly IVotingAdditing repo;

        //
        public IPageNavigator PageNavigator { get; }

        public AddNewVotingPresenter() : this(
         Bootstrapper.Instance.Resolver.Get<IXamLogger>(),
         Bootstrapper.Instance.Resolver.Get<IVotingAdditing>()) { }
        public AddNewVotingPresenter(IXamLogger logger, IVotingAdditing repo) {
            PageNavigator = new PageNavigatorAdapter  {
                Title = "Add new voting"
            };
            this.logger = logger;
            this.repo = repo;
        }

        protected override void Init(AddNewVotingViewState vs, AddNewVotingController con) {
            base.Init(vs, con);
            con.Presenter = this;
        }

        internal void AddNewVoting(NewActivitySnapshot data) {
            var state = ViewState;
            repo.Add(new Domain.Model.VotingModel {
                ActorName = state.UserName,
                Body = data.Body,
                Title = data.Title,
                Verb = "",
                DateClosed = data.FinishDate,
                DateOpened = DateTime.Now
            });
            PageNavigator.Navigation.PopToRootAsync(true);
        }
    }


}

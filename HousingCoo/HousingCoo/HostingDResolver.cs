using HousingCoo.Data;
using HousingCoo.Domain.Interactors;
using HousingCoo.Presentation.Messaging;
using HousingCoo.Presentation.People;
using HousingCoo.Presentation.Profile;
using HousingCoo.Presentation.Voting;
using Xamarin.Presentation;
using Xamarin.Presentation.DI;
using Xamarin.Presentation.Navigation;

namespace HousingCoo {
    public class HostingDResolver : DependencyResolver {
        protected override void Registration() {
            Register<IXamLogger, DiagnosticsDebugLogger>();

            Register<HostingVMContainer, HostingVMContainer>();
            Register<IVotingsGateway, DataGateway>();

            Register<IVotingListProducer, VotingListInteractor>();
            Register<IVotingCommentsProducer, VotingListInteractor>();
            Register<INotificationProducer, VotingListInteractor>();

            Register<ICommutator, HostingNAdapter>();

        }
    }

    public class HostingVMContainer : ViewModelContainer {
        protected override void Registration() {
            Map<VotingDetailPage, VotingDetailPresenter>();
            Map<VoitingListPage, VotingListPresenter>();
            Map<AddNewVotingPage, AddNewVotingPresenter>();
            Map<PrivateMessagingPage, PrivateMessagingPresenter>();
            Map<PreviewPeoplePage, PreviewPeoplePresenter>();
            Map<EditProfilePage, EditProfilePresenter>();


        }
    }

    public class HostingNAdapter : NavigationAdapter {
        public HostingNAdapter() : this(Bootstrapper.Instance.Resolver.GetAsSingleton<HostingVMContainer>()) { }
        public HostingNAdapter(ViewModelContainer container) : base(container) { }
    }
}

using HousingCoo.Data;
using HousingCoo.Domain.Interactors;
using HousingCoo.Presentation.Messaging;
using HousingCoo.Presentation.People;
using HousingCoo.Presentation.Profile;
using HousingCoo.Presentation.Voting;
using System;
using Xamarin.Presentation;
using Xamarin.Presentation.DI;
using Xamarin.Presentation.Navigation;

namespace HousingCoo {
    public class Bootstrapper {
        public static Bootstrapper Instance => bootstrapper.Value;

        private static readonly Lazy<Bootstrapper> bootstrapper;
        static Bootstrapper() {
            bootstrapper = new Lazy<Bootstrapper>(() => new Bootstrapper());
        }

        public HostingDResolver Resolver { get; }

        private Bootstrapper() {
            Resolver = new HostingDResolver();

            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap(typeof(Domain.Entities.VotingEntity), typeof(Domain.Model.VotingModel));
                cfg.CreateMap(typeof(Domain.Entities.PeopleEntity), typeof(Domain.Model.PeopleModel));
                cfg.CreateMap(typeof(Domain.Entities.NotificationEntity), typeof(Domain.Model.NotificationModel));
                cfg.CreateMap(typeof(Domain.Entities.CommentVotingEntity), typeof(Domain.Model.CommentVotingModel));
                cfg.CreateMap(typeof(Domain.Entities.PrivateMessageEntity), typeof(Domain.Model.PrivateMessageModel));
                //
                cfg.CreateMap(typeof(Domain.Model.PeopleModel), typeof(Domain.Entities.PeopleEntity));
                cfg.CreateMap(typeof(Domain.Model.CommentVotingModel), typeof(Domain.Entities.CommentVotingEntity));
            });
        }

    }

    public class HostingDResolver : DependencyResolver {
        protected override void Registration() {
            Register<IXamLogger, DiagnosticsDebugLogger>();

            Register<HostingVMContainer, HostingVMContainer>();
            Register<IVotingsGateway, DataGateway>();

            Register<IVotingListProducer, VotingListInteractor>();
            Register<IVotingCommentsProducer, VotingListInteractor>();
            Register<INotificationProducer, VotingListInteractor>();
            Register<IVotingAdditing, VotingListInteractor>();

            Register<IPeopleListProducer, VotingListInteractor>();
            Register<IMessagingPeopleListProducer, VotingListInteractor>();
            Register<IPrivateMessageListProducer, VotingListInteractor>();
            Register<IPeopleAdd, VotingListInteractor>();
            Register<IPeopleRemove, VotingListInteractor>();

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

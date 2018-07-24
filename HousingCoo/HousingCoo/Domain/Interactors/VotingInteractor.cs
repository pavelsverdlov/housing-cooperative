using HousingCoo.Domain.Entities;
using HousingCoo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Presentation;

namespace HousingCoo.Domain.Interactors {

    #region Voting
    public interface IVotingListConsumer {
        void OnPageReceived(IEnumerable<VotingModel> votings);
    }
    public interface IVotingListProducer {
        void ReceiveNextPage(IVotingListConsumer consumer);
    }

    public interface IVotingAdditing {
        void Add(VotingModel v);
    }

    #endregion

    #region VotingComments
    public interface IVotingCommentsConsumer {
        VotingModel Model { get; }
        void OnCommentsReceived(IEnumerable<CommentVotingModel> comments);
    }
    public interface IVotingCommentsProducer {
        void ReceiveComments(IVotingCommentsConsumer consumer);
    }

    public interface IVotingCommentAdd {
        void Add(VotingModel voting, CommentVotingModel v);
    }

    #endregion

    #region Notifications

    public interface INotificationConsumer {
        AccountModel Account { get; }
        void OnNotificationsReceived(IEnumerable<NotificationModel> comments);
    }
    public interface INotificationProducer {
        void SubstrubeTo(INotificationConsumer consumer);
    }

    #endregion

    #region People

    public interface IPeopleListConsumer {
        void OnReceived(IEnumerable<PeopleModel> votings);
    }
    public interface IPeopleListProducer {
        void Receive(IPeopleListConsumer consumer);
    }
    public interface IPeopleAdd {
        void Add(PeopleModel v);
    }

    public interface IPeopleRemove { 
        void Remove(PeopleModel v);
    }


    #endregion

    #region private messages

    public interface IPrivateMessageListConsumer {
        PeopleModel Account { get; }
        PeopleModel MessageTo { get; }
        void OnReceived(IEnumerable<PrivateMessageModel> votings);
    }
    public interface IPrivateMessageListProducer {
        void Receive(IPrivateMessageListConsumer consumer);
    }
    
    #endregion

    #region people messaging with

    public interface IMessagingPeopleListConsumer {
        void OnReceived(IEnumerable<PeopleModel> votings);
    }
    public interface IMessagingPeopleListProducer {
        void Receive(IMessagingPeopleListConsumer consumer);
    }

    #endregion


    public class VotingListInteractor :
        IVotingListProducer,
        IVotingAdditing,

        IVotingCommentAdd,
        IVotingCommentsProducer,
        INotificationProducer,

        IPeopleListProducer,
        IPrivateMessageListProducer,
        IMessagingPeopleListProducer,
        IPeopleAdd,
        IPeopleRemove {

        private readonly IVotingsGateway gateway;
        private int pageCount;
        public VotingListInteractor() : this(Bootstrapper.Instance.Resolver.Get<IVotingsGateway>()) { }
        public VotingListInteractor(IVotingsGateway gateway) {
            this.gateway = gateway;
            pageCount = 0;
        }
        public async void ReceiveNextPage(IVotingListConsumer receiver) {
            IEnumerable<VotingEntity> votings = await gateway.GetVotings(pageCount);

            receiver.OnPageReceived(votings.OrderBy(x=>x.DateOpened).Map<VotingEntity, VotingModel>());
            pageCount++;
        }

        public async void ReceiveComments(IVotingCommentsConsumer receiver) {
            IEnumerable<CommentVotingEntity> comments = await gateway.GetComments(receiver.Model.Id);

            receiver.OnCommentsReceived(comments.Map<CommentVotingEntity, CommentVotingModel>());
        }

        private int count = 10;
        public void SubstrubeTo(INotificationConsumer consumer) {
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(2), () => {
                try {
                    IEnumerable<NotificationEntity> notify = gateway.GetNotifications(consumer.Account.Id).Result;
                    --count;
                    DispatcherEx.BeginRise(() => consumer.OnNotificationsReceived(notify.Map<NotificationEntity, NotificationModel>()));
                    if (count == 0) {
                        return false;
                    }
                } catch (Exception ex) {
                    ex.ToString();
                }
                return true;
            });
        }


        void IVotingAdditing.Add(VotingModel v) {
            long id = 1 + HousingCoo.Data.Services.Storage.Instance.Votings.Keys.Max();
            v.Id = id;
            VotingEntity entity = v.Map<VotingEntity>();
            HousingCoo.Data.Services.Storage.Instance.Votings.Add(id, entity);
        }
        public void Add(VotingModel voting, CommentVotingModel v) {
            gateway.AddCommentToVoting(0, v.Map<CommentVotingEntity>());
        }

        #region People

        public async void Receive(IPeopleListConsumer consumer) {
            IEnumerable<PeopleEntity> list = await gateway.GetPeople(0);
            consumer.OnReceived(list.Map<PeopleEntity, PeopleModel>());
        }

        public async void Receive(IPrivateMessageListConsumer consumer) {
            var list = await gateway.GetPrivateMessages(consumer.Account.Id, consumer.MessageTo.Id);
            consumer.OnReceived(list.Map<PrivateMessageEntity, PrivateMessageModel>());
        }

        public async void Receive(IMessagingPeopleListConsumer consumer) {
            IEnumerable<PeopleEntity> list = await gateway.GetPeople(0);
            consumer.OnReceived(list.Map<PeopleEntity, PeopleModel>());
        }

        public void Add(PeopleModel v) {
            var saved = gateway.AddPerson(0,v.Map<PeopleEntity>());
            v.Id = saved.Id;
        }

        public void Remove(PeopleModel v) {
            gateway.RemovePerson(0, v.Id);
        }

      

        #endregion


    }

    public interface IVotingsGateway {
        Task<IEnumerable<VotingEntity>> GetVotings(int pageNum);
        Task<IEnumerable<CommentVotingEntity>> GetComments(long votingId);
        Task<IEnumerable<NotificationEntity>> GetNotifications(long userId);
        Task<IEnumerable<PeopleEntity>> GetPeople(long companyId);
        Task<IEnumerable<PrivateMessageEntity>> GetPrivateMessages(long userId, long peopleWithHumId);

        PeopleEntity AddPerson(int companyId, PeopleEntity entity);
        void RemovePerson(int coumanyId, long id);

        void AddCommentToVoting(int votingId, CommentVotingEntity en);

    }

}

using HousingCoo.Domain.Entities;
using HousingCoo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousingCoo.Domain.Interactors {

    #region Voting
    public interface IVotingListConsumer {
        void OnPageReceived(IEnumerable<VotingModel> votings);
    }
    public interface IVotingListProducer {
        void ReceiveNextPage(IVotingListConsumer consumer);
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

    #endregion


    public class VotingListInteractor : IVotingListProducer, IVotingCommentsProducer {
        readonly IVotingsGateway gateway;

        int pageCount;
        public VotingListInteractor() : this(Bootstrapper.Instance.Resolver.Get<IVotingsGateway>()) { }
        public VotingListInteractor(IVotingsGateway gateway) {
            this.gateway = gateway;
            pageCount = 0;
        }
        public async void ReceiveNextPage(IVotingListConsumer receiver) {
            var votings = await gateway.GetVotings(pageCount);

            receiver.OnPageReceived(votings.Map<VotingEntity, VotingModel>());
            pageCount++;
        }

        public async void ReceiveComments(IVotingCommentsConsumer receiver) {
            var comments = await gateway.GetComments(receiver.Model.Id);

            receiver.OnCommentsReceived(comments.Map<CommentVotingEntity, CommentVotingModel>());
        }
    }

    public interface IVotingsGateway {
        Task<IEnumerable<VotingEntity>> GetVotings(int pageNum);
        Task<IEnumerable<CommentVotingEntity>> GetComments(long votingId);
    }

}

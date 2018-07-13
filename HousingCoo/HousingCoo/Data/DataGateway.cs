using HousingCoo.Data.Services;
using HousingCoo.Domain.Entities;
using HousingCoo.Domain.Interactors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HousingCoo.Data {
    public class DataGateway : IVotingsGateway {
        private const int votingsOnPage = 20;
        public Task<IEnumerable<CommentVotingEntity>> GetComments(long votingId) {
            return Task.Run(() => {
                var client = new VotingCommentsServiceClient();

                return client.GetComments(votingId);
            });
        }

        public Task<IEnumerable<VotingEntity>> GetVotings(int pageNum) {
            return Task.Run(() => {

                var client = new VotingServiceClient();

                return client.GetNextPage(votingsOnPage, pageNum);
            });
        }
    }
}

using HousingCoo.Data.Services;
using HousingCoo.Domain.Entities;
using HousingCoo.Domain.Interactors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HousingCoo.Data {
    public class DataGateway : IVotingsGateway {
        private const int votingsOnPage = 20;
        public Task<IEnumerable<CommentVotingEntity>> GetComments(long votingId) {
            return Task.Run(() => {
                VotingCommentsServiceClient client = new VotingCommentsServiceClient();

                return client.GetComments(votingId);
            });
        }

        public Task<IEnumerable<NotificationEntity>> GetNotifications(long userId) {
            return Task.Run(() => {
                var cost = new System.Random().Next(100, 999);
                var date = DateTime.Now.AddMonths(new System.Random().Next(0, 12));

                return (IEnumerable<NotificationEntity>)new [] {
                    new NotificationEntity {
                        Title = $"Monthly payment {cost}$",
                        Message = $"School for {date.ToString("dd MMMM yyyy")}",
                    }
                };
            });
        }

        public Task<IEnumerable<VotingEntity>> GetVotings(int pageNum) {
            return Task.Run(() => {

                VotingServiceClient client = new VotingServiceClient();

                return client.GetNextPage(votingsOnPage, pageNum);
            });
        }
    }
}

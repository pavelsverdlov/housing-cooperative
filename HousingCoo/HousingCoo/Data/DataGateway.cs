using HousingCoo.Data.Services;
using HousingCoo.Domain.Entities;
using HousingCoo.Domain.Interactors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Presentation;

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

        public Task<IEnumerable<PeopleEntity>> GetPeople(long companyId) {
            return Task.Run(() => {

                return (IEnumerable<PeopleEntity>)Storage.Instance.People[companyId];
            });
        }

        public Task<IEnumerable<PrivateMessageEntity>> GetPrivateMessages(long userId, long peopleWithHumId) {
            return Task.Run(() => {

                return (IEnumerable<PrivateMessageEntity>)Storage.Instance.PrivateMesseges[userId];
            });
        }






        public PeopleEntity AddPerson(int companyId, PeopleEntity entity) {
            var community = Storage.Instance.People[companyId];
            long max = 0;
            if (community.Any()) {
                max = community.Max(x => x.Id);
            }
            entity.Id = ++max;
            community.Add(entity);

            return entity;
        }

        public void RemovePerson(int companyId, long id) {
            Storage.Instance.People[companyId]
                .Where(x => x.Id == id)
                .ForEach(x => Storage.Instance.People[companyId].Remove(x));
            
        }

        public void AddCommentToVoting(long votingId, CommentVotingEntity en) {
            var comm = Storage.Instance.Comments[votingId];
            long max = 0;
            if (comm.Any()) { 
                max = comm.Max(x => x.Id);
            }
            en.Id = ++max;
            Storage.Instance.Comments[votingId].Add(en);
        }
    }
}

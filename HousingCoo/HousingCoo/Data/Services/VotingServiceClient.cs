using HousingCoo.Domain.Entities;
using System;
using System.Collections.Generic;

namespace HousingCoo.Data.Services {
    public class VotingServiceClient {
        public IEnumerable<VotingEntity> GetNextPage(int count, int pageNum) {
            var list = new List<VotingEntity>();
            for (var i = 0; i < 10; ++i) {

                System.Threading.Thread.Sleep(200);

                list.Add(new VotingEntity {
                    Title = "Карпаты " + i,
                    ActorName = "Name " + i,
                    Verb = " add voting",
                    Body = $"Для поездки необходимо здать кучу денег в размере {new Random().Next(100, 999)}$ с человека.",
                    DateOpened = DateTime.Now,
                    DateClosed = DateTime.Now,
                });
            }
            return list;
        }
    }
    public class VotingCommentsServiceClient {
        public IEnumerable<CommentVotingEntity> GetComments(long votingId) {
            var list = new List<CommentVotingEntity>();
            for (var i = 0; i < 10; ++i) {

                System.Threading.Thread.Sleep(200);

                list.Add(new CommentVotingEntity {
                    Actor = "Actor " + i,
                    Message = "Some message " + i,
                });
            }
            return list;
        }
    }

}

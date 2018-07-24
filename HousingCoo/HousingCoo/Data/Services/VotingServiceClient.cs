using HousingCoo.Domain.Entities;
using System;
using System.Collections.Generic;

namespace HousingCoo.Data.Services {
    class Storage {
        public readonly static Storage Instance;
        static Storage() {
            Instance = new Storage();
        }

        public object Account { get; }

        //long - VotingEntity.ID
        public Dictionary<long, VotingEntity> Votings { get; }
        public Dictionary<long, List<CommentVotingEntity>> Comments { get; }

        //long -PeopleEntity.ID
        public Dictionary<long, List<PeopleEntity>> People { get; }
        public Dictionary<long, List<PrivateMessageEntity>> PrivateMesseges { get; }

        public Storage() {
            Votings = new Dictionary<long, VotingEntity>();
            Comments = new Dictionary<long, List<CommentVotingEntity>>();
            People = new Dictionary<long, List<PeopleEntity>>();
            PrivateMesseges = new Dictionary<long, List<PrivateMessageEntity>>();

            Votings.Add(0, new VotingEntity {
                Title = "Карпаты",
                ActorName = "Vasy Pupkin",
                Verb = " добавил голосование",
                Body = $"Для поездки необходимо здать кучу денег в размере {new Random().Next(100, 999)}$ с человека.",
                DateOpened = DateTime.Now,
                DateClosed = DateTime.Now,
            });

            Comments.Add(0, new List<CommentVotingEntity> {
               new CommentVotingEntity {
                    Actor = "Vasy",
                    Message = "Сильно дорого для Карпат"
                }
            });

            People.Add(0, new List<PeopleEntity> {
                new PeopleEntity {
                    Name = "Vasy Pupkin",
                    Info = "test user"
                }
            });

            PrivateMesseges.Add(0, new List<PrivateMessageEntity> {
                new PrivateMessageEntity {
                    Message = "A dorsal view of a female Nephila pilipes, a species of golden silk orb-weaver spider found in East and Southeast Asia as well as Australia.",
                    Date = DateTime.Now,
                    Type = 0
                },
                new PrivateMessageEntity {
                    Message = "A dorsal view of a female Nephila pilipes, a species of golden silk orb-weaver spider found in East and Southeast Asia as well as Australia.",
                    Date = DateTime.Now,
                    Type = 1
                }
            });

        }

    }


    public class VotingServiceClient {
        public IEnumerable<VotingEntity> GetNextPage(int count, int pageNum) {
            var list = new List<VotingEntity>(Storage.Instance.Votings.Values);
            return list;
        }
    }
    public class VotingCommentsServiceClient {
        public IEnumerable<CommentVotingEntity> GetComments(long votingId) {
            if (!Storage.Instance.Comments.ContainsKey(votingId)) {
                return new CommentVotingEntity[0];
            }
            var list = new List<CommentVotingEntity>(Storage.Instance.Comments[votingId]);
            return list;
        }
    }

}

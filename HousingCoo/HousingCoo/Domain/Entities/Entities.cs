using System;
using System.Collections.Generic;
using System.Text;

namespace HousingCoo.Domain.Entities {
    public class Entity {
        public long Id { get; set; }
    }
    public class VotingEntity : Entity {
        public string Title { get; set; }
        public string ActorName { get; set; }
        public string Verb { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }
        public string Body { get; set; }
    }
    public class CommentVotingEntity : Entity {
        public string Actor { get; set; }
        public string Message { get; set; }
    }
    public enum NotificationTypes {
        Undefined,

    }
    public class NotificationEntity : Entity {
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationTypes Type { get; set; }
    }

    public class PeopleEntity : Entity {
        public string Name { get; set; }
        public string Info { get; set; }
    }

    public class PrivateMessageEntity : Entity {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Incoming  - 0
        /// Outcoming - 1
        /// </summary>
        public byte Type { get; set; }
    }
}

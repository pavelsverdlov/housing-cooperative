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
}

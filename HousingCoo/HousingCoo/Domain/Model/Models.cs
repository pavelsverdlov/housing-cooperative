using System;
using System.Collections.Generic;
using System.Text;

namespace HousingCoo.Domain.Model {
    public class Model {
        public long Id { get; set; }
    }
    public class AccountModel : Model {

    }
    public class VotingModel: Model {
        public string Title { get; set; }
        public string ActorName { get; set; }
        public string Verb { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }
        public string Body { get; set; }
    }

    public class CommentVotingModel : Model {
        public string Actor { get; set; }
        public string Message { get; set; }
    }
    public class NotificationModel : Model {
        //public string Type { get; set; } TODO: should be difference model clases for each Notification.Type
        public string Title { get; set; }
        public string Message { get; set; }
    }
}

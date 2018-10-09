using GridDemo;
using System;

namespace GridDemo {
    public class IssueData {
        public IssueData(string subject, string user, DateTime created, int votes, Priority priority) {
            Subject = subject;
            User = user;
            Created = created;
            Votes = votes;
            Priority = priority;
        }
        public string Subject { get; private set; }
        public string User { get; private set; }
        public DateTime Created { get; private set; }
        public int Votes { get; private set; }
        public Priority Priority { get; private set; }
    }
}

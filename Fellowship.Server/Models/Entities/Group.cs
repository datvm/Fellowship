using System;
using System.Collections.Generic;

namespace Fellowship.Server.Models.Entities
{
    public partial class Group
    {
        public Group()
        {
            GroupActivity = new HashSet<GroupActivity>();
            GroupMember = new HashSet<GroupMember>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AdminAccountId { get; set; }
        public bool MemberCanAddPeople { get; set; }
        public bool MemberCanSetActivity { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedTime { get; set; }

        public Account AdminAccount { get; set; }
        public ICollection<GroupActivity> GroupActivity { get; set; }
        public ICollection<GroupMember> GroupMember { get; set; }
    }
}

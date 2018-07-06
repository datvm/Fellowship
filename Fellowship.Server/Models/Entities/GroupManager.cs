using System;
using System.Collections.Generic;

namespace Fellowship.Server.Models.Entities
{
    public partial class GroupManager
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int GroupMemberId { get; set; }
        public int SetByAccountId { get; set; }
        public bool CreatedTime { get; set; }
        public bool CanAddOtherMember { get; set; }
        public bool Deleted { get; set; }

        public Group Group { get; set; }
        public GroupMember GroupMember { get; set; }
        public Account SetByAccount { get; set; }
    }
}

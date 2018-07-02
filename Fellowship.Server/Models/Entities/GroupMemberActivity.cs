using System;
using System.Collections.Generic;

namespace Fellowship.Server.Models.Entities
{
    public partial class GroupMemberActivity
    {
        public long Id { get; set; }
        public int GroupMemberId { get; set; }
        public int? LinkedGroupActivityId { get; set; }
        public decimal Fund { get; set; }
        public DateTime CreatedTime { get; set; }

        public GroupMember GroupMember { get; set; }
        public GroupActivity LinkedGroupActivity { get; set; }
    }
}

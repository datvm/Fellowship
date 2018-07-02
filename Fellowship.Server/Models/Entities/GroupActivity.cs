using System;
using System.Collections.Generic;

namespace Fellowship.Server.Models.Entities
{
    public partial class GroupActivity
    {
        public GroupActivity()
        {
            GroupMemberActivity = new HashSet<GroupMemberActivity>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }

        public Group Group { get; set; }
        public ICollection<GroupMemberActivity> GroupMemberActivity { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Fellowship.Server.Models.Entities
{
    public partial class GroupMember
    {
        public GroupMember()
        {
            GroupMemberActivity = new HashSet<GroupMemberActivity>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public int AccountId { get; set; }
        public string Nickname { get; set; }
        public DateTime AddedOn { get; set; }
        public bool Kicked { get; set; }
        public decimal FundCache { get; set; }

        public Account Account { get; set; }
        public Group Group { get; set; }
        public ICollection<GroupMemberActivity> GroupMemberActivity { get; set; }
    }
}

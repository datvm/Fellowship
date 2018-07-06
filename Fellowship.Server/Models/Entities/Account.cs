using System;
using System.Collections.Generic;

namespace Fellowship.Server.Models.Entities
{
    public partial class Account
    {
        public Account()
        {
            AccountSpecialClaim = new HashSet<AccountSpecialClaim>();
            Group = new HashSet<Group>();
            GroupManager = new HashSet<GroupManager>();
            GroupMember = new HashSet<GroupMember>();
        }

        public int Id { get; set; }
        public int? CreatedByAccountId { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookId { get; set; }
        public string FacebookProfile { get; set; }
        public string Name { get; set; }

        public ICollection<AccountSpecialClaim> AccountSpecialClaim { get; set; }
        public ICollection<Group> Group { get; set; }
        public ICollection<GroupManager> GroupManager { get; set; }
        public ICollection<GroupMember> GroupMember { get; set; }
    }
}

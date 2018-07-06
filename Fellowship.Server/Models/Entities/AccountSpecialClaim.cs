using System;
using System.Collections.Generic;

namespace Fellowship.Server.Models.Entities
{
    public partial class AccountSpecialClaim
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public Account Account { get; set; }
    }
}

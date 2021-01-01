namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class UserFuseCouple : BaseModel<int>
    {
        public int CardId { get; set; }

        public Card Card { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}

namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;
    using TheDarkPortal.Data.Models.Cards;

    public class UserCard : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int CardId { get; set; }

        public Card Card { get; set; }
    }
}

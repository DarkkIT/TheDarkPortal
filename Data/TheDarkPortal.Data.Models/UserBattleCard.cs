namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class UserBattleCard : BaseModel<int>
    {
        public int BattleCardId { get; set; }

        public BattleCard BattleCard { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}

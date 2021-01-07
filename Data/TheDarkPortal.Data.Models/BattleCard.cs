namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class BattleCard : BaseModel<int>
    {
        public BattleCard()
        {
            this.UserBattleCards = new HashSet<UserBattleCard>();
        }

        public string Name { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double Power { get; set; }

        public double Attack { get; set; }

        public double CurrentAttack { get; set; }

        public double Defense { get; set; }

        public double CurrentDefense { get; set; }

        public double Health { get; set; }

        public double CurrentHealth { get; set; }

        public string Element { get; set; }

        public bool IsSelected { get; set; }

        public bool IsAttacker { get; set; }

        public bool IsDestroyed { get; set; }

        public bool HaveTakenTurn { get; set; }

        public virtual ICollection<UserBattleCard> UserBattleCards { get; set; }
    }
}

namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class Card : BaseModel<int>
    {
        public Card()
        {
            this.UserCards = new HashSet<UserCard>();
            this.UserFuseCouples = new HashSet<UserFuseCouple>();
        }

        public string Name { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double Power { get; set; }

        public double Defense { get; set; }

        public double Health { get; set; }

        public string Element { get; set; }

        public int Price { get; set; }

        public virtual ICollection<UserCard> UserCards { get; set; }

        public virtual ICollection<UserFuseCouple> UserFuseCouples { get; set; }
    }
}

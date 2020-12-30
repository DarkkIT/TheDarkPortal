namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class FuseCard : BaseModel<int>
    {
        public FuseCard()
        {
            this.UserFuseCouples = new HashSet<UserFuseCouple>();
        }

        public string Name { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double Power { get; set; }

        public double Defense { get; set; }

        public double Health { get; set; }

        public virtual ICollection<UserFuseCouple> UserFuseCouples { get; set; }
    }
}

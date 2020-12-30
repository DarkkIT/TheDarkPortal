namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class Currency : BaseDeletableModel<int>
    {
        public Currency()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public int Silver { get; set; }

        public int Gold { get; set; }

        public int Platinum { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}

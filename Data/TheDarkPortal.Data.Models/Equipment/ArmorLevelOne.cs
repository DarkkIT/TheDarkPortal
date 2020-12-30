namespace TheDarkPortal.Data.Models.Equipment
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class ArmorLevelOne : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public int Power { get; set; }

        public int Defense { get; set; }

        public int Health { get; set; }

        public int Price { get; set; }
    }
}

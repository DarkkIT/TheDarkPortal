﻿namespace TheDarkPortal.Data.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Common.Models;

    public class CardLevelFive : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double Attack { get; set; }

        public double Defense { get; set; }

        public double Health { get; set; }

        public string Element { get; set; }

        public int Price { get; set; }
    }
}

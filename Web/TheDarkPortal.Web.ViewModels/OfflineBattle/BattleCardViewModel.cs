namespace TheDarkPortal.Web.ViewModels.OfflineBattle
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Services.Mapping;

    public class BattleCardViewModel : IMapFrom<TempBattleCards>
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public string Name { get; set; }

        public int Tire { get; set; }

        public int Level { get; set; }

        public double Power => Math.Floor((this.Attack + this.Defense + this.Health) / 3);

        public double Attack { get; set; }

        public double Defense { get; set; }

        public double Health { get; set; }

        public string Element { get; set; }

        public bool IsAttacker { get; set; }

        public bool IsDestroyed { get; set; }

        public bool HaveTakenTurns { get; set; }

        public bool IsSelected { get; set; }

        public string ImagePath => "/images/" + this.Name + "-" + this.Tire + ".jpg";
    }
}

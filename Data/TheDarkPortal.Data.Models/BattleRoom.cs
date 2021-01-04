namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using TheDarkPortal.Data.Common.Models;

    public class BattleRoom : BaseDeletableModel<int>
    {

        public string PlayerOneId { get; set; }

        public ApplicationUser PlayerOne { get; set; }

        public string PlayerTwoId { get; set; }

        public ApplicationUser PlayerTwo { get; set; }

        public bool IsFirstPlayerTurn { get; set; }

        public TimeSpan TimeLeftInTurn { get; set; }

    }
}

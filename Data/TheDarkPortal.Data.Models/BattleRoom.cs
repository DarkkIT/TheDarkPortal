namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using TheDarkPortal.Data.Common.Models;

    public class BattleRoom : BaseModel<int>
    {
        [ForeignKey("Attacker")]
        public string AttackerId { get; set; }

        public ApplicationUser Attacker { get; set; }

        [ForeignKey("Defender")]
        public string DefenderId { get; set; }

        public ApplicationUser Defender { get; set; }

        public bool IsAttackerTurn { get; set; }

        public TimeSpan TimeLeftInTurn { get; set; }

    }
}

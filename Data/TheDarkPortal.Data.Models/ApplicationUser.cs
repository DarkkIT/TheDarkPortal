// ReSharper disable VirtualMemberCallInConstructor
namespace TheDarkPortal.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;
    using TheDarkPortal.Data.Common.Models;
    using TheDarkPortal.Data.Models.Cards;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Cards = new HashSet<CardLevelOne>();
            this.UserFuseCouples = new HashSet<UserFuseCouple>();
            this.BattleCards = new HashSet<UserBattleCard>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string NickName { get; set; }

        public int ArenaPoints { get; set; }

        public int EventPoints { get; set; }

        public int VipLevel { get; set; }

        public int Silver { get; set; }

        public int Gold { get; set; }

        public int Platinum { get; set; }

        //[ForeignKey("BattleRoom")]
        //public int BattleRoomId { get; set; }

        //public BattleRoom BattleRoom { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<CardLevelOne> Cards { get; set; }

        public virtual ICollection<UserFuseCouple> UserFuseCouples { get; set; }

        public virtual ICollection<UserBattleCard> BattleCards { get; set; }
    }
}

namespace TheDarkPortal.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TheDarkPortal.Data.Common.Models;
    using TheDarkPortal.Data.Models;
    using TheDarkPortal.Data.Models.Cards;
    using TheDarkPortal.Data.Models.Equipment;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FuseCard> FuseCards { get; set; }

        public virtual DbSet<UserFuseCouple> UserFuseCouples { get; set; }

        public virtual DbSet<HelmetLevelOne> HelmetsLevelOne { get; set; }

        public virtual DbSet<ArmorLevelOne> ArmorsLevelOne { get; set; }

        public virtual DbSet<GlovesLevelOne> GlovesLevelOne { get; set; }

        public virtual DbSet<PantsLevelOne> PantsLevelOne { get; set; }

        public virtual DbSet<WeaponLevelOne> WeaponsLevelOne { get; set; }

        public virtual DbSet<Card> Cards { get; set; }

        public virtual DbSet<UserCard> UserCards { get; set; }

        public virtual DbSet<CardLevelFive> CardLevelFives { get; set; }

        public virtual DbSet<CardLevelFour> CardLevelFours { get; set; }

        public virtual DbSet<CardLevelThree> CardLevelThrees { get; set; }

        public virtual DbSet<CardLevelTwo> CardLevelTwos { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<CardLevelOne> CardsLevelOne { get; set; }

        public virtual DbSet<Vip> Vips { get; set; }

        public virtual DbSet<BattleRoom> BattleRooms { get; set; }

        public virtual DbSet<BattleCard> BattleCards { get; set; }

        public virtual DbSet<UserBattleCard> UserBattleCards { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }


        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        // Applies configurations
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}

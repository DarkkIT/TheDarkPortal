namespace TheDarkPortal.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheDarkPortal.Data.Models.Equipment;

    public class WeaponSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.WeaponsLevelOne.Any())
            {
                return;
            }

            var weaponList = new List<WeaponLevelOne>();

            var weapon1 = new WeaponLevelOne
            {
                Name = "DragonWeapon",
                Power = 2000,
                Defense = 0,
                Health = 0,
                Price = 300,
            };

            weaponList.Add(weapon1);

            var weapon2 = new WeaponLevelOne
            {
                Name = "HunterWeapon",
                Power = 1500,
                Defense = 500,
                Health = 0,
                Price = 300,
            };

            weaponList.Add(weapon2);

            await dbContext.WeaponsLevelOne.AddRangeAsync(weaponList);
        }
    }
}

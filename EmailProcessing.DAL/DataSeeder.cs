using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProcessing.DAL
{
    public class DataSeeder
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<UserEntity> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public DataSeeder(AppDbContext ctx, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {

            _ctx.Database.EnsureCreated();
            Random r = new Random();
            if (await _roleManager.FindByNameAsync("adminRole") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("adminRole"));
            }
            if (await _roleManager.FindByNameAsync("userRole") == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("userRole"));
            }
            string[] addresses = new string[] { "Варшавское шоссе", "ул. Академика Янгеля", "ул. Чертановская", "Рязанский пр.", "Волгоградский пр.", "ш. Энтузиастов", "ул. Россошанская", "ул. Ленинское шоссе", "ул. Охотный ряд", "ул. Воздвиженка", "ул. Новослободская", "ул. Трифоновская" };
            if (!_ctx.Users.Any())
            {

                var user = new UserEntity()
                {
                    Email = "admin@acado.com",
                    UserName = "admin"

                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    await _userManager.AddToRoleAsync(user, "adminRole");
                }

                _ctx.Groups.AddRange(new List<GroupEntity>() { new GroupEntity() { GroupName = "Бригада 1" }, new GroupEntity() { GroupName = "Бригада 2" }, new GroupEntity() { GroupName = "Бригада 3" }, });
                _ctx.SaveChanges();
                for (int i = 0; i <= 10; i++)
                {
                    var adduser = new UserEntity()
                    {
                        Email = $"user{i}@acado.com",
                        UserName = $"user{i}",
                        GroupId = r.Next(1, 3)
                    };

                    var resultUser = await _userManager.CreateAsync(adduser, "P@ssw0rd!");
                    if (resultUser.Succeeded)
                    {
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(adduser);
                        await _userManager.AddToRoleAsync(adduser, "userRole");
                    }
                }
            }
            if (!_ctx.ApplicationStatuses.Any())
            {
                _ctx.ApplicationStatuses.AddRange(new List<ApplicationStatusEntity>()
                {
                    new ApplicationStatusEntity(){ StatusName = "В обработке" },
                    new ApplicationStatusEntity(){ StatusName = "В ожидании" },
                    new ApplicationStatusEntity(){ StatusName = "В работе" },
                    new ApplicationStatusEntity(){ StatusName = "Возврат" },
                    new ApplicationStatusEntity(){ StatusName = "Выполнено" }
                });
                _ctx.SaveChanges();
            }
            if (!_ctx.Districts.Any())
            {
                _ctx.Districts.AddRange(new List<DistrictEntity>() {
                    new DistrictEntity { DistrictName = "Север" },
                    new DistrictEntity { DistrictName = "Юг" },
                    new DistrictEntity { DistrictName = "Восток" },
                    new DistrictEntity { DistrictName = "Запад" }
                });
                _ctx.SaveChanges();
            }
            if (!_ctx.Chanels.Any())
            {

                // string[] EquipTypes = new string[] { "Концентратор", "Коммутатор", "Маршрутизаторы", "Мост", "Шлюз", "Мультиплексор", "Межсетевой экран" };
                char[] chanels = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'Y' };
                for (int i = 0; i <= 200; i++)
                {
                    _ctx.Chanels.Add(new ChanelEntity
                    {
                        Code = $"{chanels[r.Next(0, chanels.Count() - 1)]}, {r.Next(2888, 4990)}",
                        AddressNode = $"{addresses[r.Next(0, addresses.Count() - 1)]}, {r.Next(1, 200)}"

                    });
                }
                _ctx.SaveChanges();
            }

            if (!_ctx.Applications.Any())
            {

                // Random r = new Random();

                for (int i = 0; i < 500000; i++)
                {
                    var create = GenererateRandomDate();
                    int status = r.Next(1, 5);

                    _ctx.Applications.Add(new ApplicationEntiry
                    {
                        NumML = r.Next(500001, 599999),
                        Address = $"{addresses[r.Next(0, addresses.Count() - 1)]}, {r.Next(1, 200)}",
                        ApplicationStatusId = status,
                        DistrictId = r.Next(1, 4),
                        CreateDate = create,
                        EndDate = (status > 3) ? (DateTime?)create.AddDays(r.Next(1, 10)) : null,
                        //  EquipmentId = r.Next(1, 199)
                    });
                }
                _ctx.SaveChanges();
            }
            if (!_ctx.Equipments.Any())
            {
                //  Random r = new Random();
                string[] EquipTypes = new string[] { "Концентратор", "Коммутатор", "Маршрутизаторы", "Мост", "Шлюз", "Мультиплексор", "Межсетевой экран" };
                string[] Brands = new string[] { "Cisco", "Huawei", "Netgear", "Erisson", "Dell", "HP", "Intel" };
                for (int i = 0; i <= 2000; i++)
                {
                    _ctx.Equipments.Add(new EquipmentEntity
                    {
                        EquipmentName = $"{EquipTypes[r.Next(0, EquipTypes.Count() - 1)]} {Brands[r.Next(0, Brands.Count() - 1)]}",
                        ChanelId = r.Next(1, 199),
                        Port = r.Next(80, 8999).ToString(),
                        SerialNumber = Guid.NewGuid().ToString(),
                        ApplicationId = r.Next(3000, 49999)
                    });
                }
                _ctx.SaveChanges();
            }
        }

        DateTime GenererateRandomDate()
        {

            Random rnd = new Random();
            DateTime date = DateTime.Now;
            int year = rnd.Next(date.Year, DateTime.Now.Year + 1);
            int month = rnd.Next(1, 12);
            int day = DateTime.DaysInMonth(year, month);

            int Day = rnd.Next(1, day);

            DateTime dt = new DateTime(year, month, Day);
            return dt;
        }

    }
}

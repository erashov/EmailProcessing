using EmailProcessing.DAL.Entities;
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
            //  string[] addresses = new string[] { "Варшавское шоссе", "ул. Академика Янгеля", "ул. Чертановская", "Рязанский пр.", "Волгоградский пр.", "ш. Энтузиастов", "ул. Россошанская", "ул. Ленинское шоссе", "ул. Охотный ряд", "ул. Воздвиженка", "ул. Новослободская", "ул. Трифоновская" };
            if (!_ctx.Users.Any())
            {

                var user = new UserEntity()
                {
                    Email = "admin@akado.com",
                    UserName = "admin"

                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    await _userManager.AddToRoleAsync(user, "adminRole");
                }


            }
            if (!_ctx.RequestTypes.Any())
            {
                _ctx.RequestTypes.AddRange(new List<RequestType>() {
                    new RequestType { FormatMessage = "SOAP", TypeName = "soap" },
                    new RequestType { FormatMessage = "POST", TypeName = "Web API(POST)" },
                    new RequestType { FormatMessage = "GET", TypeName = "Web API(GET)" }
                });
            }
            if (!_ctx.ParamTypes.Any())
            {
                _ctx.ParamTypes.AddRange(new List<ParamType>() { new ParamType { Name = "Строка", Value = "string" },
                    new ParamType { Name = "Число", Value = "number" },
                    new ParamType { Name = "Дата", Value = "date" }, new ParamType { Name = "Телефон", Value = "phone" }, });
                _ctx.SaveChanges();

            }
            if (!_ctx.Serrings.Any())
            {
                _ctx.Serrings.AddRange(new List<Setting>() {
                    new Setting(){
                        ImapServer = "mail.akado-telecom.ru",
                        ImapPort =993,
                        InputMail = "startultimus@akado-telecom.ru",
                        InputMailPassword = "SnrQF5kX",
                        Name="Заявка из метро",
                        Subject="Заявка из метро",
                        RequestTypeId=1,
                        RegexMask=@"(\S+?)\s*:\s*(\S+)",
                        ServiceUrl="localhost:5000/api/Application"
                    },
                       new Setting(){
                        ImapServer = "mail.akado-telecom.ru",
                        ImapPort =993,
                        InputMail = "startultimus@akado-telecom.ru",
                        InputMailPassword = "SnrQF5kX",
                        Name="Заявка на AKADO МОСКВА",
                        Subject="Заявка на AKADO МОСКВА ",
                        RequestTypeId=1,
                        RegexMask=@"(\S+?)\s*:\s*(\S+)",
                        ServiceUrl="localhost:5000/api/Application"
                    }
                });
                _ctx.SaveChanges();
            }
            if (!_ctx.ParamSettings.Any())
            {
                _ctx.ParamSettings.AddRange(new List<ParamSetting>() {
                    new ParamSetting() { FullName= "Лендинг", Name="landing", PramTypeId=1, SettingId=1 },
                    new ParamSetting() { FullName= "Дата создания", Name="create_date", PramTypeId=1, SettingId=1 },
                    new ParamSetting() { FullName= "Введенный номер телефона", Name="create_date", PramTypeId=1, SettingId=1 },
                    new ParamSetting() { FullName= "Адрес", Name="address", PramTypeId=1, SettingId=2 },
                    new ParamSetting() { FullName= "ФИО", Name="fio", PramTypeId=1, SettingId=2 },
                    new ParamSetting() { FullName= "Контактный телефон", Name="phone", PramTypeId=1, SettingId=2 },
                    new ParamSetting() { FullName= "Дополн. телефон", Name="add_phone", PramTypeId=1, SettingId=2 },
                    new ParamSetting() { FullName= "Услуги", Name="service", PramTypeId=1, SettingId=2 },
                    new ParamSetting() { FullName= "Комментарий", Name="comment", PramTypeId=1, SettingId=2 },
                });
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

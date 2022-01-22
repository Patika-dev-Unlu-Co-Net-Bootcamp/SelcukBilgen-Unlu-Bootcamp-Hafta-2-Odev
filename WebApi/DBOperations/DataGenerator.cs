using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                   new CounselingCenterDbContext(serviceProvider
                       .GetRequiredService<DbContextOptions<CounselingCenterDbContext>>()))
            {
                if (context.Users.Any()) // Db'de User var mı?
                    return;

                context.Users.AddRange(
                    new User
                    {
                        Email = "mahmut.tuncer@mail.com",
                        UserName = "Mahmut Tuncer",
                        UserRole = UserRole.Specialist
                    },
                    new User
                    {
                        Email = "nihat.dogan@mail.com",
                        UserName = "Nihat Doğan",
                        UserRole = UserRole.Specialist
                    },
                    new User
                    {
                        Email = "ismail.turut@mail.com",
                        UserName = "İsmail Türüt",
                        UserRole = UserRole.Specialist
                    },
                    new User
                    {
                       
                        Email = "selcuk.bilgen@mail.com",
                        UserName = "Selçuk Bilgen",
                        UserRole = UserRole.Client
                    },
                    new User
                    {
                        Email = "hakki.bulut@mail.com",
                        UserName = "Hakkı Bulut",
                        UserRole = UserRole.Client
                    },
                    new User
                    {
                        Email = "serkan.merkan@mail.com",
                        UserName = "Serkan Merkan",
                        UserRole = UserRole.Client
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
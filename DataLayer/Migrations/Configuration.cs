using System;
using System.Data.Entity.Migrations;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using Utilities.Security;

namespace DataLayer.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ShopDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ShopDbContext context)
        {
            context.Roles.AddOrUpdate(x => new { x.Name, x.Description },
                 new Role { Name = "admin", Description = "مدیر" },
                 new Role { Name = "user", Description = "مشتری" });

            context.SiteOptions.AddOrUpdate(op => new { op.Name, op.Value },
                new SiteOption { Name = "StoreName", Value = "" }, 
                new SiteOption { Name = "StoreKeyWords", Value = "" },
                new SiteOption { Name = "StoreDescription", Value = "" },
                new SiteOption { Name = "Tel1", Value = "" },
                new SiteOption { Name = "Tel2", Value = "" },
                new SiteOption { Name = "Address", Value = "" },
                new SiteOption { Name = "PhoneNumber1", Value = "" },
                new SiteOption { Name = "PhoneNumber2", Value = "" },
                new SiteOption { Name = "CommentModeratorStatus", Value = "true" }
                );

            context.SaveChanges();

            context.Users.AddOrUpdate(u => u.UserName, new User
            {
                RegisterDate = DateTime.Now,
                IsBaned = false,
                RegisterType = UserRegisterType.Active,
                PhoneNumber = "09146208938",
                Password = Encryption.EncryptingPassword("09146208938"),
                Role = context.Roles.Find(1),
                UserName = "admin"
            });
            base.Seed(context);
        }
    }
}


using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using DomainClasses.Enums;

namespace DomainClasses.Entities
{
    public class User
    {
        public virtual long Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual string LastName { get; set; }
        public virtual string AvatarPath { get; set; }
        public virtual DateTime? BirthDay { get; set; }

        [Index(IsUnique = true)]
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        [InverseProperty("UsersFavorite")]
        public virtual ICollection<Product> ProductsFavorite { get; set; }
        public virtual UserRegisterType RegisterType { get; set; }
        [Index(IsUnique = true)]
        public virtual string PhoneNumber { get; set; }
        public virtual string Address { get; set; }
        public virtual string IP { get; set; }
        public virtual bool IsBaned { get; set; }
        public virtual DateTime RegisterDate { get; set; }
        public virtual DateTime? BanedDate { get; set; }
        public virtual DateTime? LastLoginDate { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Product> LikedProducts { get; set; }
        public virtual ICollection<Comment> LikedComments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual byte[] RowVersion { get; set; }

    }

}

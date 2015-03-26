using DomainClasses.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class Comment
    {
        public virtual long Id { get; set; }
        public virtual string Content { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual byte[] RowVersion { get; set; }
        public virtual User Author { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual long? ParentId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual long ProductId { get; set; }
        public virtual Rate Rate { get; set; }
        public virtual ICollection<Comment> Children { get; set; }
        public virtual CommentSectionType SectionType { get; set; }
        public virtual ICollection<User> LikedUsers { get; set; }
    }
}

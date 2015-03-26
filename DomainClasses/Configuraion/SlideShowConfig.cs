using DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Configuraion
{
    public class SlideShowConfig : EntityTypeConfiguration<SlideShow>
    {
        public SlideShowConfig()
        {
            Property(a => a.ImageAltText).IsRequired().HasMaxLength(50);
            Property(a => a.ImagePath).IsRequired().IsMaxLength();
            Property(a => a.Link).IsRequired().IsMaxLength();
            Property(a => a.Title).IsRequired().HasMaxLength(50);
            Property(a => a.Description).IsOptional().HasMaxLength(300);
            Property(a => a.DataVertical).IsRequired();
            Property(a => a.DataHorizontal).IsRequired();
            Property(a => a.Position).IsRequired().HasMaxLength(20);
            Property(a => a.ShowTransition).IsRequired().HasMaxLength(20);
            Property(a => a.HideTransition).IsRequired().HasMaxLength(20);
        }
    }
}

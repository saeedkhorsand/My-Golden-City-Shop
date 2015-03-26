using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    public class SlideShow
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string ImageAltText { get; set; }
        public virtual string Link { get; set; }
        public virtual string Position { get; set; }
        public virtual string ShowTransition { get; set; }
        public virtual string HideTransition { get; set; }
        public virtual int DataVertical { get; set; }
        public virtual int DataHorizontal { get; set; }
    }
}

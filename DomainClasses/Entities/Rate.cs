using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses.Entities
{
    [ComplexType]
    public class Rate
    {
        public virtual double? TotalRating { get; set; }
        public virtual int? TotalRaters { get; set; }
        public virtual double? AverageRating { get; set; }
    }
}

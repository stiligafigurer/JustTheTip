using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class Visitor
    {
        [Key, Column(Order = 1)]
        public virtual User VisitedUser { get; set; }
        [Key, Column(Order = 2)]
        public virtual User VisitingUser { get; set; }
        // Use DateTime with millisecond to ensure every row is unique
        [Key, Column(Order = 3), DataType(DataType.DateTime)]
        public virtual DateTime VisitDate { get; set; }
    }
}
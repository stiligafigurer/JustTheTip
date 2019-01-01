using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class Visitor
    {
        [Key]
        public virtual User VisitedUser { get; set; }
        [Key]
        public virtual User VisitingUser { get; set; }
        // Use DateTime with millisecond to ensure every row is unique
        [Key]
        public virtual DateTime VisitDate { get; set; }
    }
}
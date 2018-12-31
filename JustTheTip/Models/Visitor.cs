using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class Visitor
    {
        // Tricky w/o unique ID. Repeated visits == shitstorm
        public virtual User VisitedUser { get; set; }
        public virtual User VisitingUser { get; set; }
        public virtual DateTime VisitDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class Friends
    {
        public virtual User User { get; set; }
        public virtual User Friend { get; set; }
        public virtual Category RelationCategory { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class User_Interest
    {
        public virtual User UserID { get; set; }
        public virtual Interest InterestID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustTheTip.Models {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeUntilEighteenAttribute : RangeAttribute {
        public RangeUntilEighteenAttribute() : base(DateTime.Now.Year - 100, DateTime.Now.Year - 18) {
        }
    }
}
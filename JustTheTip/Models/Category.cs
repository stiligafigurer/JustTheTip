﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustTheTip.Models
{
    public class Category
    {
        public virtual int CategoryID { get; set; }
        public virtual string Description { get; set; }
    }
}
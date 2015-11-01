using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class Account
    {
        public int Id { get; set; }
        public virtual User Users { get; set; }
        public virtual Project Projects { get; set; }
    }
}
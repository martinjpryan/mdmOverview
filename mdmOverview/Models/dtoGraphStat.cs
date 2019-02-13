using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models
{
    [NotMapped]
    public class dtoGraphStat
    {
        public string y { get; set; }
        public string x { get; set; }
        public string desc { get; set; }

    }
}
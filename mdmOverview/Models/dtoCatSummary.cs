using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models
{
    [NotMapped]
    public class dtoCatSummary
    {
        public string cat { get; set; }
        public int span { get; set; }
        public string act { get; set; }
        public int tot { get; set; }
    }
}
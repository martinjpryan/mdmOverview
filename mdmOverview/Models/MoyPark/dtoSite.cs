using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models.MoyPark
{
    [NotMapped]
    public class dtoSite
    {
        public string sapsitecode { get; set; }
        public string legacysitecode { get; set; }
        public string legacysitedesc { get; set; }
    }
}
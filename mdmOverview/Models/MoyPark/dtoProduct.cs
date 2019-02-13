using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models.MoyPark
{
    [NotMapped]
    public class dtoProduct
    {
        public string description { get; set; }
        public string materialid { get; set; }
        public string legacycode { get; set; }
        public string pricingunit { get; set; }
        public string sapdocid { get; set; }
        public string recguid { get; set; }
        public List<dtoSite> sites { get; set; }
        public List<dtoUnit> units { get; set; }
        public dtoProduct()
        {
            sites = new List<dtoSite>();
            units = new List<dtoUnit>();
        }
    }
}
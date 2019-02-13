using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models
{
    [Table("scoMaster.Plants")]
    public class Plant
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string plantcode { get; set; }
        public int companycode { get; set; }
        public string plantdescription { get; set; }
    }
}
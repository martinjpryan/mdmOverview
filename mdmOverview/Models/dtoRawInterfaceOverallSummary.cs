using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models
{
    [NotMapped]
    public class dtoRawInterfaceOverallSummary
    {
        public int errorcount { get; set; }
        public int insertcount { get; set; }
        public int updatecount { get; set; }
        public int updatemax { get; set; }
        public int insertmax { get; set; }
        public int errormax { get; set; }
        public List<dtoCatSummary> updatesummary { get; set; }
        public List<dtoCatSummary> insertsummary { get; set; }
        public List<dtoCatSummary> errorsummary { get; set; }
        public List<dtoGraphStat> graphInserts { get; set; }
        public List<dtoGraphStat> graphUpdates { get; set; }
        public List<dtoGraphStat> graphErrors { get; set; }
        public dtoRawInterfaceOverallSummary()
        {
            updatesummary = new List<dtoCatSummary>();
            insertsummary = new List<dtoCatSummary>();
            errorsummary = new List<dtoCatSummary>();
            graphInserts = new List<dtoGraphStat>();
            graphUpdates = new List<dtoGraphStat>();
            graphErrors = new List<dtoGraphStat>();
        }


    }
}
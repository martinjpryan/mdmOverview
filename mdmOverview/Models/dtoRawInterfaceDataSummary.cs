using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models
{
    [NotMapped]
    public class dtoRawInterfaceDataSummary
    {
        public int errorcount { get; set; }
        public int insertcount { get; set; }
        public int updatecount { get; set; }
        public int updatemax { get; set; }
        public int insertmax { get; set; }
        public List<RawInterfaceData> errors { get; set; }
        public List<RawInterfaceData> inserts { get; set; }
        public List<RawInterfaceData> updates { get; set; }

        public List<dtoGraphStat> graphInserts { get; set; }
        public List<dtoGraphStat> graphUpdates { get; set; }
        public List<dtoGraphStat> graphErrors { get; set; }
        public dtoRawInterfaceDataSummary()
        {
            updates = new List<RawInterfaceData>();
            inserts = new List<RawInterfaceData>();
            errors = new List<RawInterfaceData>();
            graphInserts = new List<dtoGraphStat>();
            graphUpdates = new List<dtoGraphStat>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mdmOverview.Models
{
    [Table("scoMaster.RawInterfaceData")]
    public class RawInterfaceData
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string EntityNumber { get; set; }
        public string IdocNumber { get; set; }
        public string ServiceId { get; set; }
        public int StatusId { get; set; }
        public string Data { get; set; }
        public string StatusMessage { get; set; }
        public string Type { get; set; }
        public DateTimeOffset Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset Modified { get; set; }
        public string ModifiedBy { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
    }
}
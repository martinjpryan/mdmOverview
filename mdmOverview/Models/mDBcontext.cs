using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace mdmOverview.Models
{
    public class mDBcontext: DbContext
    {
             public mDBcontext() : base("name=dbContextMDM")
        {
        }
        public virtual DbSet<RawInterfaceData> RawInterfaceDatas { get; set; }
        public virtual DbSet<Plant> Plants { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}